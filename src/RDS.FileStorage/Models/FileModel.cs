using RDS.FileStorage.Utils;

namespace RDS.FileStorage.Models
{
    public class FileModel
    {
        public string Name {
            get
            {
                return PathUtils.FilePathToFileName(this.Path);
            } 
        }

        public string Path { get; set; }
    }
}