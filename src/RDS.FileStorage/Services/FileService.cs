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
        private readonly ILogger<IFileService> log;

        private readonly BlobContainerClient containerClient;
        public FileService(BlobServiceClient blobServiceClient, ILogger<IFileService> log)
        {
            this.blobServiceClient = blobServiceClient;
            this.log = log;
            containerClient = blobServiceClient.GetBlobContainerClient("demo");
        }

        public async Task DeleteFile(string filePath)
        {
            try
            {
                await containerClient.DeleteBlobIfExistsAsync(filePath, DeleteSnapshotsOption.IncludeSnapshots);
            }
            catch(RequestFailedException e)
            {
                log.LogError(e.Message);

                throw new FileException($"Could not delete file {filePath}");
            }
        }

        public async Task<byte[]> GetFile(string filePath)
        {
            try 
            {
                var blobClient = containerClient.GetBlobClient(filePath);

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
                throw new FileException($"Could not get file {filePath}.");
            }
        }

        public async Task MoveFile(string fromFilePath, string toFilePath)
        {
            try
            {
                var blobClient = containerClient.GetBlobClient(fromFilePath);
                BlobDownloadInfo blob = await blobClient.DownloadAsync();

                using(var ms = new MemoryStream())
                {
                    await blob.Content.CopyToAsync(ms);
                    await containerClient.UploadBlobAsync(toFilePath, ms);
                }

                await containerClient.DeleteBlobIfExistsAsync(fromFilePath, DeleteSnapshotsOption.IncludeSnapshots);
            }
            catch(RequestFailedException e)
            {
                log.LogError(e.Message);
                throw new FileException($"Could not Move file from {fromFilePath} to {toFilePath}");
            }
        }

        public async Task RenameFile(string filePath, string newName)
        {
            string directory = Path.GetDirectoryName(filePath);
            string newFilePath = Path.Combine(directory, newName);
            
            try
            {
                await MoveFile(directory, newFilePath);
            }
            catch(FileException e)
            {
                throw new FileException($"Could not rename file {filePath}");
            }
            
        }

        public async Task<FileModel> SaveFile(string filePath, Stream file)
        {
            try
            {
                var blobClient = containerClient.GetBlobClient(filePath);
                await blobClient.UploadAsync(file);

                return new FileModel{FullPath = blobClient.Name};

            }
            catch(RequestFailedException e)
            {
                throw new FileException($"Could not save file {filePath}");
            }        
        }
    }
}