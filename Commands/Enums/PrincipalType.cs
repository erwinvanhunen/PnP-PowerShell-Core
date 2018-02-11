using System;
using System.Collections.Generic;
using System.Text;

namespace SharePointPnP.PowerShell.Core.Enums
{
    public enum PrincipalType
    {
        None = 0,
        User = 1,
        DistributionList = 2,
        SecurityGroup = 4,
        SharePointGroup = 8,
        All = 15
    }
}
