﻿using AngleSharp.Dom;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharePointPnP.PowerShell.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace SharePointPnP.PowerShell.Core.Model
{
    /// <summary>
    /// This class is used to instantiate controls of type 3 (= client side web parts). Using this class you can instantiate a control and 
    /// add it on a <see cref="ClientSidePage"/>.
    /// </summary>
    public class ClientSideWebPart : CanvasControl
    {
        #region variables
        // Constants
        public const string WebPartAttribute = "data-sp-webpart";
        public const string WebPartDataVersionAttribute = "data-sp-webpartdataversion";
        public const string WebPartDataAttribute = "data-sp-webpartdata";
        public const string WebPartComponentIdAttribute = "data-sp-componentid";
        public const string WebPartHtmlPropertiesAttribute = "data-sp-htmlproperties";

        private ClientSideComponent component;
        private string jsonWebPartData;
        private string htmlPropertiesData;
        private string htmlProperties;
        private string webPartId;
        private string webPartData;
        private string title;
        private string description;
        private string propertiesJson;
        private ClientSideWebPartControlData spControlData;
        private JObject properties;
        private JObject serverProcessedContent;
        #endregion

        #region construction
        /// <summary>
        /// Instantiates client side web part from scratch.
        /// </summary>
        public ClientSideWebPart() : base()
        {
            this.controlType = 3;
            this.webPartData = "";
            this.htmlPropertiesData = "";
            this.htmlProperties = "";
            this.title = "";
            this.description = "";
            this.SetPropertiesJson("{}");
        }

        /// <summary>
        /// Instantiates a client side web part based on the information that was obtain from calling the AvailableClientSideComponents methods on the <see cref="ClientSidePage"/> object.
        /// </summary>
        /// <param name="component">Component to create a ClientSideWebPart instance for</param>
        public ClientSideWebPart(ClientSideComponent component) : this()
        {
            if (component == null)
            {
                throw new ArgumentNullException("Passed in component cannot be null");
            }
            this.Import(component);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Value of the "data-sp-webpartdata" attribute
        /// </summary>
        public string JsonWebPartData
        {
            get
            {
                return jsonWebPartData;
            }
        }

        /// <summary>
        /// Value of the "data-sp-htmlproperties" element
        /// </summary>
        public string HtmlPropertiesData
        {
            get
            {
                return htmlPropertiesData;
            }
        }

        /// <summary>
        /// Value of the "data-sp-htmlproperties" attribute
        /// </summary>
        public string HtmlProperties
        {
            get
            {
                return htmlProperties;
            }

        }

        /// <summary>
        /// ID of the client side web part
        /// </summary>
        public string WebPartId
        {
            get
            {
                return webPartId;
            }
        }

        /// <summary>
        /// Value of the "data-sp-webpart" attribute
        /// </summary>
        public string WebPartData
        {
            get
            {
                return webPartData;
            }
        }

        /// <summary>
        /// Title of the web part
        /// </summary>
        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
            }

        }

        /// <summary>
        /// Description of the web part
        /// </summary>
        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }

        /// <summary>
        /// Json serialized web part information. For 1st party web parts this ideally is the *full* JSON string 
        /// fetch via workbench or via copying it from an existing page. It's important that the serverProcessedContent
        /// element is included here!
        /// </summary>
        public string PropertiesJson
        {
            get
            {
                return this.Properties.ToString(Formatting.None);
            }
            set
            {
                this.SetPropertiesJson(value);
            }
        }

        /// <summary>
        /// Web properties as configurable <see cref="JObject"/>
        /// </summary>
        public JObject Properties
        {
            get
            {
                return this.properties;
            }
        }

        /// <summary>
        /// ServerProcessedContent json node
        /// </summary>
        public JObject ServerProcessedContent
        {
            get
            {
                return this.serverProcessedContent;
            }
        }

        /// <summary>
        /// Return <see cref="Type"/> of the client side web part
        /// </summary>
        public override Type Type
        {
            get
            {
                return typeof(ClientSideWebPart);
            }
        }


        /// <summary>
        /// Value of the "data-sp-controldata" attribute
        /// </summary>
        public ClientSideWebPartControlData SpControlData
        {
            get
            {
                return this.spControlData;
            }
        }
        #endregion

        #region public methods
        /// <summary>
        /// Imports a <see cref="ClientSideComponent"/> to use it as base for configuring the client side web part instance
        /// </summary>
        /// <param name="component"><see cref="ClientSideComponent"/> to import</param>
        /// <param name="clientSideWebPartPropertiesUpdater">Function callback that allows you to manipulate the client side web part properties after import</param>
        public void Import(ClientSideComponent component, Func<String, String> clientSideWebPartPropertiesUpdater = null)
        {
            this.component = component;
            // Sometimes the id guid is encoded with curly brackets, so let's drop those
            this.webPartId = component.Id.ToString("D");

            // Parse the manifest json blob as we need some data from it
            JObject wpJObject = JObject.Parse(component.Manifest);
            this.title = wpJObject["preconfiguredEntries"][0]["title"]["default"].Value<string>();
            this.description = wpJObject["preconfiguredEntries"][0]["title"]["default"].Value<string>();
            this.SetPropertiesJson(wpJObject["preconfiguredEntries"][0]["properties"].ToString(Formatting.None));

            if (clientSideWebPartPropertiesUpdater != null)
            {
                this.propertiesJson = clientSideWebPartPropertiesUpdater(this.propertiesJson);
            }
        }

        /// <summary>
        /// Returns a HTML representation of the client side web part
        /// </summary>
        /// <param name="controlIndex">The sequence of the control inside the section</param>
        /// <returns>HTML representation of the client side web part</returns>
        public override string ToHtml(float controlIndex)
        {
            // Can this control be hosted in this section type?
            if (this.Section.Type == CanvasSectionTemplate.OneColumnFullWidth)
            {
                if (!(this.WebPartId.Equals(ClientSidePage.ClientSideWebPartEnumToName(DefaultClientSideWebParts.Image), StringComparison.InvariantCultureIgnoreCase) ||
                      this.WebPartId.Equals(ClientSidePage.ClientSideWebPartEnumToName(DefaultClientSideWebParts.Hero), StringComparison.InvariantCultureIgnoreCase)))
                {
                    throw new Exception("You cannot host this web part inside a one column full width section, only an image web part or hero web part are allowed");
                }
            }

            // Obtain the json data
            ClientSideWebPartControlData controlData = new ClientSideWebPartControlData()
            {
                ControlType = this.ControlType,
                Id = this.InstanceId.ToString("D"),
                WebPartId = this.WebPartId,
                Position = new ClientSideCanvasControlPosition()
                {
                    ZoneIndex = this.Section.Order,
                    SectionIndex = this.Column.Order,
                    SectionFactor = this.Column.ColumnFactor,
                    ControlIndex = controlIndex,
                },
            };
            ClientSideWebPartData webpartData = new ClientSideWebPartData() { Id = controlData.WebPartId, InstanceId = controlData.Id, Title = this.Title, Description = this.Description, DataVersion = this.DataVersion, Properties = "jsonPropsToReplacePnPRules" };

            this.jsonControlData = JsonConvert.SerializeObject(controlData);
            this.jsonWebPartData = JsonConvert.SerializeObject(webpartData);
            this.jsonWebPartData = jsonWebPartData.Replace("\"jsonPropsToReplacePnPRules\"", this.Properties.ToString(Formatting.None));

            StringBuilder html = new StringBuilder(100);
            html.Append($@"<div {CanvasControlAttribute}=""{this.CanvasControlData}"" {CanvasDataVersionAttribute}=""{this.DataVersion}"" {ControlDataAttribute}=""{this.JsonControlData}"">");
            html.Append($@"<div {WebPartAttribute}=""{this.WebPartData}"" {WebPartDataVersionAttribute}=""{this.DataVersion}"" {WebPartDataAttribute}=""{this.JsonWebPartData}"">");
            html.Append($@"<div {WebPartComponentIdAttribute}=""""");
            html.Append(this.WebPartId);
            html.Append("/div>");
            html.Append($@"<div {WebPartHtmlPropertiesAttribute}=""{this.HtmlProperties}"">");
            RenderHtmlProperties(ref html);
            html.Append("</div>");
            html.Append("</div>");
            html.Append("</div>");
            //var htmlWriter = new HtmlTextWriter(new System.IO.StringWriter(html), "");
            //try
            //{
            //    htmlWriter.NewLine = string.Empty;
            //    htmlWriter.AddAttribute(CanvasControlAttribute, this.CanvasControlData);
            //    htmlWriter.AddAttribute(CanvasDataVersionAttribute, this.DataVersion);
            //    htmlWriter.AddAttribute(ControlDataAttribute, this.JsonControlData);
            //    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Div);

            //    htmlWriter.AddAttribute(WebPartAttribute, this.WebPartData);
            //    htmlWriter.AddAttribute(WebPartDataVersionAttribute, this.DataVersion);
            //    htmlWriter.AddAttribute(WebPartDataAttribute, this.JsonWebPartData);
            //    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Div);

            //    htmlWriter.AddAttribute(WebPartComponentIdAttribute, "");
            //    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Div);
            //    htmlWriter.Write(this.WebPartId);
            //    htmlWriter.RenderEndTag();

            //    htmlWriter.AddAttribute(WebPartHtmlPropertiesAttribute, this.HtmlProperties);
            //    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Div);
            //    // Allow for override of the HTML value rendering if this would be needed by controls
            //    RenderHtmlProperties(ref htmlWriter);
            //    htmlWriter.RenderEndTag();

            //    htmlWriter.RenderEndTag();
            //    htmlWriter.RenderEndTag();
            //}
            //finally
            //{
            //    if (htmlWriter != null)
            //    {
            //        htmlWriter.Dispose();
            //    }
            //}

            return html.ToString();
        }

        /// <summary>
        /// Overrideable method that allows inheriting webparts to control the HTML rendering
        /// </summary>
        /// <param name="htmlWriter">Reference to the html renderer used</param>
        protected virtual void RenderHtmlProperties(ref StringBuilder htmlWriter)
        {
            if (this.ServerProcessedContent != null)
            {
                if (this.ServerProcessedContent["searchablePlainTexts"] != null)
                {
                    foreach (JProperty property in this.ServerProcessedContent["searchablePlainTexts"])
                    {
                        htmlWriter.Append($@"<div data-sp-prop-name=""{property.Name}"" data-sp-searchableplaintext=""true"">");
                        htmlWriter.Append(property.Value.ToString());
                        htmlWriter.Append("</div>");
                    }
                }

                if (this.ServerProcessedContent["imageSources"] != null)
                {
                    foreach (JProperty property in this.ServerProcessedContent["imageSources"])
                    {
                        htmlWriter.Append($@"<img data-sp-prop-name=""{property.Name}""");

                        if (!string.IsNullOrEmpty(property.Value.ToString()))
                        {
                            htmlWriter.Append($@" src=""{property.Value.Value<string>()}""");
                        }
                        htmlWriter.Append("></img>");
                    }
                }

                if (this.ServerProcessedContent["links"] != null)
                {
                    foreach (JProperty property in this.ServerProcessedContent["links"])
                    {
                        htmlWriter.Append($@"<a data-sp-prop-name=""{property.Name}"" href=""{property.Value.Value<string>()}""></a>");
                    }
                }
            }
            else
            {
                htmlWriter.Append(this.htmlPropertiesData);
            }
        }
        #endregion

        #region Internal and private methods
        internal override void FromHtml(IElement element)
        {
            base.FromHtml(element);

            // load data from the data-sp-controldata attribute
            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            this.spControlData = JsonConvert.DeserializeObject<ClientSideWebPartControlData>(element.GetAttribute(CanvasControl.ControlDataAttribute), jsonSerializerSettings);
            this.controlType = this.spControlData.ControlType;


            var wpDiv = element.GetElementsByTagName("div").Where(a => a.HasAttribute(ClientSideWebPart.WebPartDataAttribute)).FirstOrDefault();
            this.webPartData = wpDiv.GetAttribute(ClientSideWebPart.WebPartAttribute);

            // Decode the html encoded string
            var decoded = WebUtility.HtmlDecode(wpDiv.GetAttribute(ClientSideWebPart.WebPartDataAttribute));
            JObject wpJObject = JObject.Parse(decoded);
            this.title = wpJObject["title"] != null ? wpJObject["title"].Value<string>() : "";
            this.description = wpJObject["description"] != null ? wpJObject["description"].Value<string>() : "";
            // Set property to trigger correct loading of properties 
            this.PropertiesJson = wpJObject["properties"].ToString(Formatting.None);
            // Store the server processed content as that's needed for full fidelity
            if (wpJObject["serverProcessedContent"] != null)
            {
                this.serverProcessedContent = (JObject)wpJObject["serverProcessedContent"];
            }

            this.webPartId = wpJObject["id"].Value<string>();

            var wpHtmlProperties = wpDiv.GetElementsByTagName("div").Where(a => a.HasAttribute(ClientSideWebPart.WebPartHtmlPropertiesAttribute)).FirstOrDefault();
            this.htmlPropertiesData = wpHtmlProperties.InnerHtml;
            this.htmlProperties = wpHtmlProperties.GetAttribute(ClientSideWebPart.WebPartHtmlPropertiesAttribute);
        }

        private void SetPropertiesJson(string json)
        {
            if (String.IsNullOrEmpty(json))
            {
                json = "{}";
            }

            this.propertiesJson = json;

            var parsedJson = JObject.Parse(json);

            // If the passed structure is the top level JSON structure, which it typically is, then grab the properties from it
            if (parsedJson["webPartData"] != null && parsedJson["webPartData"]["properties"] != null)
            {
                this.properties = (JObject)parsedJson["webPartData"]["properties"];
            }
            else if (parsedJson["properties"] != null)
            {
                this.properties = (JObject)parsedJson["properties"];
            }
            else
            {
                this.properties = parsedJson;
            }

            // If the web part has the serverProcessedContent property then keep this one as it might be needed as input to render the web part HTML later on
            if (parsedJson["webPartData"] != null && parsedJson["webPartData"]["serverProcessedContent"] != null)
            {
                this.serverProcessedContent = (JObject)parsedJson["webPartData"]["serverProcessedContent"];
            }
            else if (parsedJson["serverProcessedContent"] != null)
            {
                this.serverProcessedContent = (JObject)parsedJson["serverProcessedContent"];
            }

        }
        #endregion
    }
}