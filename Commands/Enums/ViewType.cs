using System;
using System.Collections.Generic;
using System.Text;

namespace SharePointPnP.PowerShell.Core.Enums
{
    public enum ViewType
    {
        None = 0,
        Html = 1,
        Grid = 2048,
        Recurrence = 8193,
        Chart = 131072,
        Calendar = 524288,
        Gantt = 67108864
    }
}
