using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Model;
using System.Management.Automation;
using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Enums;
using System.Collections.Generic;

namespace SharePointPnP.PowerShell.Core.Branding
{
    [Cmdlet(VerbsCommon.Get, "Feature")]
    [CmdletHelp(VerbsCommon.Get, "Feature", "Returns all activated or a specific activated feature",
       Category = CmdletHelpCategory.Features,
       OutputType = typeof(IEnumerable<Feature>),
       OutputTypeLink = "https://msdn.microsoft.com/en-us/library/microsoft.sharepoint.client.feature.aspx")]
    [CmdletExample(
    Code = @"PS:> Get-PnPFeature",
    Remarks = @"This will return all activated web scoped features", SortOrder = 1)]
    [CmdletExample(
    Code = @"PS:> Get-PnPFeature -Scope Site",
    Remarks = @"This will return all activated site scoped features", SortOrder = 2)]
    [CmdletExample(
    Code = @"PS:> Get-PnPFeature -Identity fb689d0e-eb99-4f13-beb3-86692fd39f22",
    Remarks = @"This will return a specific activated web scoped feature", SortOrder = 3)]
    [CmdletExample(
    Code = @"PS:> Get-PnPFeature -Identity fb689d0e-eb99-4f13-beb3-86692fd39f22 -Scope Site",
    Remarks = @"This will return a specific activated site scoped feature", SortOrder = 4)]

    public class GetFeature : PnPCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, HelpMessage = "The feature ID or name to query for, Querying by name is not supported in version 15 of the Client Side Object Model")]
        public GuidPipeBind Identity;

        [Parameter(Mandatory = false, HelpMessage = "The scope of the feature. Defaults to Web.")]
        public FeatureScope Scope = FeatureScope.Web;

        protected override void ExecuteCmdlet()
        {
            var baseUrl = "";

            if (MyInvocation.BoundParameters.ContainsKey("Identity"))
            {
                baseUrl = $"{(Scope == FeatureScope.Web ? "Web" : "Site")}/Features/GetByGuid(guid'{Identity.Id}')";
            }
            else
            {
                baseUrl = $"{(Scope == FeatureScope.Web ? "Web" : "Site")}/Features";
            }

            WriteObject(new RestRequest(Context, baseUrl).Expand("DisplayName").Get<ResponseCollection<Feature>>().Items, true);
        }

    }
}
