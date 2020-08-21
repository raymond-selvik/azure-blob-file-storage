using RDS.FileStorage.Models;

namespace RDS.FileStorage.Services
{
    public interface IFileSystemService
    {
        DirectoryModel GetDirectory(string currentDir);
    }
}