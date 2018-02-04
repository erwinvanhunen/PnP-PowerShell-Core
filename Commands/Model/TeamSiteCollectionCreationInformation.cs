namespace SharePointPnP.PowerShell.Core.Model
{
    
    /// <summary>
    /// Class for site creation information
    /// </summary>
    public class TeamSiteCollectionCreationInformation : SiteCreationInformation
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public TeamSiteCollectionCreationInformation() : base()
        {
        }

        /// <summary>
        /// TeamSiteCollectionCreationInformation constructor
        /// </summary>
        /// <param name="alias">Alias for the group linked to this site</param>
        /// <param name="displayName">Name of the site</param>
        /// <param name="description">Title of the site</param>
        public TeamSiteCollectionCreationInformation(string alias, string displayName, string description = null) : base(alias, displayName, description)
        {
        }
    }
}