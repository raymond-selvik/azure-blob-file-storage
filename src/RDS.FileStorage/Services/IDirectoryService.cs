using System.Collections.Generic;
using System.Threading.Tasks;
using RDS.FileStorage.Models;

namespace RDS.FileStorage.Services
{
    public interface IDirectoryService
    {
        Task<List<FolderModel>> GetListOfFolders(string dir);
        Task<List<FileEntity>> GetListOfFiles(string dir);
        Task AddNewFolder(string dir, string name);
    }
}