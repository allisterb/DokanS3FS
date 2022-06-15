using System;
using System.Collections.Generic;
using System.Linq;
namespace DokanS3FS;

using System.IO;
using System.Globalization;

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

    #region Methods
    private NtStatus AsTrace(string method, string fileName, DokanFileInfo info, NtStatus result, params string[] parameters)
    {
        var extraParameters = parameters != null && parameters.Length > 0 ? ", " + string.Join(", ", parameters) : string.Empty;

        Verbose($"{System.Threading.Thread.CurrentThread.ManagedThreadId:D2} / {config.Vol} {method}({fileName}, {info.ToTrace()}{extraParameters}) -> {result}".ToString(CultureInfo.CurrentCulture));

        return result;
    }

    private NtStatus AsTrace(string method, string fileName, DokanFileInfo info, DokanNet.FileAccess access, FileShare share, FileMode mode, FileOptions options, FileAttributes attributes, NtStatus result)
    {
        Verbose($"{System.Threading.Thread.CurrentThread.ManagedThreadId:D2} / {config.Vol} {method}({fileName}, {info.ToTrace()}, [{access}], [{share}], [{mode}], [{options}], [{attributes}]) -> {result}".ToString(CultureInfo.CurrentCulture));

        return result;
    }

    private NtStatus AsDebug(string method, string fileName, DokanFileInfo info, NtStatus result, params string[] parameters)
    {
        var extraParameters = parameters != null && parameters.Length > 0 ? ", " + string.Join(", ", parameters) : string.Empty;

        Debug($"{System.Threading.Thread.CurrentThread.ManagedThreadId:D2} / {config.Vol} {method}({fileName}, {info.ToTrace()}{extraParameters}) -> {result}".ToString(CultureInfo.CurrentCulture));

        return result;
    }

    private NtStatus AsDebug(string method, string fileName, DokanFileInfo info, DokanNet.FileAccess access, FileShare share, FileMode mode, FileOptions options, FileAttributes attributes, NtStatus result)
    {
        Debug($"{System.Threading.Thread.CurrentThread.ManagedThreadId:D2} / {config.Vol} {method}({fileName}, {info.ToTrace()}, [{access}], [{share}], [{mode}], [{options}], [{attributes}]) -> {result}".ToString(CultureInfo.CurrentCulture));

        return result;
    }

    private NtStatus AsWarning(string method, string fileName, DokanFileInfo info, NtStatus result, params string[] parameters)
    {
        var extraParameters = parameters != null && parameters.Length > 0 ? ", " + string.Join(", ", parameters) : string.Empty;

        Warn($"{System.Threading.Thread.CurrentThread.ManagedThreadId:D2} / {config.Vol} {method}({fileName}, {info.ToTrace()}{extraParameters}) -> {result}".ToString(CultureInfo.CurrentCulture));

        return result;
    }

    private NtStatus AsError(string method, string fileName, DokanFileInfo info, NtStatus result, params string[] parameters)
    {
        var extraParameters = parameters != null && parameters.Length > 0 ? ", " + string.Join(", ", parameters) : string.Empty;

        Error($"{System.Threading.Thread.CurrentThread.ManagedThreadId:D2} / {config.Vol} {method}({fileName}, {info.ToTrace()}{extraParameters}) -> {result}".ToString(CultureInfo.CurrentCulture));

        return result;
    }

    private NtStatus AsError(string method, string fileName, DokanFileInfo info, DokanNet.FileAccess access, FileShare share, FileMode mode, FileOptions options, FileAttributes attributes, NtStatus result)
    {
        Error($"{System.Threading.Thread.CurrentThread.ManagedThreadId:D2} / {config.Vol} {method}({fileName}, {info.ToTrace()}, [{access}], [{share}], [{mode}], [{options}], [{attributes}]) -> {result}".ToString(CultureInfo.CurrentCulture));

        return result;
    }
    #endregion

    #region Fields
    public S3DriveConfig config;
    public AmazonS3Client s3client;
    #endregion
}

