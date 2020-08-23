using System.Collections.Generic;
using System.IO;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging;
using RDS.FileStorage.Exceptions;
using RDS.FileStorage.Models;
using RDS.FileStorage.Utils;

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
            try
            {
                 var folders = new List<FolderModel>();
                 var prefix = PathUtils.DirPathToBlobPrefix(dir);

                 foreach(var item in containerClient.GetBlobsByHierarchy(prefix: prefix, delimiter : "/"))
                 {
                     if(item.IsPrefix)
                     {
                         folders.Add(new FolderModel{FullPath = PathUtils.BlobPrefixToDirPath(item.Prefix)});
                     }
                 }

                 return folders;
            }
            catch(RequestFailedException e)
            {
                throw new DirectoryException($"Could not get list of folders in directory {dir}");
            }
        }

        public List<FileModel> GetListOfFiles(string dir)
        {
            try
            {
                 var files = new List<FileModel>();
                 var prefix =  PathUtils.DirPathToBlobPrefix(dir);

                 foreach(var item in containerClient.GetBlobsByHierarchy(prefix: PathUtils.DirPathToBlobPrefix(dir), delimiter : "/"))
                 {
                     if(item.IsBlob)
                     {
                         files.Add(new FileModel{FullPath = item.Blob.Name });
                     }
                 }

                 return files;
            }
            catch(RequestFailedException e)
            {
                throw new DirectoryException($"Could not get list of files in directory {dir}");
            }
        }
    }
}