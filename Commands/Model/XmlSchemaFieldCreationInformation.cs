using System;
using System.Collections.Generic;
using System.Text;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class XmlSchemaFieldCreationInformation : ClientSideObject
    {
        public string SchemaXml { get; set; }

        public XmlSchemaFieldCreationInformation() : base("SP.XmlSchemaFieldCreationInformation")
        { }
    }
}
