using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class ResponseCollection<T> : ClientSideObject
    {
        [JsonProperty("value")]
        public List<T> Items { get; set; }

        [JsonProperty("odata.nextLink")]
        public string NextLink { get; set; }
    }
}