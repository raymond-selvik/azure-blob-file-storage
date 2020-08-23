using RDS.FileStorage.Utils;
using Xunit;

namespace RDS.FileStorage.Test.UtilTests
{
    public class PathUtilTest 
    {
        [Fact]
        public void DirPathToBlobPrefixTest()
        {
            var path = "files/folder";
            var expected = "files/folder/";

            var actual = PathUtils.DirPathToBlobPrefix(path);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BlobPrefixToDirPathTest()
        {
            var prefix = "files/";
            var expected = "files";

            var actual = PathUtils.BlobPrefixToDirPath(prefix);

            Assert.Equal(expected, actual);
        }
    }
}