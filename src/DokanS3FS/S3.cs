namespace DokanS3FS;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Amazon.S3;
using Amazon.S3.Model;
public class S3 : Runtime
{
    #region Config
    public static new S3Config Config { get; private set; } = new S3Config();
    
    public static S3Config ParseConfig(string filename)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(S3Config));
        using Stream reader = new FileStream(filename, FileMode.Open);
        S3Config config =  (S3Config) serializer.Deserialize(reader)!;
        if (config is null)
        {
            throw new S3ConfigException("Did not parse S3 drives configuration.");
        }
        
        // We need to validate attributes manually because we aren't using a XSD schema.
        for(int i = 0; i < config.Drives.Length; i++)
        {
            if (string.IsNullOrEmpty(config.Drives[i].Vol)) throw new S3ConfigException($"The vol attribute is required for drive element at index {i}.");
            if (string.IsNullOrEmpty(config.Drives[i].ServiceUrl)) throw new S3ConfigException($"The url attribute is required for drive element at index {i}.");
            if (string.IsNullOrEmpty(config.Drives[i].Key)) throw new S3ConfigException($"The key attribute is required for drive element at index {i}.");
            if (string.IsNullOrEmpty(config.Drives[i].Secret)) throw new S3ConfigException($"The secret attribute is required for drive element at index {i}.");
        }

        return config;
    }

    public static void SetConfig(string filename)
    {
        Config = ParseConfig(filename);
        Config.Source = File.ReadAllText(filename);
        foreach (var d in Config.Drives)
        {
            Drives.Add(new S3Drive(d));
        }
    }

    public static List<S3Drive> Drives { get; } = new ();
    #endregion

    #region S3 API
    public static AmazonS3Client GetS3Client(string url, string key, string secret)
    {
        AmazonS3Config config = new AmazonS3Config();
        config.ServiceURL = url;
        Debug("Creating client for service url {0}...", url);
        return new AmazonS3Client(key, secret, config);
    }

    public static AmazonS3Client GetS3Client(S3DriveConfig drive) => GetS3Client(drive.ServiceUrl, drive.Key, drive.Secret);

    public static async Task<bool> CreateBucket(AmazonS3Client client, string name)
    {
        try
        {
            using var op = Runtime.Begin("Creating bucket {0}", name);
            var r = await client.PutBucketAsync(name, Ct);
            op.Complete();
            return r.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }
        catch (Exception e)
        {
            Error(e, "Error creating bucket {0}.", name);
            return false;
        }
    }
    #endregion
}
