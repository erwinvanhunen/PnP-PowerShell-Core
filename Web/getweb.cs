using SharePointPnP.PowerShell.Core.Model;
using System.Management.Automation;
using SharePointPnP.PowerShell.Core.Base;

namespace SharePointPnP.PowerShell.Core.Web
{
    /// <summary>
    ///     A simple Cmdlet that outputs a greeting to the pipeline.
    /// </summary>
	[OutputType(typeof(string))]
    [Cmdlet(VerbsCommon.Get, "Web")]
    public class GetWeb : PnPCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            WriteObject(new RestRequest("Web").Get<Model.Web>());
        }
    }
}