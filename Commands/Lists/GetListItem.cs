﻿using Newtonsoft.Json;
using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Xml.Linq;

namespace SharePointPnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Get, "ListItem", DefaultParameterSetName = ParameterSet_ALLITEMS)]
    [CmdletHelp(VerbsCommon.Get, "ListItem", "Retrieves list items",
        Category = CmdletHelpCategory.Lists)]
    [CmdletExample(
        Code = "PS:> Get-PnPListItem -List Tasks",
        Remarks = "Retrieves all list items from the Tasks list",
        SortOrder = 1)]
    [CmdletExample(
        Code = "PS:> Get-PnPListItem -List Tasks -Id 1",
        Remarks = "Retrieves the list item with ID 1 from from the Tasks list",
        SortOrder = 2)]
    [CmdletExample(
        Code = "PS:> Get-PnPListItem -List Tasks -UniqueId bd6c5b3b-d960-4ee7-a02c-85dc6cd78cc3",
        Remarks = "Retrieves the list item with unique id bd6c5b3b-d960-4ee7-a02c-85dc6cd78cc3 from from the tasks lists",
        SortOrder = 3)]
    [CmdletExample(
        Code = "PS:> (Get-PnPListItem -List Tasks -Fields \"Title\",\"GUID\").FieldValues",
        Remarks = "Retrieves all list items, but only includes the values of the Title and GUID fields in the list item object",
        SortOrder = 4)]
    [CmdletExample(
        Code = "PS:> Get-PnPListItem -List Tasks -Query \"<View><Query><Where><Eq><FieldRef Name='GUID'/><Value Type='Guid'>bd6c5b3b-d960-4ee7-a02c-85dc6cd78cc3</Value></Eq></Where></Query></View>\"",
        Remarks = "Retrieves all list items based on the CAML query specified",
        SortOrder = 5)]
    [CmdletExample(
        Code = "PS:> Get-PnPListItem -List Tasks -PageSize 1000",
        Remarks = "Retrieves all list items from the Tasks list in pages of 1000 items",
        SortOrder = 6)]
    [CmdletExample(
        Code = "PS:> Get-PnPListItem -List Tasks -PageSize 1000 -ScriptBlock { Param($items) $items.Context.ExecuteQuery() } | % { $_.BreakRoleInheritance($true, $true) }",
        Remarks = "Retrieves all list items from the Tasks list in pages of 1000 items and breaks permission inheritance on each item",
        SortOrder = 7)]
    public class GetListItem : PnPCmdlet
    {
        private const string ParameterSet_BYID = "By Id";
        private const string ParameterSet_BYUNIQUEID = "By Unique Id";
        private const string ParameterSet_BYQUERY = "By Query";
        private const string ParameterSet_ALLITEMS = "All Items";
        [Parameter(Mandatory = true, HelpMessage = "The list to query", Position = 0, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public ListPipeBind List;

        [Parameter(Mandatory = false, HelpMessage = "The ID of the item to retrieve", ParameterSetName = ParameterSet_BYID)]
        public int Id = -1;

        [Parameter(Mandatory = false, HelpMessage = "The unique id (GUID) of the item to retrieve", ParameterSetName = ParameterSet_BYUNIQUEID)]
        public GuidPipeBind UniqueId;

        [Parameter(Mandatory = false, HelpMessage = "The CAML query to execute against the list", ParameterSetName = ParameterSet_BYQUERY)]
        public string Query;


        [Parameter(Mandatory = false, HelpMessage = "The fields to retrieve. If not specified all fields will be loaded in the returned list object.", ParameterSetName = ParameterSet_ALLITEMS)]
        [Parameter(Mandatory = false, HelpMessage = "The fields to retrieve. If not specified all fields will be loaded in the returned list object.", ParameterSetName = ParameterSet_BYID)]
        [Parameter(Mandatory = false, HelpMessage = "The fields to retrieve. If not specified all fields will be loaded in the returned list object.", ParameterSetName = ParameterSet_BYUNIQUEID)]
        public string[] Fields;

        [Parameter(Mandatory = false, HelpMessage = "The number of items to retrieve per page request.", ParameterSetName = ParameterSet_ALLITEMS)]
        [Parameter(Mandatory = false, HelpMessage = "The number of items to retrieve per page request.", ParameterSetName = ParameterSet_BYQUERY)]
        public int PageSize = -1;

        [Parameter(Mandatory = false, HelpMessage = "The script block to run after every page request.", ParameterSetName = ParameterSet_ALLITEMS)]
        [Parameter(Mandatory = false, HelpMessage = "The script block to run after every page request.", ParameterSetName = ParameterSet_BYQUERY)]
        public ScriptBlock ScriptBlock;

        protected override void ExecuteCmdlet()
        {
            var list = List.GetList(Context);

            if (HasId())
            {
                var request = new RestRequest(Context, $"{list.ObjectPath}/Items({Id})");
                if (Fields != null)
                {
                    request = request.Select(Fields);
                }
                var listItem = request.Get<ListItem>();
                WriteObject(listItem);
            }
            else if (HasUniqueId())
            {
                var listItem = new RestRequest(Context, $"{list.ObjectPath}/Items").Filter($"GUID eq '{UniqueId.Id}'").Get<ResponseCollection<ListItem>>().Items.FirstOrDefault();
                WriteObject(listItem);
            }
            else
            {
                if (!string.IsNullOrEmpty(Query))
                {
                    CamlQuery query = HasCamlQuery() ? new CamlQuery { ViewXml = Query } : CamlQuery.CreateAllItemsQuery();

                    if (Fields != null)
                    {
                        var queryElement = XElement.Parse(query.ViewXml);

                        var viewFields = queryElement.Descendants("ViewFields").FirstOrDefault();
                        if (viewFields != null)
                        {
                            viewFields.RemoveAll();
                        }
                        else
                        {
                            viewFields = new XElement("ViewFields");
                            queryElement.Add(viewFields);
                        }

                        foreach (var field in Fields)
                        {
                            XElement viewField = new XElement("FieldRef");
                            viewField.SetAttributeValue("Name", field);
                            viewFields.Add(viewField);
                        }
                        query.ViewXml = queryElement.ToString();
                    }

                    if (HasPageSize())
                    {
                        var queryElement = XElement.Parse(query.ViewXml);

                        var rowLimit = queryElement.Descendants("RowLimit").FirstOrDefault();
                        if (rowLimit != null)
                        {
                            rowLimit.RemoveAll();
                        }
                        else
                        {
                            rowLimit = new XElement("RowLimit");
                            queryElement.Add(rowLimit);
                        }

                        rowLimit.SetAttributeValue("Paged", "TRUE");
                        rowLimit.SetValue(PageSize);

                        query.ViewXml = queryElement.ToString();
                    }

                    var camlDict = new Dictionary<string, object>() { { "query", query } };
                    var json = JsonConvert.SerializeObject(camlDict);

                    // do
                    // {
                    var listItems = new RestRequest(Context, $"{list.ObjectPath}/GetItems").Post<ResponseCollection<ListItem>>(json);

                    WriteObject(listItems.Items, true);

                    if (ScriptBlock != null)
                    {
                        ScriptBlock.Invoke(listItems);
                    }
                }
                else
                {
                    var restRequest = new RestRequest(Context, $"{list.ObjectPath}/Items");
                    if (HasPageSize())
                    {
                        restRequest = restRequest.Top((uint)PageSize);
                    }
                    
                    if(Fields != null)
                    {
                        restRequest = restRequest.Select(Fields);
                    }
                    var results = restRequest.Get<ResponseCollection<ListItem>>();
                    while (results != null && results.Items.Any())
                    {
                        WriteObject(results.Items, true);
                        if (!string.IsNullOrEmpty(results.NextLink))
                        {
                            restRequest = new RestRequest(Context, results.NextLink);
                            
                            results = restRequest.Get<ResponseCollection<ListItem>>();
                        } else
                        {
                            results = null;
                        }
                    }
                }
            }
        }

        private bool HasId()
        {
            return Id != -1;
        }

        private bool HasUniqueId()
        {
            return UniqueId != null && UniqueId.Id != Guid.Empty;
        }

        private bool HasCamlQuery()
        {
            return Query != null;
        }

        private bool HasFields()
        {
            return Fields != null;
        }

        private bool HasPageSize()
        {
            return PageSize > 0;
        }
    }
}
