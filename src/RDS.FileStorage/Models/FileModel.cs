using System.IO;

namespace RDS.FileStorage.Models
{
    public class FileModel
    {
        public string Name {
            get
            {
                return Path.GetFileName(this.FullPath);
            } 
        }

        public string FullPath { get; set; }
    }
}