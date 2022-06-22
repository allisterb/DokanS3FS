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

using System.IO;
using System.Globalization;

using Amazon.S3;
using Amazon.S3.Model;
using DokanNet;

public partial class S3Drive : Runtime
{
    #region Constructors
    public S3Drive(S3DriveConfig config) : base()
    {
        this.config = config;
        this.s3client = S3.GetS3Client(config);
        this.bucket = this.config.Bucket;
        Initialized = true;
    }
    #endregion

    #region Fields
    public S3DriveConfig config;
    public AmazonS3Client s3client;
    public string bucket;
    #endregion

    #region Methods
    private NtStatus TraceNtStatus(string method, string fileName, IDokanFileInfo info, NtStatus result, params string[] parameters)
    {
        var extraParameters = parameters != null && parameters.Length > 0 ? ", " + string.Join(", ", parameters) : string.Empty;

        Verbose($"{System.Threading.Thread.CurrentThread.ManagedThreadId:D2} / {config.Vol} {method}({fileName}, {info.ToTrace()}{extraParameters}) -> {result}".ToString(CultureInfo.CurrentCulture));

        return result;
    }

    private NtStatus TraceNtStatus(string method, string fileName, IDokanFileInfo info, DokanNet.FileAccess access, FileShare share, FileMode mode, FileOptions options, FileAttributes attributes, NtStatus result)
    {
        Verbose($"{System.Threading.Thread.CurrentThread.ManagedThreadId:D2} / {config.Vol} {method}({fileName}, {info.ToTrace()}, [{access}], [{share}], [{mode}], [{options}], [{attributes}]) -> {result}".ToString(CultureInfo.CurrentCulture));

        return result;
    }

    private NtStatus DebugNtStatus(string method, string fileName, DokanFileInfo info, NtStatus result, params string[] parameters)
    {
        var extraParameters = parameters != null && parameters.Length > 0 ? ", " + string.Join(", ", parameters) : string.Empty;

        Debug($"{System.Threading.Thread.CurrentThread.ManagedThreadId:D2} / {config.Vol} {method}({fileName}, {info.ToTrace()}{extraParameters}) -> {result}".ToString(CultureInfo.CurrentCulture));

        return result;
    }

    private NtStatus DebugNtStatus(string method, string fileName, DokanFileInfo info, DokanNet.FileAccess access, FileShare share, FileMode mode, FileOptions options, FileAttributes attributes, NtStatus result)
    {
        Debug($"{System.Threading.Thread.CurrentThread.ManagedThreadId:D2} / {config.Vol} {method}({fileName}, {info.ToTrace()}, [{access}], [{share}], [{mode}], [{options}], [{attributes}]) -> {result}".ToString(CultureInfo.CurrentCulture));

        return result;
    }

    private NtStatus WarnNtStatus(string method, string fileName, DokanFileInfo info, NtStatus result, params string[] parameters)
    {
        var extraParameters = parameters != null && parameters.Length > 0 ? ", " + string.Join(", ", parameters) : string.Empty;

        Warn($"{System.Threading.Thread.CurrentThread.ManagedThreadId:D2} / {config.Vol} {method}({fileName}, {info.ToTrace()}{extraParameters}) -> {result}".ToString(CultureInfo.CurrentCulture));

        return result;
    }

    private NtStatus NtStatusError(string method, string fileName, DokanFileInfo info, NtStatus result, params string[] parameters)
    {
        var extraParameters = parameters != null && parameters.Length > 0 ? ", " + string.Join(", ", parameters) : string.Empty;

        Error($"{System.Threading.Thread.CurrentThread.ManagedThreadId:D2} / {config.Vol} {method}({fileName}, {info.ToTrace()}{extraParameters}) -> {result}".ToString(CultureInfo.CurrentCulture));

        return result;
    }

    private NtStatus NtStatusError(string method, string fileName, DokanFileInfo info, DokanNet.FileAccess access, FileShare share, FileMode mode, FileOptions options, FileAttributes attributes, NtStatus result)
    {
        Error($"{System.Threading.Thread.CurrentThread.ManagedThreadId:D2} / {config.Vol} {method}({fileName}, {info.ToTrace()}, [{access}], [{share}], [{mode}], [{options}], [{attributes}]) -> {result}".ToString(CultureInfo.CurrentCulture));

        return result;
    }
    #endregion

    
}


