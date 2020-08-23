using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging;
using RDS.FileStorage.Models;

namespace RDS.FileStorage.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly BlobServiceClient blobServiceClient;
        private readonly ILogger<IFileStorageService> log;

        private readonly BlobContainerClient containerClient;
        public FileStorageService(BlobServiceClient blobServiceClient, ILogger<IFileStorageService> log)
        {
            this.blobServiceClient = blobServiceClient;
            this.log = log;
            containerClient = blobServiceClient.GetBlobContainerClient("demo");
        }

        public async Task<byte[]> DownloadFile(FileModel file)
        {
            var blob = containerClient.GetBlobClient(file.FullPath);

            BlobDownloadInfo download = await blob.DownloadAsync();

            using (var ms = new MemoryStream())
            {
                download.Content.CopyTo(ms);
                return ms.ToArray();
            } 
        }

        //void UploadFilse

        //void Delete File
    }
}