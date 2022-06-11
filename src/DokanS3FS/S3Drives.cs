using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
namespace DokanS3FS
{
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName ="s3")]
    public partial class S3Drives
    {
        private byte threadsField;

        private mountDrive[]? drivesField;

        /// <remarks/>
        [XmlAttribute()]
        public byte threads
        {
            get
            {
                return this.threadsField;
            }
            set
            {
                this.threadsField = value;
            }
        }

        /// <remarks/>
        [XmlArrayItem("drive", IsNullable = false)]
        //[XmlElement(ElementName = "drives", IsNullable = false)]
        public mountDrive[]? drives
        {
            get
            {
                return this.drivesField;
            }
            set
            {
                this.drivesField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class mountDrive
    {

        private string schemaField = string.Empty;

        private string userNameField = string.Empty;

        private string rootField = string.Empty;

        private string encryptionKeyField = string.Empty;

        private ushort timeoutField;

        private string parametersField = string.Empty;

        private string apiKeyField = string.Empty;

        [XmlAttribute()]
        public string schema
        {
            get
            {
                return this.schemaField;
            }
            set
            {
                this.schemaField = value;
            }
        }

      
        [XmlAttribute()]
        public string userName
        {
            get
            {
                return this.userNameField;
            }
            set
            {
                this.userNameField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string root
        {
            get
            {
                return this.rootField;
            }
            set
            {
                this.rootField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string encryptionKey
        {
            get
            {
                return this.encryptionKeyField;
            }
            set
            {
                this.encryptionKeyField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public ushort timeout
        {
            get
            {
                return this.timeoutField;
            }
            set
            {
                this.timeoutField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string? parameters
        {
            get
            {
                return this.parametersField;
            }
            set
            {
                this.parametersField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string? apiKey
        {
            get
            {
                return this.apiKeyField;
            }
            set
            {
                this.apiKeyField = value;
            }
        }
    }


}
