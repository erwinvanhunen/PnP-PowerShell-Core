using System;
using System.Management.Automation;
using System.Collections;
using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Model;
using System.Collections.Generic;

namespace SharePointPnP.PowerShell.Core.Fields
{
    [Cmdlet(VerbsCommon.Set, "Field")]
    [CmdletHelp(VerbsCommon.Set, "Field","Changes one or more properties of a field in a specific list or for the whole web",
        Category = CmdletHelpCategory.Fields,
        OutputType = typeof(Field),
        OutputTypeLink = "https://msdn.microsoft.com/en-us/library/microsoft.sharepoint.client.field.aspx")]
    [CmdletExample(
        Code = @"PS:> Set-PnPField -Identity AssignedTo -Values @{JSLink=""customrendering.js"";Group=""My fields""}",
        Remarks = @"Updates the AssignedTo field on the current web to use customrendering.js for the JSLink and sets the group name the field is categorized in to ""My Fields"". Lists that are already using the AssignedTo field will not be updated.",
        SortOrder = 1)]
    [CmdletExample(
        Code = @"PS:> Set-PnPField -Identity AssignedTo -Values @{JSLink=""customrendering.js"";Group=""My fields""} -UpdateExistingLists",
        Remarks = @"Updates the AssignedTo field on the current web to use customrendering.js for the JSLink and sets the group name the field is categorized in to ""My Fields"". Lists that are already using the AssignedTo field will also be updated.",
        SortOrder = 2)]
    [CmdletExample(
        Code = @"PS:> Set-PnPField -List ""Tasks"" -Identity ""AssignedTo"" -Values @{JSLink=""customrendering.js""}",
        Remarks = @"Updates the AssignedTo field on the Tasks list to use customrendering.js for the JSLink",
        SortOrder = 3)]
    public class SetField : PnPCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true, HelpMessage = "The list object, name or id where to update the field. If omited the field will be updated on the web.")]
        public ListPipeBind List;

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, HelpMessage = "The field object, internal field name (case sensitive) or field id to update")]
        public FieldPipeBind Identity;

        [Parameter(Mandatory = true, HelpMessage = "Hashtable of properties to update on the field. Use the syntax @{property1=\"value\";property2=\"value\"}.")]
        public Hashtable Values;

        [Parameter(Mandatory = false, HelpMessage = "If provided, the field will be updated on existing lists that use it as well. If not provided or set to $false, existing lists using the field will remain unchanged but new lists will get the updated field.")]
        public SwitchParameter UpdateExistingLists;

        protected override void ExecuteCmdlet()
        {
            Field field = null;
            if (MyInvocation.BoundParameters.ContainsKey("List"))
            {
                var list = List.GetList(Context);
                field = Identity.GetListField(Context, list);
            }
            else
            {
                field = Identity.GetWebField(Context);
            }

            var fieldDict = new Dictionary<string, object>();
            foreach (string key in Values.Keys)
            {
                var value = Values[key];

                var property = field.GetType().GetProperty(key);
                if (property == null)
                {
                    WriteWarning($"No property '{key}' found on this field. Value will be ignored.");
                }
                else
                {
                    try
                    {
                        property.SetValue(field, value);
                        fieldDict.Add(key, value);
                    }
                    catch (Exception e)
                    {
                        WriteWarning($"Setting property '{key}' to '{value}' failed with exception '{e.Message}'. Value will be ignored.");
                    }
                }
            }
            new RestRequest(Context, field.ObjectPath).Merge(new MetadataType("SP.Field"), fieldDict);
        }
    }
}