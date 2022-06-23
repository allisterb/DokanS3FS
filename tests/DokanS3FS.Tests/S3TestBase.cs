namespace DokanS3FS;

public class S3TestBase
{
    public S3TestBase()
    {
        S3.SetConfig("s3.xml");
    }
}

