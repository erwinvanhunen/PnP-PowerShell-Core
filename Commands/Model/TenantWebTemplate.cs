using System;
using System.Collections.Generic;
using System.Text;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class TenantWebTemplate
    {
        public uint CompatibilityLevel { get; set; }
        public string Description { get; set; }
        public string DisplayCategory { get; set; }
        public int Id { get; set; }
        public uint Lcid { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
    }
}
