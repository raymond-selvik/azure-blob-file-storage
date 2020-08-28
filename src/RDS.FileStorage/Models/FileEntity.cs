using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace RDS.FileStorage.Models
{
    public class FileEntity
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type {get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "path")]
        public string Path { get; set; }
        
        [JsonProperty(PropertyName = "folder")]
        public string Folder { get; set; }


        [JsonProperty(PropertyName = "drive")]
        public string Drive { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }

    public static class FileType {
        public static string FOLDER = "FOLDER";
        public static string FILE = "FILE";
    }
}