using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using RDS.FileStorage.Models;
using RDS.FileStorage.Services;

namespace RDS.FileStorage.Controllers
{
    [ApiController]
    [Route("directory")]
    public class DirectoryController : ControllerBase
    {
        private readonly IDirectoryService directoryService;

        private readonly ILogger<DirectoryController> log;

        public DirectoryController(
            IDirectoryService directoryService, 
            ILogger<DirectoryController> log)
        {
            this.directoryService = directoryService;
            this.log = log;
        }


        [HttpGet("folders")]
        public async Task<List<FolderModel>> GetListOfFolders(string dir)
        {
            
            var folders = await directoryService.GetListOfFolders(Path.GetFullPath(dir));
            return folders;
        }

        [HttpGet("files")]
        public async Task<List<FileEntity>> GetListOfFiles(string dir)
        {
            return await directoryService.GetListOfFiles(Path.GetFullPath(dir));
        }

        [HttpPost("newfolder")]
        public async Task AddNewFolder([FromBody] FolderModel folder)
        {
            await directoryService.AddNewFolder(folder.FullPath, folder.Name);
        }
    }
}