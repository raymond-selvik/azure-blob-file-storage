using System.Collections.Generic;
using RDS.FileStorage.Models;

namespace RDS.FileStorage.Services
{
    public interface IFileStorageService
    {
        List<BlobFile> GetFiles(string directory);
    }
}