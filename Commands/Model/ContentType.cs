using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class ContentTypeId
    {
        public string StringValue { get; set; }
    }
    public class ContentType : ClientSideObject
    {
        public ContentType() : base("SP.ContentType")
        { }

        public List<FieldLink> FieldLinks { get; set; }

        public string Description { get; set; }
        public string DisplayFormTemplateName { get; set; }
        public string DisplayFormUrl { get; set; }
        public string DocumentTemplate { get; set; }
        public string DocumentTemplateUrl { get; set; }
        public string EditFormTemplateName { get; set; }
        public string EditFormUrl { get; set; }
        public string Group { get; set; }
        public bool Hidden { get; set; }
        public ContentTypeId Id { get; set; }
        public string JSLink { get; set; }
        public string MobileDisplayFormUrl { get; set; }
        public string MobileEditFormUrl { get; set; }
        public string MobileNewFormUrl { get; set; }
        public string Name { get; set; }
        public string NewFormTemplateName { get; set; }
        public string NewFormUrl { get; set; }
        public bool ReadOnly { get; set; }
        public string SchemaXml { get; set; }
        public string Scope { get; set; }
        public bool Sealed { get; set; }
        public string StringId { get; set; }
    }
}
