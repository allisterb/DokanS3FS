namespace DokanS3FS;

using Amazon.S3;
using Amazon.S3.Model;
public class S3 : Runtime
{
    public static AmazonS3Client GetClient(string key, string secret, string url)
    {
        AmazonS3Config config = new AmazonS3Config();
        config.ServiceURL = url;
        Debug("Creating client for service url {0}...", url);
        return new AmazonS3Client(key, secret, config);
    }
}
;