using System;
using System.Collections.Generic;
using System.Text;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class FieldLink : ClientSideObject
    {
        public object FieldInternalName { get; set; }
        public bool Hidden { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Required { get; set; }

        public FieldLink() : base("SP.FieldLink")
        { }
    }
}
