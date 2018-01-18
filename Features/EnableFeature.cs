using System.Management.Automation;
using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Enums;
using SharePointPnP.PowerShell.Core.Model;

[Cmdlet(VerbsLifecycle.Enable, "Feature")]
    [CmdletHelp("Enables a feature", Category = CmdletHelpCategory.Features)]
    [CmdletExample(
        Code = "PS:> Enable-PnPFeature -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe", 
        Remarks = @"This will enable the feature with the id ""99a00f6e-fb81-4dc7-8eac-e09c6f9132fe""", 
        SortOrder = 1)]
    [CmdletExample(
        Code = "PS:> Enable-PnPFeature -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe -Scope Site",
        Remarks = @"This will enable the feature with the id ""99a00f6e-fb81-4dc7-8eac-e09c6f9132fe"" with the site scope.",  
        SortOrder = 3)]
    public class EnableFeature : PnPCmdlet
    {
        [Parameter(Mandatory = true, Position=0, ValueFromPipeline=true, HelpMessage = "The id of the feature to enable.")]
        public GuidPipeBind Identity;

        [Parameter(Mandatory = false, HelpMessage = "Specify the scope of the feature to activate, either Web or Site. Defaults to Web.")]
        public FeatureScope Scope = FeatureScope.Web;

        protected override void ExecuteCmdlet()
        {
            var featureId = Identity.Id;
            if(Scope == FeatureScope.Web)
            {
                new RestRequest($"Web/Features/Add(guid'{featureId}')").Post();             
            }
            else
            {
                new RestRequest($"Site/Features/Add(guid'{featureId}')").Post();             
            }
        }
    }