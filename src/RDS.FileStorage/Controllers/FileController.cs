using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RDS.FileStorage.Models;
using RDS.FileStorage.Services;

namespace RDS.FileStorage.Controllers
{
    [ApiController]
    [Route("file")]
    public class FileController : ControllerBase
    {

        private readonly IFileStorageService fileStorageService;
        private readonly ILogger<FileController> log;

        public FileController(IFileStorageService fileStorageService, ILogger<FileController> log)
        {
            this.fileStorageService = fileStorageService;
            this.log = log;
        }

        [HttpPost("download")]
        public async Task<ActionResult> Download([FromBody] FileModel file)
        {
            try
            {
                var fileBytes = await fileStorageService.DownloadFile(file);
                return File(fileBytes, "application/octet-stream");
            }
            catch(FileNotFoundException e)
            {
                log.LogError(e.Message);
                return new NotFoundResult();
            }
        }
    }
}