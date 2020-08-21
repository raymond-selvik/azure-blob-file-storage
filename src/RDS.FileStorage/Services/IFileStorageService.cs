using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using RDS.FileStorage.Models;

namespace RDS.FileStorage.Services
{
    public interface IFileStorageService
    {
        List<BlobFile> GetFiles(string directory);
        Task<byte[]> DownloadFile(string filePath);
    }
}