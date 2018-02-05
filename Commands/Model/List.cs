using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SharePointPnP.PowerShell.Core.Model
{

    public class CustomActionElements
    {
        public List<UserCustomAction> UserCustomActions { get; set; }
    }

    public class ImagePath
    {
        public string DecodedUrl { get; set; }
    }

    public class ParentWebPath
    {
        public string DecodedUrl { get; set; }
    }

    public class List : ClientSideObject
    {
        public bool AllowContentTypes { get; set; }
        public int BaseTemplate { get; set; }
        public int BaseType { get; set; }
        public bool ContentTypesEnabled { get; set; }
        public string DefaultViewUrl {get;set;}
        public Folder RootFolder { get; set; }
        public bool CrawlNonDefaultViews { get; set; }
        public DateTime Created { get; set; }
        public CurrentChangeToken CurrentChangeToken { get; set; }
        public CustomActionElements CustomActionElements { get; set; }
        public string DefaultContentApprovalWorkflowId { get; set; }
        public bool DefaultItemOpenUseListSetting { get; set; }
        public string Description { get; set; }
        public string Direction { get; set; }
        public string DocumentTemplateUrl { get; set; }
        public int DraftVersionVisibility { get; set; }
        public bool EnableAttachments { get; set; }
        public bool EnableFolderCreation { get; set; }
        public bool EnableMinorVersions { get; set; }
        public bool EnableModeration { get; set; }
        public bool EnableVersioning { get; set; }
        public string EntityTypeName { get; set; }
        public bool ExemptFromBlockDownloadOfNonViewableFiles { get; set; }
        public bool FileSavePostProcessingEnabled { get; set; }
        public bool ForceCheckout { get; set; }
        public bool HasExternalDataSource { get; set; }
        public bool Hidden { get; set; }
        public Guid Id { get; set; }
        public ImagePath ImagePath { get; set; }
        public string ImageUrl { get; set; }
        public bool IrmEnabled { get; set; }
        public bool IrmExpire { get; set; }
        public bool IrmReject { get; set; }
        public bool IsApplicationList { get; set; }
        public bool IsCatalog { get; set; }
        public bool IsPrivate { get; set; }
        public int ItemCount { get; set; }
        public DateTime LastItemDeletedDate { get; set; }
        public DateTime LastItemModifiedDate { get; set; }
        public DateTime LastItemUserModifiedDate { get; set; }
        public int ListExperienceOptions { get; set; }
        public string ListItemEntityTypeFullName { get; set; }
        public int MajorVersionLimit { get; set; }
        public int MajorWithMinorVersionsLimit { get; set; }
        public bool MultipleDataList { get; set; }
        public bool NoCrawl { get; set; }
        public bool OnQuickLaunch {get;set;}
        public ParentWebPath ParentWebPath { get; set; }
        public string ParentWebUrl { get; set; }
        public bool ParserDisabled { get; set; }
        public bool ServerTemplateCanCreateFolders { get; set; }
        public string TemplateFeatureId { get; set; }
        public string Title { get; set; }
    }
}