using System.IO;

namespace RDS.FileStorage.Utils
{
    public static class PathUtils
    {
        public static string FilePathToFileName(string path)
        {
            return path.Substring(path.LastIndexOf("/") + 1);
        }

        public static string FolderPathToFolderName(string path)
        {
            var folder = path.Remove(path.Length - 1);
            return folder.Substring(folder.LastIndexOf("/") + 1);
        }

    }
}