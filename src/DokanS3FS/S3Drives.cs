using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DokanS3FS
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class mount
    {

        private mountDrive[]? drivesField;

        private string? libPathField;

        private byte? threadsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("drive", IsNullable = false)]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string? libPath
        {
            get
            {
                return this.libPathField;
            }
            set
            {
                this.libPathField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte? threads
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
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class mountDrive
    {

        private string? schemaField;

        private string? userNameField;

        private string? rootField;

        private string? encryptionKeyField;

        private ushort? timeoutField;

        private string? parametersField;

        private string? apiKeyField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string? schema
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string? userName
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
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string? root
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
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string? encryptionKey
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
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort? timeout
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
        [System.Xml.Serialization.XmlAttributeAttribute()]
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
        [System.Xml.Serialization.XmlAttributeAttribute()]
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
