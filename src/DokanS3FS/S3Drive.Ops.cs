#region Attribution
// Contains code from: https://github.com/viciousviper/DokanCloudFS/blob/develop/DokanCloudFS/CloudOperations.cs
// CloudOperations.cs has the following copyright notice:

/*
The MIT License(MIT)

Copyright(c) 2015 IgorSoft

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion

namespace DokanS3FS;

using System;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.IO;

using DokanNet;

public partial class S3Drive : Runtime
{
    public NtStatus CreateFile(
            string fileName,
            DokanNet.FileAccess access,
            FileShare share,
            FileMode mode,
            FileOptions options,
            FileAttributes attributes,
            IDokanFileInfo info)
    {
        if (info == null)
            throw new ArgumentNullException(nameof(info));

        // HACK: Fix for Bug in Dokany related to a missing trailing slash for directory names
        if (string.IsNullOrEmpty(fileName))
            fileName = @"\";
        // HACK: Fix for Bug in Dokany related to a call to CreateFile with a fileName of '\*'
        else if (fileName == @"\*" && access == DokanNet.FileAccess.ReadAttributes)
            return TraceNtStatus(nameof(CreateFile), fileName, info, access, share, mode, options, attributes, DokanResult.Success);

        if (fileName == @"\")
        {
            info.IsDirectory = true;
            return TraceNtStatus(nameof(CreateFile), fileName, info, access, share, mode, options, attributes, DokanResult.Success);
        }

        fileName = fileName.TrimEnd(Path.DirectorySeparatorChar);

        switch (mode)
        {
            case FileMode.Create:
                var f = new S3FileInfo(s3client, bucket, fileName, Ct);
                info.Context = new S3FileStreamContext(f, DokanNet.FileAccess.WriteData);
                if (f.Exists)
                {
                    return TraceNtStatus(nameof(CreateFile), fileName, info, access, share, mode, options, attributes, DokanResult.AlreadyExists);
                }
                break;
            default:
                throw new NotImplementedException();
        }
        return NtStatus.Success;
    }
}