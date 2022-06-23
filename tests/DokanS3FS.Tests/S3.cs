using System.Linq;

using Xunit;

namespace DokanS3FS
{
    public class S3Tests : S3TestBase
    {
        public S3Tests() : base() {}

        [Fact]
        public void CanConnectToS3()
        {
            Assert.NotEmpty(S3.Config.Drives);
            Assert.NotEmpty(S3.Drives);
        }
    }
}