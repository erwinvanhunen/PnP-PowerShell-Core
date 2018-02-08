namespace SharePointPnP.PowerShell.Core.Model
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class File : ClientSideObject
    {
        [JsonProperty("ListItemAllFields@odata.navigationLinkUrl")]
        public string ListItemAllFieldsOdataNavigationLinkUrl { get; set; }

        [JsonProperty("ListItemAllFields")]
        public ListItemAllFields ListItemAllFields { get; set; }

        [JsonProperty("CheckInComment")]
        public string CheckInComment { get; set; }

        [JsonProperty("CheckOutType")]
        public long CheckOutType { get; set; }

        [JsonProperty("ContentTag")]
        public string ContentTag { get; set; }

        [JsonProperty("CustomizedPageStatus")]
        public long CustomizedPageStatus { get; set; }

        [JsonProperty("ETag")]
        public string ETag { get; set; }

        [JsonProperty("Exists")]
        public bool Exists { get; set; }

        [JsonProperty("IrmEnabled")]
        public bool IrmEnabled { get; set; }

        [JsonProperty("Length")]
        public string Length { get; set; }

        [JsonProperty("Level")]
        public long Level { get; set; }

        [JsonProperty("LinkingUri")]
        public object LinkingUri { get; set; }

        [JsonProperty("LinkingUrl")]
        public string LinkingUrl { get; set; }

        [JsonProperty("MajorVersion")]
        public long MajorVersion { get; set; }

        [JsonProperty("MinorVersion")]
        public long MinorVersion { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("ServerRelativeUrl")]
        public string ServerRelativeUrl { get; set; }

        [JsonProperty("TimeCreated")]
        public System.DateTime TimeCreated { get; set; }

        [JsonProperty("TimeLastModified")]
        public System.DateTime TimeLastModified { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("UIVersion")]
        public long UiVersion { get; set; }

        [JsonProperty("UIVersionLabel")]
        public string UiVersionLabel { get; set; }

        [JsonProperty("UniqueId")]
        public string UniqueId { get; set; }
    }

    public class ListItemAllFields : ClientSideObject
    {
        [JsonExtensionData]
        public Dictionary<string, object> FieldValues { get; set; }

        [JsonProperty("ID")]
        public long Id { get; set; }

        [JsonProperty("Created")]
        public System.DateTime Created { get; set; }

        [JsonProperty("AuthorId")]
        public long AuthorId { get; set; }

        [JsonProperty("Modified")]
        public System.DateTime Modified { get; set; }

        [JsonProperty("EditorId")]
        public long EditorId { get; set; }

        [JsonProperty("CheckoutUserId")]
        public long? CheckoutUserId { get; set; }

        [JsonProperty("GUID")]
        public Guid Guid { get; set; }
    }
}
