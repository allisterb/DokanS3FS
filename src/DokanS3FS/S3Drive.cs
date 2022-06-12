using System;
using System.Collections.Generic;
using System.Linq;
namespace DokanS3FS;

using System.IO;
using Amazon.S3;
using Amazon.S3.Model;
using DokanNet;

public class S3Drive : Runtime
{
    public S3Drive(S3DriveConfig config) :base()
    {
        this.config = config;
        this.s3client = S3.GetS3Client(config);
        Initialized = true;
    }

    public NtStatus CreateFile(
            string fileName,
            DokanNet.FileAccess access,
            FileShare share,
            FileMode mode,
            FileOptions options,
            FileAttributes attributes,
            IDokanFileInfo info)
    {
        return NtStatus.Success;    
    }
    public S3DriveConfig config;
    public AmazonS3Client s3client;
}

