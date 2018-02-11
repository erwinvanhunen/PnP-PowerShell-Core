using Newtonsoft.Json;
using SharePointPnP.PowerShell.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class Principal : ClientSideObject
    {
        public Principal(string type) : base(type)
        { }

        [JsonProperty("Id")]
        private int _id { get; set; }

        [JsonProperty("IsHiddenInUI")]
        private bool _isHiddenInUI { get; set; }

        [JsonProperty("LoginName")]
        private string _loginName { get; set; }

        [JsonProperty("PrincipalType")]
        private int _principalType { get; set; }

        [JsonIgnore]
        public int Id
        {
            get
            {
                return _id;
            }
        }

        [JsonIgnore]
        public bool IsHiddenInUI
        {
            get
            {
                return _isHiddenInUI;
            }
        }

        [JsonIgnore]
        public string LoginName
        {
            get
            {
                return _loginName;
            }
        }

        [JsonProperty]
        public string Title { get; set; }
        
        [JsonIgnore]
        public PrincipalType PrincipalType
        {
            get
            {
                return (PrincipalType)_principalType;
            }
        }
    }
}
