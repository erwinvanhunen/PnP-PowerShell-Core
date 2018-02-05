using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Enums;
using SharePointPnP.PowerShell.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace SharePointPnP.PowerShell.Core.Branding
{
    [Cmdlet(VerbsCommon.Remove, "CustomAction", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
    [CmdletHelp(VerbsCommon.Remove,"CustomAction","Removes a custom action",
        Category = CmdletHelpCategory.Branding)]
    [CmdletExample(Code = @"PS:> Remove-PnPCustomAction -Identity aa66f67e-46c0-4474-8a82-42bf467d07f2",
                   Remarks = @"Removes the custom action with the id 'aa66f67e-46c0-4474-8a82-42bf467d07f2'.",
                   SortOrder = 1)]
    [CmdletExample(Code = @"PS:> Remove-PnPCustomAction -Identity aa66f67e-46c0-4474-8a82-42bf467d07f2 -Scope web",
                   Remarks = @"Removes the custom action with the id 'aa66f67e-46c0-4474-8a82-42bf467d07f2' from the current web.",
                   SortOrder = 2)]
    [CmdletExample(Code = @"PS:> Remove-PnPCustomAction -Identity aa66f67e-46c0-4474-8a82-42bf467d07f2 -Force",
                   Remarks = @"Removes the custom action with the id 'aa66f67e-46c0-4474-8a82-42bf467d07f2' without asking for confirmation.",
                   SortOrder = 3)]
    [CmdletExample(Code = @"PS:> Get-PnPCustomAction -Scope All | ? Location -eq ScriptLink | Remove-PnPCustomAction",
                   Remarks = @"Removes all custom actions that are ScriptLinks",
                   SortOrder = 4)]
    public class RemoveCustomAction : PnPCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, HelpMessage = "The id or name of the CustomAction that needs to be removed or a CustomAction instance itself")]
        public UserCustomActionPipeBind Identity;

        [Parameter(Mandatory = false, HelpMessage = "Define if the CustomAction is to be found at the web or site collection scope. Specify All to allow deletion from either web or site collection.")]
        public CustomActionScope Scope = CustomActionScope.Web;

        [Parameter(Mandatory = false, HelpMessage = "Use the -Force flag to bypass the confirmation question")]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            List<UserCustomAction> actions = new List<UserCustomAction>();

            if (Identity != null && Identity.UserCustomAction != null)
            {
                actions.Add(Identity.UserCustomAction);
            }
            else
            {
                if (Scope == CustomActionScope.All || Scope == CustomActionScope.Web)
                {

                    actions.AddRange(new RestRequest("Web/UserCustomActions").Get<ResponseCollection<UserCustomAction>>().Items);
                }
                if (Scope == CustomActionScope.All || Scope == CustomActionScope.Site)
                {
                    actions.AddRange(new RestRequest("Site/UserCustomActions").Get<ResponseCollection<UserCustomAction>>().Items);
                }

                if (Identity != null)
                {
                    actions = actions.Where(action => Identity.Id.HasValue ? Identity.Id.Value == action.Id : Identity.Name == action.Name).ToList();

                    if (!actions.Any())
                    {
                        throw new PSArgumentException($"No CustomAction found with the {(Identity.Id.HasValue ? "Id" : "name")} '{(Identity.Id.HasValue ? Identity.Id.Value.ToString() : Identity.Name)}' within the scope '{Scope}'", "Identity");
                    }
                }

                if (!actions.Any())
                {
                    WriteVerbose($"No CustomAction found within the scope '{Scope}'");
                    return;
                }
            }

            foreach (var action in actions.Where(action => Force || (MyInvocation.BoundParameters.ContainsKey("Confirm") && !bool.Parse(MyInvocation.BoundParameters["Confirm"].ToString())) || ShouldContinue($"Remove custom action {action.Name} with id {action.Id} on scope {action.Scope}", "Confirm")))
            {
                switch (action.Scope)
                {
                    case CustomActionScope.Web:
                        new RestRequest($"Web/UserCustomActions(guid'{action.Id}')").Delete();
                        break;

                    case CustomActionScope.Site:
                        new RestRequest($"Site/UserCustomActions(guid'{action.Id}')").Delete();
                        break;
                }
            }
        }
    }
}
