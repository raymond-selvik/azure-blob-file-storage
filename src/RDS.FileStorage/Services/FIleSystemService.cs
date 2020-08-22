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

        
        public List<FolderModel> GetListOfFolders(string currentDir)
        {
            var folders = new List<FolderModel>();

            foreach(var item in containerClient.GetBlobsByHierarchy(prefix: currentDir, delimiter : "/"))
            {
                if(item.IsPrefix)
                {
                    var dir = new FolderModel
                    {
                        Name = Path.GetRelativePath(currentDir, item.Prefix),
                        FullPath = item.Prefix
                    };

                    folders.Add(dir);
                }
            }

            return folders;
        }

        public List<FileModel> GetListOfFiles(string directory)
        {
            
            var files = new List<FileModel>();

            foreach(var item in containerClient.GetBlobsByHierarchy(prefix: directory, delimiter : "/"))
            {
                if(item.IsBlob)
                {
                    string filename = item.Blob.Name.Substring(item.Blob.Name.LastIndexOf("/") + 1);

                    var file = new FileModel
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


    }
}