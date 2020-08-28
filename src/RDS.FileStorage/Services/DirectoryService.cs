using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
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

        private readonly ICosmosService cosmosService;
        private readonly ILogger<IDirectoryService> log;

        
        public DirectoryService(ICosmosService cosmosService, ILogger<IDirectoryService> log)
        {
            this.cosmosService = cosmosService;
            this.log = log;
        }

        
        public async Task<List<FolderModel>> GetListOfFolders(string dir)
        {
           
            var folders = new List<FolderModel>();
            var files = await cosmosService.GetItemsAsync($"SELECT * from c  WHERE c.folder = '{dir}' AND c.type = '{FileType.FOLDER}'");
            
            foreach(var file in files)
            {
                folders.Add(new FolderModel{FullPath = file.Path, Name = file.Name});
            }
            folders = folders.Distinct().ToList();
            return folders;
    
        }

        public async Task<List<FileEntity>> GetListOfFiles(string dir)
        {
            return await cosmosService.GetItemsAsync($"SELECT * FROM c WHERE c.folder = '{dir}' AND c.type = '{FileType.FILE}'");
        }

        public async Task AddNewFolder(string dir, string name)
        {
            var folder = new FileEntity 
            {
                Id = Guid.NewGuid().ToString(),
                Path = Path.Join(dir, name),
                Name = name,
                Folder = dir,
                Type = FileType.FOLDER,
                Drive = "demo"
            };

            await cosmosService.AddItemAsync(folder);
        }
    }
}