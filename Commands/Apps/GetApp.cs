using System.Management.Automation;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Net;
using SharePointPnP.PowerShell.Core.Model;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Helpers;
using SharePointPnP.PowerShell.Core.Attributes;

namespace SharePointPnP.PowerShell.Core.Apps
{
    /// <summary>
    ///     A simple Cmdlet that outputs a greeting to the pipeline.
    /// </summary>
	[OutputType(typeof(List<AppMetadata>))]
    [Cmdlet(VerbsCommon.Get, "App")]
    [CmdletHelp(VerbsCommon.Get,"App","Returns the available apps from the app catalog",
        Category = CmdletHelpCategory.Apps,
        OutputType = typeof(List<AppMetadata>), SupportedPlatform = CmdletSupportedPlatform.Online)]
    [CmdletExample(Code = @"PS:> Get-PnPApp", Remarks = @"This will return all available app metadata from the tenant app catalog. It will list the installed version in the current site.", SortOrder = 1)]
    [CmdletExample(Code = @"PS:> Get-PnPApp -Identity 2646ccc3-6a2b-46ef-9273-81411cbbb60f", Remarks = @"This will the specific app metadata from the app catalog.", SortOrder = 2)]
    public class GetApp : PnPCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0, HelpMessage = "The ID, name or Url (Lists/MyList) of the list.")]
        public GuidPipeBind Identity;
        protected override void ExecuteCmdlet()
        {
            if (MyInvocation.BoundParameters.ContainsKey("Identity"))
            {
                //WriteObject(Identity.GetList());
            }
            else
            {
                AppManager mgr = new AppManager(Context);
                WriteObject(mgr.GetAvailable());
            }
        }
    }
}