using System;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class Site
    {
        public bool AllowCreateDeclarativeWorkflow { get; set; }
        public bool AllowDesigner { get; set; }
        public bool AllowMasterPageEditing { get; set; }
        public bool AllowRevertFromTemplate { get; set; }
        public bool AllowSaveDeclarativeWorkflowAsTemplate { get; set; }
        public bool AllowSavePublishDeclarativeWorkflow { get; set; }
        public bool AllowSelfServiceUpgrade { get; set; }
        public bool AllowSelfServiceUpgradeEvaluation { get; set; }
        public int AuditLogTrimmingRetention { get; set; }
        public string Classification { get; set; }
        public int CompatibilityLevel { get; set; }
        public CurrentChangeToken CurrentChangeToken { get; set; }
        public bool DisableAppViews { get; set; }
        public bool DisableCompanyWideSharingLinks { get; set; }
        public bool DisableFlows { get; set; }
        public bool ExternalSharingTipsEnabled { get; set; }
        public string GeoLocation { get; set; }
        public string GroupId { get; set; }
        public string HubSiteId { get; set; }
        public string Id { get; set; }
        public bool IsHubSite { get; set; }
        public object LockIssue { get; set; }
        public int MaxItemsPerThrottledOperation { get; set; }
        public bool NeedsB2BUpgrade { get; set; }
        public ResourcePath ResourcePath { get; set; }
        public string PrimaryUri { get; set; }
        public bool ReadOnly { get; set; }
        public string RequiredDesignerVersion { get; set; }
        public int SandboxedCodeActivationCapability { get; set; }
        public string ServerRelativeUrl { get; set; }
        public bool ShareByEmailEnabled { get; set; }
        public bool ShareByLinkEnabled { get; set; }
        public bool ShowUrlStructure { get; set; }
        public bool TrimAuditLog { get; set; }
        public bool UIVersionConfigurationEnabled { get; set; }
        public DateTime UpgradeReminderDate { get; set; }
        public bool UpgradeScheduled { get; set; }
        public DateTime UpgradeScheduledDate { get; set; }
        public bool Upgrading { get; set; }
        public string Url { get; set; }
    }
}