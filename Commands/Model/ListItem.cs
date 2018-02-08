namespace SharePointPnP.PowerShell.Core.Model
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class ListItem : ClientSideObject
    {
        [JsonProperty("FieldValuesAsText")]
        public Dictionary<string, string> FieldValuesAsText { get; set; }

        [JsonProperty("FileSystemObjectType")]
        public long FileSystemObjectType { get; set; }

        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonProperty("ServerRedirectedEmbedUri")]
        public object ServerRedirectedEmbedUri { get; set; }

        [JsonProperty("ServerRedirectedEmbedUrl")]
        public string ServerRedirectedEmbedUrl { get; set; }

        [JsonProperty("ID")]
        public long ID { get; set; }

        [JsonProperty("ContentTypeId")]
        public string ContentTypeId { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Modified")]
        public System.DateTime Modified { get; set; }

        [JsonProperty("Created")]
        public System.DateTime Created { get; set; }

        [JsonProperty("AuthorId")]
        public long AuthorId { get; set; }

        [JsonProperty("EditorId")]
        public long EditorId { get; set; }

        [JsonProperty("Attachments")]
        public bool Attachments { get; set; }

        [JsonProperty("GUID")]
        public Guid Guid { get; set; }

        [JsonProperty("ComplianceAssetId")]
        public object ComplianceAssetId { get; set; }

        [JsonProperty("CommentsDisabled")]
        public bool CommentsDisabled { get; set; }

        [JsonExtensionData]
        public Dictionary<string, object> FieldValues { get; set; }
    }
}
