using Microsoft.AspNetCore.Http;

namespace RDS.FileStorage.Models 
{
    public class FileDto 
    {
        public string Path {get; set; }

        public IFormFile FileForm { get; set; }

    }
}