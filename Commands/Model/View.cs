using Newtonsoft.Json;
using SharePointPnP.PowerShell.Core.Enums;
using System;
using System.Collections.Generic;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class View : ClientSideObject
    {
        public View() : base("SP.View")
        { }

        [JsonProperty("ViewType")]
        private string _viewType { get; set; }

        [JsonProperty("ViewFields")]
        private ViewFields _viewFields { get; set; }
        public object Aggregations { get; set; }
        public object AggregationsStatus { get; set; }
        public string BaseViewId { get; set; }
        public ContentTypeId ContentTypeId { get; set; }
        public object CustomFormatter { get; set; }
        public bool DefaultView { get; set; }
        public bool DefaultViewForContentType { get; set; }
        public bool EditorModified { get; set; }
        public object Formats { get; set; }
        public bool Hidden { get; set; }
        public string HtmlSchemaXml { get; set; }
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public bool IncludeRootFolder { get; set; }
        public object ViewJoins { get; set; }
        public string JSLink { get; set; }
        public string ListViewXml { get; set; }
        public object Method { get; set; }
        public bool MobileDefaultView { get; set; }
        public bool MobileView { get; set; }
        public object ModerationType { get; set; }
        public bool OrderedView { get; set; }
        public bool Paged { get; set; }
        public bool PersonalView { get; set; }
        public object ViewProjectedFields { get; set; }
        public string ViewQuery { get; set; }
        public bool ReadOnlyView { get; set; }
        public bool RequiresClientIntegration { get; set; }
        public int RowLimit { get; set; }
        public int Scope { get; set; }
        public ServerRelativePath ServerRelativePath { get; set; }
        public string ServerRelativeUrl { get; set; }
        public object StyleId { get; set; }
        public bool TabularView { get; set; }
        public bool Threaded { get; set; }
        public string Title { get; set; }
        public string Toolbar { get; set; }
        public object ToolbarTemplateName { get; set; }

        [JsonIgnore]
        public ViewType ViewType
        {
            get
            {
                return (ViewType)Enum.Parse(typeof(ViewType), _viewType);
            }
            set
            {
                _viewType = value.ToString();
            }
        }

        [JsonIgnore]
        public List<string> ViewFields
        {
            get
            {
                if (_viewFields != null)
                {
                    return _viewFields.Items;
                }
                return null;
            }
        }

        public object ViewData { get; set; }
        public object VisualizationInfo { get; set; }
    }

    internal class ViewFields
    {
        [JsonProperty("odata.type")]
        public string OdataType { get; set; }

        [JsonProperty("odata.id")]
        public string OdataId { get; set; }

        [JsonProperty("odata.editLink")]
        public string OdataEditLink { get; set; }

        [JsonProperty("SchemaXml")]
        public string SchemaXml { get; set; }

        [JsonProperty("Items")]
        public List<string> Items { get; set; }
    }

}