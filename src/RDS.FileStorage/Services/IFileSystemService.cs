using System.Collections.Generic;
using RDS.FileStorage.Models;

namespace RDS.FileStorage.Services
{
    public interface IFileSystemService
    {
        List<FolderModel> GetListOfFolders(string dir);
        List<FileModel> GetListOfFiles(string dir);
    }
}