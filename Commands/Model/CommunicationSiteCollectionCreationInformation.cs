using System;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class CommunicationSiteCollectionCreationInformation
    {
        /// <summary>
        /// The fully qualified url (e.g. https://yourtenant.sharepoint.com/sites/mysitecollection) of the site.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The title of the site to create
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The owner of the site. Reserved for future use.
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// The Guid of the site design to be used. If specified will override the SiteDesign property
        /// </summary>
        public Guid SiteDesignId { get; set; }

        /// <summary>
        /// The built-in site design to used. If both SiteDesignId and SiteDesign have been specified, the GUID specified as SiteDesignId will be used.
        /// </summary>
        public CommunicationSiteDesign SiteDesign { get; set; }

        /// <summary>
        /// If set to true, file sharing for guest users will be allowed.
        /// </summary>
        public bool AllowFileSharingForGuestUsers { get; set; }

        /// <summary>
        /// The Site classification to use. For instance 'Contoso Classified'. See https://www.youtube.com/watch?v=E-8Z2ggHcS0 for more information
        /// </summary>
        public string Classification { get; set; }

        /// <summary>
        /// The description to use for the site.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The language to use for the site. If not specified will default to the language setting of the clientcontext.
        /// </summary>
        public uint Lcid { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public CommunicationSiteCollectionCreationInformation()
        {

        }

        /// <summary>
        /// CommunicationSiteCollectionCreationInformation constructor
        /// </summary>
        /// <param name="fullUrl">Url for the new communication site</param>
        /// <param name="title">Title of the site</param>
        /// <param name="description">Description of the site</param>
        public CommunicationSiteCollectionCreationInformation(string fullUrl, string title, string description = null)
        {
            this.Url = fullUrl;
            this.Title = title;
            this.Description = description;
        }
    }
}