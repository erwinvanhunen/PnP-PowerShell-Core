using System.Management.Automation;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Net;
using SharePointPnP.PowerShell.Core.Model;

namespace SharePointPnP.PowerShell.Core.Base
{
    /// <summary>
    ///     A simple Cmdlet that outputs a greeting to the pipeline.
    /// </summary>
	[OutputType(typeof(string))]
    [Cmdlet("Invoke", "RestRequest")]
    public class ExecuteRequest : PnPCmdlet
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
                WriteObject(ExecuteGetRequest(EndPoint, Select, Filter, Expand));
            } else {
                new RestRequest(EndPoint,Filter).Select(Select).Expand(Expand).Post(Content);
            }
        }
    }
}