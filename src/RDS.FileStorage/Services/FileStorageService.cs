using System.Collections.Generic;
using Azure.Storage.Blobs;
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

        public List<BlobFile> GetFiles(string directory)
        {
            
            var files = new List<BlobFile>();

            foreach(var item in containerClient.GetBlobsByHierarchy(prefix: directory, delimiter : "/"))
            {
                if(item.IsBlob)
                {
                    string filename = item.Blob.Name.Substring(item.Blob.Name.LastIndexOf("/") + 1);

                    var file = new BlobFile
                    {
                        Name = filename,
                    };


                    files.Add(file);
                }
            }

            return files;
        }
    }
}