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

        public List<BlobDirectory> GetListOfDirectories(string currentDir)
        {
            var directories = new List<BlobDirectory>();

            foreach(var item in containerClient.GetBlobsByHierarchy(prefix: currentDir, delimiter : "/"))
            {
                if(item.IsPrefix)
                {
                    var dir = new BlobDirectory
                    {
                        Name = Path.GetRelativePath(currentDir, item.Prefix),
                        FullPath = item.Prefix
                    };

                    directories.Add(dir);
                }
            }

            return directories;
        }

        public List<BlobFile> GetListOfFiles(string directory)
        {
            
            var files = new List<BlobFile>();

            foreach(var item in containerClient.GetBlobsByHierarchy(prefix: directory, delimiter : "/"))
            {
                if(item.IsBlob)
                {
                    string filename = item.Blob.Name.Substring(item.Blob.Name.LastIndexOf("/") + 1);

                    var file = new BlobFile
                    {
                        FileName = Path.GetFileName(item.Blob.Name),
                        Directory = Path.GetDirectoryName(item.Blob.Name),
                        FullFileName = item.Blob.Name
                    };


                    files.Add(file);
                }
            }
            return files;
        }

        public async Task<byte[]> DownloadFile(string filePath)
        {
            var blob = containerClient.GetBlobClient(filePath);

            BlobDownloadInfo download = await blob.DownloadAsync();

            using (var ms = new MemoryStream())
            {
                download.Content.CopyTo(ms);
                return ms.ToArray();
            } 
        }
    }
}