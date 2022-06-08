namespace DokanS3FS;

using Amazon.S3;
using Amazon.S3.Model;
public class S3 : Runtime
{
    public static void Connect()
    {
        AmazonS3Config config = new AmazonS3Config();
        config.ServiceURL = "https://us-southeast-1.linodeobjects.com";
        var client = new AmazonS3Client(Config("AnS3KeyID"), Config("AnS3KeySecret"), config);
        ListBucketsResponse response = client.ListBucketsAsync().Result;
    }
}
;