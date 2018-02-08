using System.Management.Automation;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Net;
using SharePointPnP.PowerShell.Core.Model;
using SharePointPnP.PowerShell.Core.Attributes;

namespace SharePointPnP.PowerShell.Core.Base
{
    /// <summary>
    ///     A simple Cmdlet that outputs a greeting to the pipeline.
    /// </summary>
	[OutputType(typeof(string))]
    [Cmdlet(VerbsCommunications.Send, "RestRequest")]
    [CmdletHelp(VerbsCommunications.Send, "RestRequest", "Executes a REST request")]
    public class SendRestRequest : PnPCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string EndPoint;

        [Parameter(Mandatory = false)]
        public string Select;

        [Parameter(Mandatory = false)]
        public string Filter;

        [Parameter(Mandatory = false)]
        public string Expand;

        [Parameter(Mandatory = false)]
        public HttpMethod Method = HttpMethod.Get;

        [Parameter(Mandatory = false)]
        public string Content;
        protected override void ExecuteCmdlet()
        {
            if (Method == HttpMethod.Get)
            {
                WriteObject(ExecuteGetRequest(CurrentContext, EndPoint, Select, Filter, Expand));
            }
            else
            {
                new RestRequest(CurrentContext, EndPoint).Filter(Filter).Select(Select).Expand(Expand).Post(Content);
            }
        }
    }
}