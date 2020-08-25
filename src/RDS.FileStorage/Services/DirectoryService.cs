using System.Collections.Generic;
using System.IO;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging;
using RDS.FileStorage.Exceptions;
using RDS.FileStorage.Models;

namespace RDS.FileStorage.Services
{
    public class DirectoryService : IDirectoryService
    {

        private readonly BlobServiceClient blobServiceClient;
        private readonly ILogger<IDirectoryService> log;
        private readonly BlobContainerClient containerClient;
        
        public DirectoryService(BlobServiceClient blobServiceClient, ILogger<IDirectoryService> log)
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
                 var prefix = DirPathToBlobPrefix(dir);

                 foreach(var item in containerClient.GetBlobsByHierarchy(prefix: prefix, delimiter : "/"))
                 {
                     if(item.IsPrefix)
                     {
                         folders.Add(new FolderModel{FullPath = BlobPrefixToDirPath(item.Prefix)});
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
                 var prefix =  DirPathToBlobPrefix(dir);

                 foreach(var item in containerClient.GetBlobsByHierarchy(prefix: DirPathToBlobPrefix(dir), delimiter : "/"))
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

        string DirPathToBlobPrefix(string dir)
        {
            if (dir == null) 
            {
                return "";
            }
            else
            {
                return dir + "/";
            }
        }

        string BlobPrefixToDirPath(string prefix)
        {
            if(prefix == "") 
            {
                return "";
            }
            else
            {
                return prefix.Remove(prefix.Length - 1);
            }
        }
    }
}