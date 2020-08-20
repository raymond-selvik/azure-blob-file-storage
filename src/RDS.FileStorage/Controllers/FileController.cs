using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
            return fileStorageService.GetFiles("");
        }
    }
}