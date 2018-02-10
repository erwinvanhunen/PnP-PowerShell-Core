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

        [JsonProperty("ServerRedirectedEmbedUri")]
        public object ServerRedirectedEmbedUri { get; set; }

        [JsonProperty("ServerRedirectedEmbedUrl")]
        public string ServerRedirectedEmbedUrl { get; set; }

        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonExtensionData]
        public Dictionary<string, object> FieldValues { get; set; }
    }
}
