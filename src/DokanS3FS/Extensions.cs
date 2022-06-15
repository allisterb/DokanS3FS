namespace DokanS3FS;

using System.Globalization;

using DokanNet;

public static class DokanFileInfoExtensions
{
    public static string ToTrace(this DokanFileInfo info)
    {
        return $"{{{(info.Context != null ? info.Context.ToString() : "<null>")}, {(info.DeleteOnClose ? nameof(info.DeleteOnClose) : string.Empty)}, {(info.IsDirectory ? nameof(info.IsDirectory) : string.Empty)}, {(info.NoCache ? nameof(info.NoCache) : string.Empty)}, {(info.PagingIo ? nameof(info.PagingIo) : string.Empty)}, {info.ProcessId}, {(info.SynchronousIo ? nameof(info.SynchronousIo) : string.Empty)}, {(info.WriteToEndOfFile ? nameof(info.WriteToEndOfFile) : string.Empty)}}}".ToString(CultureInfo.CurrentCulture);
    }
}


