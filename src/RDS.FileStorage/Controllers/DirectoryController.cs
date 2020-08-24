using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
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
        public IEnumerable<FolderModel> GetListOfFolders(string dir)
        {
            return directoryService.GetListOfFolders(dir);
        }

        [HttpGet("files")]
        public IEnumerable<FileModel> GetListOfFiles(string dir)
        {
            return directoryService.GetListOfFiles(dir);
        }
    }
}