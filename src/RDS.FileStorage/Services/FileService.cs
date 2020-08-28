using System;
using System.IO;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging;
using RDS.FileStorage.Exceptions;
using RDS.FileStorage.Models;

namespace RDS.FileStorage.Services
{
    public class FileService : IFileService
    {
        private readonly BlobServiceClient blobServiceClient;
        private readonly ICosmosService cosmosService;
        private readonly ILogger<IFileService> log;

        private readonly BlobContainerClient containerClient;
        public FileService(BlobServiceClient blobServiceClient, ICosmosService cosmosService, ILogger<IFileService> log)
        {
            this.blobServiceClient = blobServiceClient;
            this.cosmosService = cosmosService;
            this.log = log;
            containerClient = blobServiceClient.GetBlobContainerClient("demo");
        }

        public async Task DeleteFile(FileEntity file)
        {
            try
            {
                await cosmosService.DeleteItemAsync(file);

                await containerClient.DeleteBlobIfExistsAsync(file.Id, DeleteSnapshotsOption.IncludeSnapshots);
            }
            catch(RequestFailedException e)
            {
                log.LogError(e.Message);

                throw new FileException($"Could not delete file {file.Name}");
            }
        }

        public async Task<byte[]> GetFile(FileEntity file)
        {
            try 
            {
                var blobClient = containerClient.GetBlobClient(file.Id);

                BlobDownloadInfo blob = await blobClient.DownloadAsync();

                using(var ms = new MemoryStream())
                {
                    await blob.Content.CopyToAsync(ms);

                    return ms.ToArray();
                }
            }
            catch(RequestFailedException e)
            {
                log.LogError(e.Message);
                throw new FileException($"Could not get file {file.Name}.");
            }
        }

       
        public async Task SaveFile(FileEntity file, Stream content)
        {
            try
            {
                await cosmosService.AddItemAsync(file);

                var blobClient = containerClient.GetBlobClient(file.Id);
                await blobClient.UploadAsync(content);

            }
            catch(RequestFailedException e)
            {
                throw new FileException($"Could not save file {file.Name}");
            }        
        }
    }
}