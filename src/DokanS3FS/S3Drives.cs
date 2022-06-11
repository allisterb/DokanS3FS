using System;
using System.Xml;
using System.Xml.Serialization;

namespace DokanS3FS;

[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(AnonymousType = true)]
[XmlRoot("s3", Namespace = "", IsNullable = false)]
public partial class S3Drives
{
    [XmlAttribute("threads")]
    public byte Threads { get; set; }

    [XmlArray("drives", IsNullable = false)]
    [XmlArrayItem("drive", IsNullable = false)]
    public S3Drive[] Drives { get; set; } = {};

    [XmlIgnore]
    public string Source { get; set; } = string.Empty;
}

[Serializable()]
[System.ComponentModel.DesignerCategory("code")]
[XmlType(AnonymousType = true)]
public partial class S3Drive
{
    [XmlAttribute("vol")]
    public string Vol { get; set; } = string.Empty;

    [XmlAttribute("url")]
    public string ServiceUrl { get; set; } = string.Empty;

    [XmlAttribute("bucket")]
    public string Bucket { get; set; } = string.Empty;

    [XmlAttribute("key")]
    public string Key { get; set; } = string.Empty;

    [XmlAttribute("secret")]
    public string Secret { get; set; } = string.Empty;

    [XmlAttribute("timeout")]
    public ushort Timeout { get; set; } = 100;
}