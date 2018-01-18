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

namespace SharePointPnP.PowerShell.Core.Lists
{
    /// <summary>
    ///     A simple Cmdlet that outputs a greeting to the pipeline.
    /// </summary>
	[OutputType(typeof(List<List>))]
    [Cmdlet(VerbsCommon.Get, "List")]
    public class GetList : PnPCmdlet
    {
          [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0, HelpMessage = "The ID, name or Url (Lists/MyList) of the list.")]
        public ListPipeBind Identity;
        protected override void ExecuteCmdlet()
        {
            if(MyInvocation.BoundParameters.ContainsKey("Identity"))
            {
                WriteObject(Identity.GetList());
            } else {
                WriteObject(new RestRequest("Lists").Expand("RootFolder/ServerRelativeUrl","OnQuickLaunch","DefaultViewUrl").Get<ResponseCollection<List>>().Items,true);
                //WriteObject(ExecuteGetRequest<ListCollection>("Lists", expand:"RootFolder/ServerRelativeUrl,OnQuickLaunch,DefaultViewUrl").value,true);
            }
        }
    }
}