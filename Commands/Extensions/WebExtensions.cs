using SharePointPnP.PowerShell.Core.Model;

namespace SharePointPnP.PowerShell.Core.Extensions
{
    public static class WebExtensions
    {
        public static bool IsNoScriptSite(this Model.Web web)
        {
            var permissions = new RestRequest(web.Context, $"{web.Url}/_api/Web/EffectiveBasePermissions").Get<BasePermissions>();
            return !permissions.Has(PermissionKind.AddAndCustomizePages);
        }
    }

}