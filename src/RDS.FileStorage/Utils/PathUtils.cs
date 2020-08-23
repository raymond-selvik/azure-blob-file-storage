using System.IO;

namespace RDS.FileStorage.Utils
{
    public static class PathUtils
    {
        public static string BlobPrefixToDirPath(string prefix)
        {
            if(prefix == "") 
            {
                return "";
            }
            else
            {
                return prefix.Remove(prefix.Length - 1);
            }
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