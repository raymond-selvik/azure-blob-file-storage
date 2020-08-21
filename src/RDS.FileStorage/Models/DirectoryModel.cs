using System.Collections.Generic;

namespace RDS.FileStorage.Models
{
    public class DirectoryModel
    {
        public string Path { get; set; }
        public List<FileModel> Files { get; set; }
        public List<FolderModel> Folders { get; set; }

    }
}