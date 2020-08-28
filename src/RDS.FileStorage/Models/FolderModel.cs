using System.IO;
using Newtonsoft.Json;

namespace RDS.FileStorage.Models
{
    public class FolderModel
    {
        public string Name { get; set; }
        public string FullPath {get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}