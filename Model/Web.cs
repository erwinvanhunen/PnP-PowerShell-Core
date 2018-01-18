using Newtonsoft.Json;
using System;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class ResourcePath
    {
        public string DecodedUrl { get; set; }
    }

    public class Web
    {
        public bool AllowRssFeeds { get; set; }
        public string AlternateCssUrl { get; set; }
        public string AppInstanceId { get; set; }
        public int Configuration { get; set; }
        public DateTime Created { get; set; }
        public CurrentChangeToken CurrentChangeToken { get; set; }
        public string CustomMasterUrl { get; set; }
        public string Description { get; set; }
        public string DesignPackageId { get; set; }
        public bool DocumentLibraryCalloutOfficeWebAppPreviewersDisabled { get; set; }
        public bool EnableMinimalDownload { get; set; }
        public bool HorizontalQuickLaunch { get; set; }

        public Guid Id { get; set; }
        public bool IsMultilingual { get; set; }
        public int Language { get; set; }
        public DateTime LastItemModifiedDate { get; set; }
        public DateTime LastItemUserModifiedDate { get; set; }
        public string MasterUrl { get; set; }
        public bool NoCrawl { get; set; }
        public bool OverwriteTranslationsOnChange { get; set; }
        public ResourcePath ResourcePath { get; set; }
        public bool QuickLaunchEnabled { get; set; }
        public bool RecycleBinEnabled { get; set; }
        public Folder RootFolder { get; set; }
        public string ServerRelativeUrl { get; set; }
        public object SiteLogoUrl { get; set; }
        public bool SyndicationEnabled { get; set; }
        public string Title { get; set; }
        public bool TreeViewEnabled { get; set; }
        public int UIVersion { get; set; }
        public bool UIVersionConfigurationEnabled { get; set; }
        public string Url { get; set; }
        public string WebTemplate { get; set; }
    }
}