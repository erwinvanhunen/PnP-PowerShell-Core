using System;
using System.Management.Automation;
using SharePointPnP.PowerShell.Core.Attributes;
using SharePointPnP.PowerShell.Core.Base;
using System.Threading.Tasks;
using SharePointPnP.PowerShell.Core.Base.PipeBinds;
using SharePointPnP.PowerShell.Core.Model;
using SharePointPnP.PowerShell.Core.Helpers;

namespace SharePointPnP.PowerShell.Core.Admin
{
    [Cmdlet(VerbsCommon.New, "Site")]
    [CmdletHelp(VerbsCommon.New, "Site", "Creates a new site collection",
        "The New-PnPSite cmdlet creates a new site collection for the current tenant. Currently only 'modern' sites like Communication Site and the Modern Team Site are supported. If you want to create a classic site, use New-PnPTenantSite.",
        OutputType = typeof(string),
        OutputTypeDescription = "Returns the url of the newly created site collection",
        Category = CmdletHelpCategory.TenantAdmin, SupportedPlatform = CmdletSupportedPlatform.Online)]
    [CmdletExample(
        Code = @"PS:> New-PnPSite -Type CommunicationSite -Title Contoso -Url https://tenant.sharepoint.com/sites/contoso",
        Remarks = @"This will create a new Communications Site collection with the title 'Contoso' and the url 'https://tenant.sharepoint.com/sites/contoso'",
        SortOrder = 1)]
    [CmdletExample(
        Code = @"PS:> New-PnPSite -Type CommunicationSite -Title Contoso -Url https://tenant.sharepoint.com/sites/contoso -SiteDesign Showcase",
        Remarks = @"This will create a new Communications Site collection with the title 'Contoso' and the url 'https://tenant.sharepoint.com/sites/contoso'. It will use the 'Showcase' design for the site.",
        SortOrder = 2)]
    [CmdletExample(
        Code = @"PS:> New-PnPSite -Type CommunicationSite -Title Contoso -Url https://tenant.sharepoint.com/sites/contoso -SiteDesignId ae2349d5-97d6-4440-94d1-6516b72449ac",
        Remarks = @"This will create a new Communications Site collection with the title 'Contoso' and the url 'https://tenant.sharepoint.com/sites/contoso'. It will use the specified custom site design for the site.",
        SortOrder = 3)]
    [CmdletExample(
        Code = @"PS:> New-PnPSite -Type CommunicationSite -Title Contoso -Url https://tenant.sharepoint.com/sites/contoso -Classification ""HBI""",
        Remarks = @"This will create a new Communications Site collection with the title 'Contoso' and the url 'https://tenant.sharepoint.com/sites/contoso'. The classification for the site will be set to ""HBI""",
        SortOrder = 4)]
    [CmdletExample(
        Code = @"PS:> New-PnPSite -Type CommunicationSite -Title Contoso -Url https://tenant.sharepoint.com/sites/contoso -AllowFileSharingForGuestUsers",
        Remarks = @"This will create a new Communications Site collection with the title 'Contoso' and the url 'https://tenant.sharepoint.com/sites/contoso'. File sharing for guest users will be enabled.",
        SortOrder = 5)]
    [CmdletExample(
        Code = @"PS:> New-PnPSite -Type TeamSite -Title Contoso -Alias contoso",
        Remarks = @"This will create a new Modern Team Site collection with the title 'Contoso' and the url 'https://tenant.sharepoint.com/sites/contoso'.",
        SortOrder = 6)]
    [CmdletExample(
        Code = @"PS:> New-PnPSite -Type TeamSite -Title Contoso -Alias contoso -IsPublic",
        Remarks = @"This will create a new Modern Team Site collection with the title 'Contoso' and the url 'https://tenant.sharepoint.com/sites/contoso' and sets the site to public.",
        SortOrder = 7)]
    [CmdletAdditionalParameter(ParameterType = typeof(string), ParameterName = "Title", Mandatory = true, HelpMessage = @"Specifies the title of the new site collection", ParameterSetName = ParameterSet_COMMUNICATIONBUILTINDESIGN)]
    [CmdletAdditionalParameter(ParameterType = typeof(string), ParameterName = "Title", Mandatory = true, HelpMessage = @"Specifies the title of the new site collection", ParameterSetName = ParameterSet_COMMUNICATIONCUSTOMDESIGN)]
    [CmdletAdditionalParameter(ParameterType = typeof(string), ParameterName = "Url", Mandatory = true, HelpMessage = @"Specifies the full url of the new site collection", ParameterSetName = ParameterSet_COMMUNICATIONBUILTINDESIGN)]
    [CmdletAdditionalParameter(ParameterType = typeof(string), ParameterName = "Url", Mandatory = true, HelpMessage = @"Specifies the full url of the new site collection", ParameterSetName = ParameterSet_COMMUNICATIONCUSTOMDESIGN)]
    [CmdletAdditionalParameter(ParameterType = typeof(string), ParameterName = "Description", Mandatory = false, HelpMessage = @"Specifies the description of the new site collection", ParameterSetName = ParameterSet_COMMUNICATIONBUILTINDESIGN)]
    [CmdletAdditionalParameter(ParameterType = typeof(string), ParameterName = "Description", Mandatory = false, HelpMessage = @"Specifies the description of the new site collection", ParameterSetName = ParameterSet_COMMUNICATIONCUSTOMDESIGN)]
    [CmdletAdditionalParameter(ParameterType = typeof(string), ParameterName = "Classification", Mandatory = false, HelpMessage = @"Specifies the classification of the new site collection", ParameterSetName = ParameterSet_COMMUNICATIONBUILTINDESIGN)]
    [CmdletAdditionalParameter(ParameterType = typeof(string), ParameterName = "Classification", Mandatory = false, HelpMessage = @"Specifies the classification of the new site collection", ParameterSetName = ParameterSet_COMMUNICATIONCUSTOMDESIGN)]
    [CmdletAdditionalParameter(ParameterType = typeof(SwitchParameter), ParameterName = "AllowFileSharingForGuestUsers", Mandatory = false, HelpMessage = @"Specifies if guest users can share files in the new site collection", ParameterSetName = ParameterSet_COMMUNICATIONBUILTINDESIGN)]
    [CmdletAdditionalParameter(ParameterType = typeof(SwitchParameter), ParameterName = "AllowFileSharingForGuestUsers", Mandatory = false, HelpMessage = @"Specifies if guest users can share files in the new site collection", ParameterSetName = ParameterSet_COMMUNICATIONCUSTOMDESIGN)]
    [CmdletAdditionalParameter(ParameterType = typeof(CommunicationSiteDesign), ParameterName = "SiteDesign", Mandatory = false, HelpMessage = @"Specifies the site design of the new site collection. Defaults to 'Topic'", ParameterSetName = ParameterSet_COMMUNICATIONBUILTINDESIGN)]
    [CmdletAdditionalParameter(ParameterType = typeof(GuidPipeBind), ParameterName = "SiteDesignId", Mandatory = true, HelpMessage = @"Specifies the site design id to use for the new site collection. If specified will override SiteDesign", ParameterSetName = ParameterSet_COMMUNICATIONCUSTOMDESIGN)]
    [CmdletAdditionalParameter(ParameterType = typeof(uint), ParameterName = "Lcid", Mandatory = false, HelpMessage = @"Specifies the language of the new site collection. Defaults to the current language of the web connected to.", ParameterSetName = ParameterSet_COMMUNICATIONBUILTINDESIGN)]
    [CmdletAdditionalParameter(ParameterType = typeof(uint), ParameterName = "Lcid", Mandatory = false, HelpMessage = @"Specifies the language of the new site collection. Defaults to the current language of the web connected to.", ParameterSetName = ParameterSet_COMMUNICATIONCUSTOMDESIGN)]
    [CmdletAdditionalParameter(ParameterType = typeof(string), ParameterName = "Title", Mandatory = true, HelpMessage = @"Specifies the title of the new site collection", ParameterSetName = ParameterSet_TEAM)]
    [CmdletAdditionalParameter(ParameterType = typeof(string), ParameterName = "Alias", Mandatory = true, HelpMessage = @"Specifies the alias of the new site collection", ParameterSetName = ParameterSet_TEAM)]
    [CmdletAdditionalParameter(ParameterType = typeof(string), ParameterName = "Description", Mandatory = false, HelpMessage = @"Specifies the description of the new site collection", ParameterSetName = ParameterSet_TEAM)]
    [CmdletAdditionalParameter(ParameterType = typeof(string), ParameterName = "Classification", Mandatory = false, HelpMessage = @"Specifies the classification of the new site collection", ParameterSetName = ParameterSet_TEAM)]
    [CmdletAdditionalParameter(ParameterType = typeof(string), ParameterName = "IsPublic", Mandatory = false, HelpMessage = @"Specifies if new site collection is public. Defaults to false.", ParameterSetName = ParameterSet_TEAM)]
    public class NewSite : PnPCmdlet, IDynamicParameters
    {
        private const string ParameterSet_COMMUNICATIONBUILTINDESIGN = "Communication Site with Built-In Site Design";
        private const string ParameterSet_COMMUNICATIONCUSTOMDESIGN = "Communication Site with Custom Design";
        private const string ParameterSet_TEAM = "Team Site";

        [Parameter(Mandatory = true, HelpMessage = "@Specifies with type of site to create.")]
        public SiteType Type;

        private CommunicationSiteParameters _communicationSiteParameters;
        private TeamSiteParameters _teamSiteParameters;

        public object GetDynamicParameters()
        {
            switch (Type)
            {
                case SiteType.CommunicationSite:
                    {
                        _communicationSiteParameters = new CommunicationSiteParameters();
                        return _communicationSiteParameters;
                    }
                case SiteType.TeamSite:
                    {
                        _teamSiteParameters = new TeamSiteParameters();
                        return _teamSiteParameters;
                    }
            }
            return null;
        }

        protected override void ExecuteCmdlet()
        {
            if (Type == SiteType.CommunicationSite)
            {
                if (!MyInvocation.BoundParameters.ContainsKey("Lcid"))
                {
                    var web = new RestRequest(CurrentContext, "Web").Select("Language").Get<Model.Web>();
                    _communicationSiteParameters.Lcid = web.Language;
                }
                var creationInformation = new CommunicationSiteCollectionCreationInformation();
                creationInformation.Title = _communicationSiteParameters.Title;
                creationInformation.Url = _communicationSiteParameters.Url;
                creationInformation.Description = _communicationSiteParameters.Description;
                creationInformation.Classification = _communicationSiteParameters.Classification;
                creationInformation.AllowFileSharingForGuestUsers = _communicationSiteParameters.AllowFileSharingForGuestUsers;
                creationInformation.Lcid = _communicationSiteParameters.Lcid;
                if (ParameterSetName == "CommunicationCustomInDesign")
                {
                    creationInformation.SiteDesignId = _communicationSiteParameters.SiteDesignId.Id;
                }
                else
                {
                    creationInformation.SiteDesign = _communicationSiteParameters.SiteDesign;
                }
                var results = SiteCollection.CreateAsync(CurrentContext, creationInformation);
                var returnedUrl = results.GetAwaiter().GetResult();
                WriteObject(returnedUrl);
            }
            else
            {
                var creationInformation = new TeamSiteCollectionCreationInformation();
                creationInformation.DisplayName = _teamSiteParameters.Title;
                creationInformation.Alias = _teamSiteParameters.Alias;
                creationInformation.Classification = _teamSiteParameters.Classification;
                creationInformation.Description = _teamSiteParameters.Description;
                creationInformation.IsPublic = _teamSiteParameters.IsPublic;

                var results = SiteCollection.CreateAsync(CurrentContext, creationInformation);
                var returnedUrl = results.GetAwaiter().GetResult();
                WriteObject(returnedUrl);
            }
        }

        public class CommunicationSiteParameters
        {
            [Parameter(Mandatory = true, ParameterSetName = ParameterSet_COMMUNICATIONBUILTINDESIGN)]
            [Parameter(Mandatory = true, ParameterSetName = ParameterSet_COMMUNICATIONCUSTOMDESIGN)]
            public string Title;

            [Parameter(Mandatory = true, ParameterSetName = ParameterSet_COMMUNICATIONBUILTINDESIGN)]
            [Parameter(Mandatory = true, ParameterSetName = ParameterSet_COMMUNICATIONCUSTOMDESIGN)]
            public string Url;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_COMMUNICATIONBUILTINDESIGN)]
            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_COMMUNICATIONCUSTOMDESIGN)]
            public string Description;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_COMMUNICATIONBUILTINDESIGN)]
            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_COMMUNICATIONCUSTOMDESIGN)]
            public string Classification;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_COMMUNICATIONBUILTINDESIGN)]
            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_COMMUNICATIONCUSTOMDESIGN)]
            public SwitchParameter AllowFileSharingForGuestUsers;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_COMMUNICATIONBUILTINDESIGN)]
            public CommunicationSiteDesign SiteDesign = CommunicationSiteDesign.Topic;

            [Parameter(Mandatory = true, ParameterSetName = ParameterSet_COMMUNICATIONCUSTOMDESIGN)]
            public GuidPipeBind SiteDesignId;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_COMMUNICATIONBUILTINDESIGN)]
            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_COMMUNICATIONCUSTOMDESIGN)]
            public uint Lcid;
        }

        public class TeamSiteParameters
        {
            [Parameter(Mandatory = true, ParameterSetName = ParameterSet_TEAM)]
            public string Title;

            [Parameter(Mandatory = true, ParameterSetName = ParameterSet_TEAM)]
            public string Alias;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TEAM)]
            public string Description;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TEAM)]
            public string Classification;

            [Parameter(Mandatory = false, ParameterSetName = ParameterSet_TEAM)]
            public SwitchParameter IsPublic;
        }
    }
}