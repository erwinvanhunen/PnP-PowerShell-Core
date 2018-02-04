namespace SharePointPnP.PowerShell.Core.Model
{
    public abstract class SiteCreationInformation
    {
        //{"displayName":"test modernteamsite","alias":"testmodernteamsite","isPublic":true,"optionalParams":{"Description":"","CreationOptions":{"results":[]},"Classification":""}}

        /// <summary>
        /// The fully qualified url (e.g. https://yourtenant.sharepoint.com/sites/mysitecollection) of the site.
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// The title of the site to create
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Defines whether the Office 365 Group will be public (default), or private.
        /// </summary>
        public bool IsPublic { get; set; } = true;

        /// <summary>
        /// The Guid of the site design to be used. If specified will override the SiteDesign property
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The Site classification to use. For instance 'Contoso Classified'. See https://www.youtube.com/watch?v=E-8Z2ggHcS0 for more information
        /// </summary>
        public string Classification { get; set; }

        public SiteCreationInformation()
        {

        }

        public SiteCreationInformation(string alias, string displayName, string description = null)
        {
            this.Alias = alias;
            this.DisplayName = displayName;
            this.Description = description;
        }
    }
}