using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DokanS3FS
{
    public class S3ConfigException : Exception
    {
        public S3ConfigException(string msg) : base(msg) { }
    }
}
