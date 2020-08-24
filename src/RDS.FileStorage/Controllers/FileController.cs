using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RDS.FileStorage.Exceptions;
using RDS.FileStorage.Models;
using RDS.FileStorage.Services;

namespace RDS.FileStorage.Controllers
{
    [ApiController]
    [Route("file")]
    public class FileController : ControllerBase
    {

        private readonly IFileService fileService;
        private readonly IDirectoryService directoryService;
        private readonly ILogger<FileController> log;

        public FileController(
            IFileService fileService, 
            ILogger<FileController> log)
        {
            this.fileService = fileService;
            this.log = log;
        }

        [HttpPost("download")]
        public async Task<IActionResult> Download([FromBody] FileModel file)
        {
            try
            {
                var fileBytes = await fileService.GetFile(file.FullPath);
                return File(fileBytes, "application/octet-stream");
            }
            catch(FileException e)
            {
                log.LogError(e.Message);
                return new NotFoundResult();
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] FileDto file)
        {
            
            try
            {
                using(var stream = file.File.OpenReadStream())
                {
                    await fileService.SaveFile(file.Path + "/" + file.File.FileName, stream);
                }

                return Ok();
            }
            catch(FileException e)
            {
                log.LogError(e.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);

            }
        }
    }
}