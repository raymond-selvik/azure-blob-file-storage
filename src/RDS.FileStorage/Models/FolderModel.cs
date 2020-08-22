using RDS.FileStorage.Utils;

namespace RDS.FileStorage.Models
{
    public class FolderModel
    {
        public string Name 
        { 
            get 
            {
                return PathUtils.FolderPathToFolderName(this.Path);
            }  
        }
        public string Path {get; set; }
    }
}