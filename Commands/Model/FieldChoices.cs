using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class FieldChoices : ClientSideObject
    {
        [JsonProperty("results")]
        public string[] Results { get; set; }

        public FieldChoices() : base("Collection(Edm.String)")
        {
        }
    }
}
