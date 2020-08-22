using RDS.FileStorage.Utils;
using Xunit;

namespace RDS.FileStorage.Test.UtilTests
{
    public class PathUtilTest 
    {
        [Fact]
        public void BlobNameToFileNameTest()
        {

        }

        [Fact]
        public void BlobPrefixToFolderName()
        {
            var expected = "folder";
            var path = "files/folder/";

            var actual = PathUtils.FolderPathToFolderName(path);

            Assert.Equal(expected, actual);

        }
    }
}