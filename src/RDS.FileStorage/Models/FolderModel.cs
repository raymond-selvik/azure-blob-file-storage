using System.IO;
namespace RDS.FileStorage.Models
{
    public class FolderModel
    {
        public string Name 
        { 
            get 
            {
                return Path.GetFileNameWithoutExtension(FullPath);
            }  
        }
        public string FullPath {get; set; }
    }
}