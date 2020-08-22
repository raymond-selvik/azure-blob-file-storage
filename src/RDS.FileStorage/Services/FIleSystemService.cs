using System.Collections.Generic;
using System.IO;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;
using RDS.FileStorage.Models;

namespace RDS.FileStorage.Services
{
    public class FileSystemService : IFileSystemService
    {

        private readonly BlobServiceClient blobServiceClient;
        private readonly ILogger<IFileSystemService> log;
        private readonly BlobContainerClient containerClient;
        
        public FileSystemService(BlobServiceClient blobServiceClient, ILogger<IFileSystemService> log)
        {
            this.blobServiceClient = blobServiceClient;
            this.log = log;
            containerClient = blobServiceClient.GetBlobContainerClient("demo");
        }

        
        public List<FolderModel> GetListOfFolders(string dir)
        {
            var folders = new List<FolderModel>();

            foreach(var item in containerClient.GetBlobsByHierarchy(prefix: dir, delimiter : "/"))
            {
                if(item.IsPrefix)
                {
                    folders.Add(new FolderModel{Path = item.Prefix});
                }
            }

            return folders;
        }

        public List<FileModel> GetListOfFiles(string dir)
        {
            var files = new List<FileModel>();

            foreach(var item in containerClient.GetBlobsByHierarchy(prefix: dir, delimiter : "/"))
            {
                if(item.IsBlob)
                {
                    files.Add(new FileModel{Path = item.Blob.Name});
                }
            }
            return files;
        }
    }
}