using Microsoft.Extensions.Logging;
using RDS.FileStorage.Models;

namespace RDS.FileStorage.Services
{
    public class FileSystemService : IFileSystemService
    {
        public readonly IFileStorageService fileStorageService;
        public readonly ILogger<IFileSystemService> log;

        public FileSystemService(IFileStorageService fileStorageService, ILogger<IFileSystemService> log)
        {
            this.fileStorageService = fileStorageService;
            this.log = log;
        }

        public DirectoryModel GetDirectory(string currentDir)
        {
            var files = fileStorageService.GetListOfFiles(currentDir);  
            var folders = fileStorageService.GetListOfFolders(currentDir);

            var directory = new DirectoryModel 
            {
                Path = currentDir,
                Files = files,
                Folders = folders
            };

            return directory;
        }

    }
}