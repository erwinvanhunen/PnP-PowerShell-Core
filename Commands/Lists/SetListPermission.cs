using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Model;
using System;
using System.Linq;
using System.Management.Automation;

namespace SharePointPnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Set, "ListPermission")]
    [CmdletHelp(VerbsCommon.Set, "ListPermission", "Sets list permissions",
        Category = CmdletHelpCategory.Lists)]
    [CmdletExample(
        Code = "PS:> Set-PnPListPermission -Identity 'Documents' -User 'user@contoso.com' -AddRole 'Contribute'",
        Remarks = "Adds the 'Contribute' permission to the user 'user@contoso.com' for the list 'Documents'",
        SortOrder = 1)]
    [CmdletExample(
        Code = "PS:> Set-PnPListPermission -Identity 'Documents' -User 'user@contoso.com' -RemoveRole 'Contribute'",
        Remarks = "Removes the 'Contribute' permission to the user 'user@contoso.com' for the list 'Documents'",
        SortOrder = 2)]
    public class SetListPermission : PnPCmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = ParameterAttribute.AllParameterSets, HelpMessage = "The ID or Title of the list.")]
        public ListPipeBind Identity;

        [Parameter(Mandatory = true, ParameterSetName = "Group")]
        public GroupPipeBind Group;

        [Parameter(Mandatory = true, ParameterSetName = "User")]
        public string User;

        [Parameter(Mandatory = false, HelpMessage = "The role that must be assigned to the group or user")]
        public string AddRole = string.Empty;

        [Parameter(Mandatory = false, HelpMessage = "The role that must be removed from the group or user")]
        public string RemoveRole = string.Empty;

        protected override void ExecuteCmdlet()
        {
            var list = Identity.GetList(Context);

            if (list != null)
            {
                Principal principal = null;
                if (ParameterSetName == "Group")
                {
                    principal = Group.GetGroup(Context);
                }
                else
                {
                    principal = new RestRequest(Context, $"Web/EnsureUser('{User}')").Post<User>();
                }
                if (principal != null)
                {
                    if (!string.IsNullOrEmpty(AddRole))
                    {
                        var roleDefinition = new RestRequest(Context, $"Web/RoleDefinitions/GetByName('{AddRole}')").Get<RoleDefinition>();
                        new RestRequest(Context, $"{list.ObjectPath}/RoleAssignments/AddRoleAssignment(principalid={principal.Id},roledefid={roleDefinition.Id})").Post();
                    }
                    if (!string.IsNullOrEmpty(RemoveRole))
                    {
                        var roleDefinitionBindings = new RestRequest(Context, $"{list.ObjectPath}/RoleAssignments/GetByPrincipalId({principal.Id})/RoleDefinitionBindings").Get<ResponseCollection<RoleDefinitionBinding>>().Items;
                        foreach (var roleDefinition in roleDefinitionBindings.Where(roleDefinition => roleDefinition.Name == RemoveRole))
                        {
                            new RestRequest(Context, roleDefinition.ObjectPath).Delete();
                        }
                    }
                }
                else
                {
                    WriteError(new ErrorRecord(new Exception("Principal not found"), "1", ErrorCategory.ObjectNotFound, null));
                }
            }
        }
    }
}
