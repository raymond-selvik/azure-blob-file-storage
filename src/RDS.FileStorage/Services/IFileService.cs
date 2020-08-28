using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using RDS.FileStorage.Models;

namespace RDS.FileStorage.Services
{
    public interface IFileService
    {
        Task<byte[]> GetFile(FileEntity file);
        Task DeleteFile(FileEntity file);
        Task SaveFile(FileEntity file, Stream content);
    }
}