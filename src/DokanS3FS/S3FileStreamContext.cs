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

using Amazon.S3.IO;

using DokanNet;

[System.Diagnostics.DebuggerDisplay("{DebuggerDisplay,nq}")]
public class S3FileStreamContext : IDisposable
{
    public S3FileInfo File { get; }

    public DokanNet.FileAccess Access { get; }

    public Stream? Stream { get; set; }

    public Task? Task { get; set; }

    public bool IsLocked { get; set; }

    public bool CanWriteDelayed => Access.HasFlag(DokanNet.FileAccess.WriteData) && (Stream?.CanRead ?? false) && Task == null;

    public S3FileStreamContext(S3FileInfo file, DokanNet.FileAccess access)
    {
        File = file;
        Access = access;
    }

    public void Dispose()
    {
        Stream?.Dispose();
    }

    public override string ToString() => DebuggerDisplay;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Debugger Display")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    private string DebuggerDisplay => $"{nameof(S3FileStreamContext)} {File.Name} [{Access}] [{nameof(Stream.Length)}={((Stream?.CanSeek ?? false) ? Stream.Length : 0)}] [{nameof(Task.Status)}={Task?.Status}] {nameof(IsLocked)}={IsLocked}".ToString(CultureInfo.CurrentCulture);
}