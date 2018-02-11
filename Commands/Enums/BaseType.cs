using System;
using System.Collections.Generic;
using System.Text;

namespace SharePointPnP.PowerShell.Core.Enums
{
    public enum BaseType
    {
        None = -1,
        GenericList = 0,
        DocumentLibrary = 1,
        Unused = 2,
        DiscussionBoard = 3,
        Survey = 4,
        Issue = 5
    }
}
