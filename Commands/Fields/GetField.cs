using System;
using System.Linq;
using System.Management.Automation;
using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Model;

namespace SharePointPnP.PowerShell.Core.Fields
{
    [Cmdlet(VerbsCommon.Get, "Field")]
    [CmdletHelp(VerbsCommon.Get, "Field", "Returns a field from a list or site",
        Category = CmdletHelpCategory.Fields,
        OutputType = typeof(Field),
        OutputTypeLink = "https://msdn.microsoft.com/en-us/library/microsoft.sharepoint.client.field.aspx")]
    [CmdletExample(
        Code = @"PS:> Get-PnPField",
        Remarks = @"Gets all the fields from the current site",
        SortOrder = 1)]
    [CmdletExample(
        Code = @"PS:> Get-PnPField -List ""Demo list"" -Identity ""Speakers""",
        Remarks = @"Gets the speakers field from the list Demo list",
        SortOrder = 2)]
    public class GetField : PnPCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true, HelpMessage = "The list object or name where to get the field from")]
        public ListPipeBind List;

        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, HelpMessage = "The field object or name to get")]
        public FieldPipeBind Identity = new FieldPipeBind();

        [Parameter(Mandatory = false, HelpMessage = "Filter to the specified group")]
        public string Group;

        protected override void ExecuteCmdlet()
        {
            string groupFilter = null;
            var expands = new string[] { "ClientSideComponentProperties", "CustomFormatter", "OutputType" };
            if (!string.IsNullOrEmpty(Group))
            {
                groupFilter = $"tolower(Group) eq '{Group.ToLower()}'";
            }
            if (List != null)
            {
                var list = List.GetList(Context);

                if (list != null)
                {
                    if (Identity.Id != Guid.Empty)
                    {
                        WriteObject(new RestRequest(Context, $"Web/Lists(guid'{list.Id.ToString("D")}')/Fields(guid'{Identity.Id}')").Filter(groupFilter).Expand(expands).Get<Field>());
                    }
                    else if (!string.IsNullOrEmpty(Identity.Name))
                    {
                        WriteObject(new RestRequest(Context, $"Web/Lists(guid'{list.Id.ToString("D")}')/Fields/GetByInternalNameOrTitle('{Identity.Name}')").Filter(groupFilter).Expand(expands).Get<Field>());
                    }
                    else
                    {
                        WriteObject(new RestRequest(Context, $"Web/Lists(guid'{list.Id.ToString("D")}')/Fields").Filter(groupFilter).Expand(expands).Get<ResponseCollection<Field>>().Items, true);
                    }
                }

            }
            else
            {

                if (Identity.Id == Guid.Empty && string.IsNullOrEmpty(Identity.Name))
                {
                    WriteObject(new RestRequest(Context, $"Web/Fields").Filter(groupFilter).Expand(expands).Get<ResponseCollection<Field>>().Items, true);
                }
                else
                {
                    if (Identity.Id != Guid.Empty)
                    {
                        WriteObject(new RestRequest(Context, $"Web/Fields(guid'{Identity.Id}')").Filter(groupFilter).Expand(expands).Get<Field>());

                    }
                    else if (!string.IsNullOrEmpty(Identity.Name))
                    {
                        WriteObject(new RestRequest(Context, $"Web/Fields/GetByInternalNameOrTitle('{Identity.Name}')").Filter(groupFilter).Expand(expands).Get<Field>());
                    }
                }
            }

        }
    }

}
