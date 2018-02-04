using Newtonsoft.Json;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class BasePermissions
    {
        [JsonProperty("High")]
        private uint _high { get; set; }

        [JsonProperty("Low")]
        private uint _low { get; set; }

        [JsonIgnore]
        public uint High
        {
            get
            {
                return _high;
            }
        }


        [JsonIgnore]
        public uint Low
        {
            get
            {
                return _low;
            }
        }

        public bool Has(PermissionKind perm)
        {
            if (perm == PermissionKind.EmptyMask)
            {
                return true;
            }
            if (perm == PermissionKind.FullMask)
            {
                return (this._high & 32767u) == 32767u && this._low == 65535u;
            }
            int num = perm - PermissionKind.ViewListItems;
            uint num2 = 1u;
            if (num >= 0 && num < 32)
            {
                num2 <<= num;
                return 0u != (this._low & num2);
            }
            if (num >= 32 && num < 64)
            {
                num2 <<= num - 32;
                return 0u != (this._high & num2);
            }
            return false;
        }

    }
}