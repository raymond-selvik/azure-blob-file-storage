using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using RDS.FileStorage.Models;

namespace RDS.FileStorage.Services
{
    public interface IFileStorageService
    {
        Task<byte[]> DownloadFile(FileModel file);

    }
}