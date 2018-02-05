namespace SharePointPnP.PowerShell.Core.Model
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class StorageEntity : ClientSideObject
    {
        public string Key {get;set;}

        [JsonProperty("Comment")]
        public string Comment { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Value")]
        public string Value { get; set; }
    }
}
