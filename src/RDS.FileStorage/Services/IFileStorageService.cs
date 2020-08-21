using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using RDS.FileStorage.Models;

namespace RDS.FileStorage.Services
{
    public interface IFileStorageService
    {
        List<FolderModel> GetListOfFolders(string currentDir);
        List<FileModel> GetListOfFiles(string currentDir);
        Task<byte[]> DownloadFile(string filePath);
    }
}