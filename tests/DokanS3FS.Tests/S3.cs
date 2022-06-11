using Xunit;

namespace DokanS3FS
{
    public class S3Tests
    {
        public S3Tests()
        {
            S3.SetConfig("s3.xml");
        }

        [Fact]
        public void CanConnectToS3()
        {
            Assert.NotEmpty(S3.Config.Drives);
        }
    }
}