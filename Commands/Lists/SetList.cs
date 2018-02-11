﻿using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Enums;
using SharePointPnP.PowerShell.Core.Model;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace SharePointPnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Set, "List")]
    [CmdletHelp(VerbsCommon.Set,"List","Updates list settings",
         Category = CmdletHelpCategory.Lists)]
    [CmdletExample(
         Code = @"Set-PnPList -Identity ""Demo List"" -EnableContentTypes $true",
         Remarks = "Switches the Enable Content Type switch on the list",
         SortOrder = 1)]
    [CmdletExample(
         Code = @"Set-PnPList -Identity ""Demo List"" -Hidden $true",
         Remarks = "Hides the list from the SharePoint UI.",
         SortOrder = 2)]
    [CmdletExample(
         Code = @"Set-PnPList -Identity ""Demo List"" -EnableVersioning $true",
         Remarks = "Turns on major versions on a list",
         SortOrder = 3)]
    [CmdletExample(
         Code = @"Set-PnPList -Identity ""Demo List"" -EnableVersioning $true -MajorVersions 20",
         Remarks = "Turns on major versions on a list and sets the maximum number of Major Versions to keep to 20.",
         SortOrder = 4)]
    [CmdletExample(
         Code = @"Set-PnPList -Identity ""Demo Library"" -EnableVersioning $true -EnableMinorVersions $true -MajorVersions 20 -MinorVersions 5",
         Remarks = "Turns on major versions on a document library and sets the maximum number of Major versions to keep to 20 and sets the maximum of Minor versions to 5.",
         SortOrder = 5)]
    public class SetList : PnPCmdlet
    {
        [Parameter(Mandatory = true, HelpMessage = "The ID, Title or Url of the list.")]
        public ListPipeBind Identity;

        [Parameter(Mandatory = false,
             HelpMessage = "Set to $true to enable content types, set to $false to disable content types")]
        public bool
            EnableContentTypes;

        [Parameter(Mandatory = false, HelpMessage = "If used the security inheritance is broken for this list")]
        public
            SwitchParameter BreakRoleInheritance;

        [Parameter(Mandatory = false, HelpMessage = "If used the roles are copied from the parent web")]
        public
            SwitchParameter CopyRoleAssignments;

        [Parameter(Mandatory = false,
             HelpMessage =
                 "If used the unique permissions are cleared from child objects and they can inherit role assignments from this object"
         )]
        public SwitchParameter ClearSubscopes;

        [Parameter(Mandatory = false, HelpMessage = "The title of the list")]
        public string Title = string.Empty;

        [Parameter(Mandatory = false, HelpMessage = "Hide the list from the SharePoint UI. Set to $true to hide, $false to show.")]
        public bool Hidden;

        [Parameter(Mandatory = false, HelpMessage = "Enable or disable versioning. Set to $true to enable, $false to disable.")]
        public bool EnableVersioning;

        [Parameter(Mandatory = false, HelpMessage = "Enable or disable minor versions versioning. Set to $true to enable, $false to disable.")]
        public bool EnableMinorVersions;

        [Parameter(Mandatory = false, HelpMessage = "Maximum major versions to keep")]
        public uint MajorVersions = 10;

        [Parameter(Mandatory = false, HelpMessage = "Maximum minor versions to keep")]
        public uint MinorVersions = 10;

        protected override void ExecuteCmdlet()
        {
            var list = Identity.GetList(Context);
            var listDict = new Dictionary<string, object>();
            if (list != null)
            {
                var batch = new BatchRequest(Context);
                if (BreakRoleInheritance)
                {
                    batch.Post($"{list.ObjectPath}/BreakRoleInheritance(CopyRoleAssignments={(CopyRoleAssignments.ToBool()).ToString().ToLower()}, clearSubscopes={(ClearSubscopes.ToBool()).ToString().ToLower()})");
                }

                if (!string.IsNullOrEmpty(Title))
                {
                    listDict.Add("Title", Title);
                }

                if (MyInvocation.BoundParameters.ContainsKey("Hidden") && Hidden != list.Hidden)
                {
                    listDict.Add("Hidden", Hidden);
                }

                if (MyInvocation.BoundParameters.ContainsKey("EnableContentTypes") && list.ContentTypesEnabled != EnableContentTypes)
                {
                    listDict.Add("ContentTypesEnabled", EnableContentTypes);
                }



                if (MyInvocation.BoundParameters.ContainsKey("EnableVersioning") && EnableVersioning != list.EnableVersioning)
                {
                    listDict.Add("EnableVersioning", EnableVersioning);
                }

                if (MyInvocation.BoundParameters.ContainsKey("EnableMinorVersions") && EnableMinorVersions != list.EnableMinorVersions)
                {
                    listDict.Add("EnableMinorVersions", EnableMinorVersions);
                }

                if (list.EnableVersioning)
                {
                    // list or doclib?

                    if (list.BaseType == BaseType.DocumentLibrary)
                    {
                        if (MyInvocation.BoundParameters.ContainsKey("MajorVersions"))
                        {
                            listDict.Add("MajorVersionLimit", (int)MajorVersions);
                        }

                        if (MyInvocation.BoundParameters.ContainsKey("MinorVersions") && list.EnableMinorVersions)
                        {
                            listDict.Add("MajorWithMinorVersionsLimit", (int)MinorVersions);
                        }
                    }
                    else
                    {
                        if (MyInvocation.BoundParameters.ContainsKey("MajorVersions"))
                        {
                            listDict.Add("MajorVersionLimit", (int)MajorVersions);
                        }
                    }
                }

                if (listDict.Any())
                {
                    batch.Merge(list.ObjectPath, new MetadataType("SP.List"), listDict);
                }

                batch.Execute();
            }
        }
    }
}