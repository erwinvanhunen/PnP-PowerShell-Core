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
using SharePointPnP.PowerShell.Core.Attributes;

namespace SharePointPnP.PowerShell.Core.Lists
{
    /// <summary>
    ///     A simple Cmdlet that outputs a greeting to the pipeline.
    /// </summary>
	[OutputType(typeof(List<List>))]
    [Cmdlet(VerbsCommon.Get, "List")]
     [CmdletHelp(VerbsCommon.Get,"List","Returns a List object",
        Category = CmdletHelpCategory.Lists,
        OutputType = typeof(List),
        OutputTypeLink = "https://msdn.microsoft.com/en-us/library/microsoft.sharepoint.client.list.aspx")]
    [CmdletExample(
        Code = "PS:> Get-PnPList",
        Remarks = "Returns all lists in the current web",
        SortOrder = 1)]
    [CmdletExample(
        Code = "PS:> Get-PnPList -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe",
        Remarks = "Returns a list with the given id.",
        SortOrder = 2)]
    [CmdletExample(
        Code = "PS:> Get-PnPList -Identity Lists/Announcements",
        Remarks = "Returns a list with the given url.",
        SortOrder = 3)]
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