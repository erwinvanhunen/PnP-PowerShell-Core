using Newtonsoft.Json;
using System;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class ResourcePath
    {
        public string DecodedUrl { get; set; }
    }



    public class Web : ClientSideObject
    {
        #region PRIVATE
        [JsonProperty("Url")]
        private string _url
        {
            get; set;
        }

        [JsonProperty("Title")]
        private string _title { get; set; }

        [JsonProperty("AllowRssFeeds")]
        private bool _allowRssFeeds { get; set; }

        [JsonProperty("AlternateCssUrl")]
        private string _alternateCssUrl { get; set; }

        [JsonProperty("AppInstanceId")]
        private string _appInstanceId { get; set; }

        [JsonProperty("Configuration")]
        private int _configuration { get; set; }

        [JsonProperty("Created")]
        private DateTime _created { get; set; }

        [JsonProperty("CurrentChangeToken")]
        private CurrentChangeToken _currentChangeToken { get; set; }

        [JsonProperty("CustomMasterUrl")]
        private string _customMasterUrl { get; set; }

        [JsonProperty("Description")]
        private string _description { get; set; }

        [JsonProperty("DesignPackageId")]
        private Guid _designPackageId { get; set; }

        [JsonProperty("DocumentLibraryCalloutOfficeWebAppPreviewersDisabled")]
        private bool _documentLibraryCalloutOfficeWebAppPreviewersDisabled { get; set; }

        [JsonProperty("EnableMinimalDownload")]
        private bool _enableMinimalDownload { get; set; }

        [JsonProperty("HorizontalQuickLaunch")]
        private bool _horizontalQuickLaunch { get; set; }

        [JsonProperty("Id")]
        private Guid _id { get; set; }

        [JsonProperty("IsMultilingual")]
        private bool _isMultilingual { get; set; }

        [JsonProperty("RequestAccessEmail")]
        private string _requestAccessEmail { get; set; }

        [JsonProperty("ServerRelativeUrl")]
        private string _serverRelativeUrl { get; set; }

        #endregion

        #region CONSTRUCTOR
        public Web() : base("web", "SP.Web")
        { }
        #endregion

        #region PUBLIC        
        [JsonIgnore]
        public bool AllowRssFeeds
        {
            get
            {
                return _allowRssFeeds;
            }
        }

        [JsonIgnore]
        public string AlternateCssUrl
        {
            get
            {
                return _alternateCssUrl;
            }
            set
            {
                _alternateCssUrl = value;
                base.ObjectProperties["AlternateCssUrl"] = value;
            }
        }

        [JsonIgnore]
        public string AppInstanceId
        {
            get
            {
                return _appInstanceId;
            }
        }

        [JsonIgnore]
        public int Configuration
        {
            get
            {
                return _configuration;
            }
        }

        [JsonIgnore]
        public DateTime Created
        {
            get
            {
                return _created;
            }
        }

        [JsonIgnore]
        public CurrentChangeToken CurrentChangeToken
        {
            get
            {
                return _currentChangeToken;
            }
        }

        [JsonIgnore]
        public string CustomMasterUrl
        {
            get
            {
                return _customMasterUrl;
            }
            set
            {
                _customMasterUrl = value;
                base.ObjectProperties["CustomMasterUrl"] = value;
            }
        }

        [JsonIgnore]
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                base.ObjectProperties["Description"] = value;
            }
        }

        [JsonIgnore]
        public Guid DesignPackageId
        {
            get
            {
                return _designPackageId;
            }
            set
            {
                _designPackageId = value;
                base.ObjectProperties["DesignPackageId"] = value;
            }
        }

        [JsonIgnore]
        public bool DocumentLibraryCalloutOfficeWebAppPreviewersDisabled
        {
            get
            {
                return _documentLibraryCalloutOfficeWebAppPreviewersDisabled;
            }
        }

        [JsonIgnore]
        public bool EnableMinimalDownload
        {
            get
            {
                return _enableMinimalDownload;
            }
            set
            {
                _enableMinimalDownload = value;
                base.ObjectProperties["EnableMinimalDownload"] = value;
            }
        }

        [JsonIgnore]
        public bool HorizontalQuickLaunch
        {
            get
            {
                return _horizontalQuickLaunch;
            }
            set
            {
                _horizontalQuickLaunch = value;
                base.ObjectProperties["HorizontalQuickLaunch"] = value;
            }
        }

        [JsonIgnore]
        public Guid Id
        {
            get
            {
                return _id;
            }
        }

        [JsonIgnore]
        public bool IsMultilingual
        {
            get
            {
                return _isMultilingual;
            }
            set
            {
                _isMultilingual = value;
                base.ObjectProperties["IsMultilingual"] = value;
            }
        }

        [JsonIgnore]
        public string RequestAccessEmail
        {
            get
            {
                return _requestAccessEmail;
            }
            set
            {
                _requestAccessEmail = value;
                base.ObjectProperties["RequestAccessEmail"] = value;
            }
        }

        public int Language { get; set; }
        public DateTime LastItemModifiedDate { get; set; }
        public DateTime LastItemUserModifiedDate { get; set; }
        public string MasterUrl { get; set; }
        public bool NoCrawl { get; set; }
        public bool OverwriteTranslationsOnChange { get; set; }
        public ResourcePath ResourcePath { get; set; }
        public bool QuickLaunchEnabled { get; set; }
        public bool RecycleBinEnabled { get; set; }
        public Folder RootFolder { get; }

        [JsonIgnore]
        public string ServerRelativeUrl
        {
            get
            {
                return _serverRelativeUrl;
            }
        }
        
        public object SiteLogoUrl { get; set; }
        public bool SyndicationEnabled { get; set; }

        [JsonIgnore]
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                base.ObjectProperties["Title"] = value;
            }
        }

        public bool TreeViewEnabled { get; set; }
        public int UIVersion { get; set; }
        public bool UIVersionConfigurationEnabled { get; set; }

        [JsonIgnore]
        public string Url
        {
            get
            {
                return _url;
            }
        }
        public string WebTemplate { get; set; }
        #endregion
    }
}