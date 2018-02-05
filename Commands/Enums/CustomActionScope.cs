namespace SharePointPnP.PowerShell.Core.Enums
{
    /// <summary>
    /// Scopes to which a CustomAction can be targeted
    /// </summary>
    public enum CustomActionScope
    {
        /// <summary>
        /// Sites
        /// </summary>
        Web  = 3,

        /// <summary>
        /// Site collections
        /// </summary>
        Site  = 2,

        /// <summary>
        /// Sites collections and sites
        /// </summary>
        All = 0
    }
}
