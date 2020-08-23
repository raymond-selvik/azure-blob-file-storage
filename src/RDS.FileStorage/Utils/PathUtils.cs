using System.IO;

namespace RDS.FileStorage.Utils
{
    public static class PathUtils
    {
        public static string BlobPrefixToDirPath(string prefix)
        {
            var path = prefix.Remove(prefix.Length - 1);
            return path;
        }

        public static string DirPathToBlobPrefix(string dir)
        {
            if (dir == null) 
            {
                return "";
            }
            else
            {
                return dir + "/";
            }
        }
    }
}