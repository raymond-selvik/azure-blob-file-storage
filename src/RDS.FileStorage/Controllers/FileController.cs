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
    [Route("filecontroller")]
    public class FileController : ControllerBase
    {
        private readonly IFileSystemService fileSystemService;

        private readonly IFileStorageService fileStorageService;
        private readonly ILogger<FileController> log;

        public FileController(
            IFileSystemService fileSystemService, 
            IFileStorageService fileStorageService,
            ILogger<FileController> log)
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

        [HttpPost("download")]
        public async Task<FileContentResult> Download([FromBody] FileModel file)
        {
            log.LogInformation(file.FullFileName);
            var bytes = await fileStorageService.DownloadFile(file.FullFileName);

            return File(bytes, "application/octet-stream");
        }
    }
}