using SharePointPnP.PowerShell.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharePointPnP.PowerShell.Core.Model
{
    public interface IClientSideObjectCustomSerializer
    {
        string GetJson();
    }
}
