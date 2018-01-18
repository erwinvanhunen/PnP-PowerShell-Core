using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class ResponseCollection<T>
    {
        [JsonProperty("value")]
        public List<T> Items { get; set; }
    }
}