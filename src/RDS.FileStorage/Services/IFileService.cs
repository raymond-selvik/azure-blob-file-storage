using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using RDS.FileStorage.Models;

namespace RDS.FileStorage.Services
{
    public interface IFileService
    {
        Task<byte[]> GetFile(string filePath);
        Task DeleteFile(string filePath);
        Task MoveFile(string fromFilePath, string toFilePath);
        Task RenameFile(string filePath, string newName);
        Task SaveFile(string filePath, Stream file);
    }
}