using System;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class Folder
    {
        public bool Exists { get; set; }
        public bool IsWOPIEnabled { get; set; }
        public int ItemCount { get; set; }
        public string Name { get; set; }
        public object ProgID { get; set; }
        public string ServerRelativeUrl { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime TimeLastModified { get; set; }
        public string UniqueId { get; set; }
        public string WelcomePage { get; set; }
    }
}