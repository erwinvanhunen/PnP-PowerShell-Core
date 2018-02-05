using System;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class Feature : ClientSideObject
    {
        public Guid DefinitionId { get; set; }
        public string DisplayName { get; set; }
    }
}