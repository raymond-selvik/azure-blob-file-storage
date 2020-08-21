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
        private readonly IFileStorageService fileStorageService;
        private readonly ILogger<FileController> log;

        public FileController(IFileStorageService fileStorageService, ILogger<FileController> log)
        {
            this.fileStorageService = fileStorageService;
            this.log = log;
        }

        [HttpGet]
        public IEnumerable<BlobFile> Get()
        {
            var files = fileStorageService.GetFiles("files/");
            return files;
        }

        [HttpPost("download")]
        public async Task<FileContentResult> Download([FromBody] BlobFile file)
        {

            log.LogInformation(file.FullFileName);
            var bytes = await fileStorageService.DownloadFile(file.FullFileName);

            return File(bytes, "application/octet-stream");
            //return Ok();

            /*var stream = new MemoryStream(Encoding.ASCII.GetBytes("Hello World"));
            return new FileStreamResult(stream, new MediaTypeHeaderValue("text/plain"))
            {
                FileDownloadName = "test.txt"
            };*/
        }

    }
}