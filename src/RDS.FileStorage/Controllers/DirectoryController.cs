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
        private readonly IFileSystemService fileSystemService;

        private readonly IFileStorageService fileStorageService;
        private readonly ILogger<DirectoryController> log;

        public DirectoryController(
            IFileSystemService fileSystemService, 
            IFileStorageService fileStorageService,
            ILogger<DirectoryController> log)
        {
            this.fileSystemService = fileSystemService;
            this.fileStorageService = fileStorageService;
            this.log = log;
        }


        [HttpGet("folders")]
        public IEnumerable<FolderModel> GetListOfFolders(string dir)
        {
            return fileSystemService.GetListOfFolders(dir);
        }

        [HttpGet("files")]
        public IEnumerable<FileModel> GetListOfFiles(string dir)
        {
            return fileSystemService.GetListOfFiles(dir);
        }
    }
}