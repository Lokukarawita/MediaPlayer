using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoInc.Net.UPnP.XML.DIDLLite
{
    [Serializable]
    public class DIDLLiteXmlException : Exception
    {
        public DIDLLiteXmlException() { }
        public DIDLLiteXmlException(string message) : base(message) { }
        public DIDLLiteXmlException(string message, Exception inner) : base(message, inner) { }
        protected DIDLLiteXmlException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
