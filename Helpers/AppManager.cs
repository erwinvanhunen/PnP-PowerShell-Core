using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using SharePointPnP.PowerShell.Core.Model;
using SharePointPnP.PowerShell.Core.Base;
using System.Linq;

namespace SharePointPnP.PowerShell.Core.Helpers
{
    /// <summary>
    /// Allows Application Lifecycle Management for Apps
    /// </summary>
    public class AppManager
    {
        private string _appCatalogUrl;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="appCatalogUrl">Optional full url to the appcatalog site, e.g. https://yourtenant.sharepoint.com/sites/apps </param>
        public AppManager(string appCatalogUrl = null)
        {
            if (!string.IsNullOrEmpty(appCatalogUrl))
            {
                Uri uriResult;
                bool result = Uri.TryCreate(appCatalogUrl, UriKind.Absolute, out uriResult) && uriResult.Scheme == "https";
                if (result)
                {
                    _appCatalogUrl = appCatalogUrl;
                }
                else
                {
                    throw new ArgumentException("appCatalogUrl is not in a valid format", nameof(appCatalogUrl));
                }
            }
        }

        /// <summary>
        /// Uploads a file to the Tenant App Catalog
        /// </summary>
        /// <param name="file">A byte array containing the file</param>
        /// <param name="filename">The filename (e.g. myapp.sppkg) of the file to upload</param>
        /// <param name="overwrite">If true will overwrite an existing entry</param>
        /// <returns></returns>
        public AppMetadata Add(byte[] file, string filename, bool overwrite = false)
        {
            return AddAsync(file, filename, overwrite).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Uploads an app file to the Tenant App Catalog
        /// </summary>
        /// <param name="path"></param>
        /// <param name="overwrite">If true will overwrite an existing entry</param>
        /// <returns></returns>
        public AppMetadata Add(string path, bool overwrite = false)
        {
            return AddAsync(path, overwrite).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Uploads a file to the Tenant App Catalog
        /// </summary>
        /// <param name="file">A byte array containing the file</param>
        /// <param name="filename">The filename (e.g. myapp.sppkg) of the file to upload</param>
        /// <param name="overwrite">If true will overwrite an existing entry</param>
        /// <returns></returns>
        public async Task<AppMetadata> AddAsync(byte[] file, string filename, bool overwrite = false)
        {
            if (file == null && file.Length == 0)
            {
                throw new ArgumentException(nameof(file));
            }
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentException(nameof(filename));
            }
            return await BaseAddRequest(file, filename, overwrite);
        }

        /// <summary>
        /// Uploads an app file to the Tenant App Catalog
        /// </summary>
        /// <param name="path"></param>
        /// <param name="overwrite">If true will overwrite an existing entry</param>
        /// <returns></returns>
        public async Task<AppMetadata> AddAsync(string path, bool overwrite = false)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException(nameof(path));
            }

            if (System.IO.File.Exists(path))
            {
                throw new IOException("File does not exist");
            }

            var bytes = System.IO.File.ReadAllBytes(path);
            var fileInfo = new FileInfo(path);
            return await BaseAddRequest(bytes, fileInfo.Name, overwrite);
        }

        /// <summary>
        /// Installs an available app from the app catalog in a site.
        /// </summary>
        /// <param name="appMetadata">The app metadata object of the app to install</param>
        /// <returns></returns>
        public bool Install(AppMetadata appMetadata)
        {
            return InstallAsync(appMetadata).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Installs an available app from the app catalog in a site.
        /// </summary>
        /// <param name="appMetadata">The app metadata object of the app to install</param>
        /// <returns></returns>
        public async Task<bool> InstallAsync(AppMetadata appMetadata)
        {
            if (appMetadata == null)
            {
                throw new ArgumentException(nameof(appMetadata));
            }
            if (appMetadata.Id == Guid.Empty)
            {
                throw new ArgumentException(nameof(appMetadata.Id));
            }
            return await InstallAsync(appMetadata.Id);
        }

        /// <summary>
        /// Installs an available app from the app catalog in a site.
        /// </summary>
        /// <param name="id">The unique id of the app. Notice that this is not the product id as listed in the app catalog.</param>
        /// <returns></returns>
        public bool Install(Guid id)
        {
            return InstallAsync(id).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Installs an available app from the app catalog in a site.
        /// </summary>
        /// <param name="id">The unique id of the app. Notice that this is not the product id as listed in the app catalog.</param>
        /// <returns></returns>
        public async Task<bool> InstallAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException(nameof(id));
            }
            return await BaseRequest(id, "Install");
        }

        /// <summary>
        /// Uninstalls an app from a site.
        /// </summary>
        /// <param name="appMetadata">The app metadata object of the app to uninstall.</param>
        /// <returns></returns>
        public bool Uninstall(AppMetadata appMetadata)
        {
            return UninstallAsync(appMetadata).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Uninstalls an app from a site.
        /// </summary>
        /// <param name="appMetadata">The app metadata object of the app to uninstall.</param>
        /// <returns></returns>
        public async Task<bool> UninstallAsync(AppMetadata appMetadata)
        {
            if (appMetadata == null)
            {
                throw new ArgumentException(nameof(appMetadata));
            }
            if (appMetadata.Id == Guid.Empty)
            {
                throw new ArgumentException(nameof(appMetadata.Id));
            }
            return await UninstallAsync(appMetadata.Id);
        }

        /// <summary>
        /// Uninstalls an app from a site.
        /// </summary>
        /// <param name="id">The unique id of the app. Notice that this is not the product id as listed in the app catalog.</param>
        /// <returns></returns>
        public bool Uninstall(Guid id)
        {
            return UninstallAsync(id).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Uninstalls an app from a site.
        /// </summary>
        /// <param name="id">The unique id of the app. Notice that this is not the product id as listed in the app catalog.</param>
        /// <returns></returns>
        public async Task<bool> UninstallAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException(nameof(id));
            }
            return await BaseRequest(id, "Uninstall");
        }

        /// <summary>
        /// Upgrades an app in a site
        /// </summary>
        /// <param name="appMetadata">The app metadata object of the app to upgrade.</param>
        /// <returns></returns>
        public bool Upgrade(AppMetadata appMetadata)
        {
            return UpgradeAsync(appMetadata).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Upgrades an app in a site
        /// </summary>
        /// <param name="appMetadata">The app metadata object of the app to upgrade.</param>
        /// <returns></returns>
        public async Task<bool> UpgradeAsync(AppMetadata appMetadata)
        {
            if (appMetadata == null)
            {
                throw new ArgumentException(nameof(appMetadata));
            }
            if (appMetadata.Id == Guid.Empty)
            {
                throw new ArgumentException(nameof(appMetadata.Id));
            }
            return await UpgradeAsync(appMetadata.Id);
        }

        /// <summary>
        /// Upgrades an app in a site
        /// </summary>
        /// <param name="id">The unique id of the app. Notice that this is not the product id as listed in the app catalog.</param>
        /// <returns></returns>
        public bool Upgrade(Guid id)
        {
            return UpgradeAsync(id).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Upgrades an app in a site
        /// </summary>
        /// <param name="id">The unique id of the app. Notice that this is not the product id as listed in the app catalog.</param>
        /// <returns></returns>
        public async Task<bool> UpgradeAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException(nameof(id));
            }
            return await BaseRequest(id, "Upgrade");
        }

        /// <summary>
        /// Deploys/trusts an app in the app catalog
        /// </summary>
        /// <param name="appMetadata">The app metadata object of the app to deploy.</param>
        /// <param name="skipFeatureDeployment">If set to true will skip the feature deployed for tenant scoped apps.</param>
        /// <returns></returns>
        public bool Deploy(AppMetadata appMetadata, bool skipFeatureDeployment = true)
        {
            return DeployAsync(appMetadata, skipFeatureDeployment).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Deploys/trusts an app in the app catalog
        /// </summary>
        /// <param name="appMetadata">The app metadata object of the app to deploy.</param>
        /// <param name="skipFeatureDeployment">If set to true will skip the feature deployed for tenant scoped apps.</param>
        /// <returns></returns>
        public async Task<bool> DeployAsync(AppMetadata appMetadata, bool skipFeatureDeployment = true)
        {
            if (appMetadata == null)
            {
                throw new ArgumentException(nameof(appMetadata));
            }
            if (appMetadata.Id == Guid.Empty)
            {
                throw new ArgumentException(nameof(appMetadata.Id));
            }
            var postObj = new Dictionary<string, object>
            {
                { "skipFeatureDeployment", skipFeatureDeployment }
            };
            return await BaseRequest(appMetadata.Id, "Deploy", true, postObj);
        }

        /// <summary>
        /// Deploys/trusts an app in the app catalog
        /// </summary>
        /// <param name="id">The unique id of the app. Notice that this is not the product id as listed in the app catalog.</param>
        /// <param name="skipFeatureDeployment">If set to true will skip the feature deployed for tenant scoped apps.</param>
        /// <returns></returns>
        public bool Deploy(Guid id, bool skipFeatureDeployment = true)
        {
            return DeployAsync(id, skipFeatureDeployment).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Deploys/trusts an app in the app catalog
        /// </summary>
        /// <param name="id">The unique id of the app. Notice that this is not the product id as listed in the app catalog.</param>
        /// <param name="skipFeatureDeployment">If set to true will skip the feature deployed for tenant scoped apps.</param>
        /// <returns></returns>
        public async Task<bool> DeployAsync(Guid id, bool skipFeatureDeployment = true)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException(nameof(id));
            }
            var postObj = new Dictionary<string, object>
            {
                { "skipFeatureDeployment", skipFeatureDeployment }
            };
            return await BaseRequest(id, "Deploy", true, postObj);
        }

        /// <summary>
        /// Retracts an app in the app catalog. Notice that this will not remove the app from the app catalog.
        /// </summary>
        /// <param name="appMetadata">The app metadata object of the app to retract.</param>
        /// <returns></returns>
        public bool Retract(AppMetadata appMetadata)
        {
            return RetractAsync(appMetadata).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Retracts an app in the app catalog. Notice that this will not remove the app from the app catalog.
        /// </summary>
        /// <param name="appMetadata">The app metadata object of the app to retract.</param>
        /// <returns></returns>
        public async Task<bool> RetractAsync(AppMetadata appMetadata)
        {
            if (appMetadata == null)
            {
                throw new ArgumentException(nameof(appMetadata));
            }
            if (appMetadata.Id == Guid.Empty)
            {
                throw new ArgumentException(nameof(appMetadata.Id));
            }
            return await BaseRequest(appMetadata.Id, "Retract", true);
        }

        /// <summary>
        /// Retracts an app in the app catalog. Notice that this will not remove the app from the app catalog.
        /// </summary>
        /// <param name="id">The unique id of the app. Notice that this is not the product id as listed in the app catalog.</param>
        /// <returns></returns>
        public bool Retract(Guid id)
        {
            return RetractAsync(id).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Retracts an app in the app catalog. Notice that this will not remove the app from the app catalog.
        /// </summary>
        /// <param name="id">The unique id of the app. Notice that this is not the product id as listed in the app catalog.</param>
        /// <returns></returns>
        public async Task<bool> RetractAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException(nameof(id));
            }
            return await BaseRequest(id, "Retract", true);
        }

        /// <summary>
        /// Removes an app from the app catalog
        /// </summary>
        /// <param name="appMetadata">The app metadata object of the app to remove.</param>
        /// <returns></returns>
        public bool Remove(AppMetadata appMetadata)
        {
            return RemoveAsync(appMetadata).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Removes an app from the app catalog
        /// </summary>
        /// <param name="appMetadata">The app metadata object of the app to remove.</param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(AppMetadata appMetadata)
        {
            if (appMetadata == null)
            {
                throw new ArgumentException(nameof(appMetadata));
            }
            if (appMetadata.Id == Guid.Empty)
            {
                throw new ArgumentException(nameof(appMetadata.Id));
            }
            return await BaseRequest(appMetadata.Id, "Remove", true);
        }

        /// <summary>
        /// Removes an app from the app catalog
        /// </summary>
        /// <param name="id">The unique id of the app. Notice that this is not the product id as listed in the app catalog.</param>
        /// <returns></returns>
        public bool Remove(Guid id)
        {
            return RemoveAsync(id).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Removes an app from the app catalog
        /// </summary>
        /// <param name="id">The unique id of the app. Notice that this is not the product id as listed in the app catalog.</param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException(nameof(id));
            }
            return await BaseRequest(id, "Remove", true);
        }

        /// <summary>
        /// Returns all available apps.
        /// </summary>
        /// <returns></returns>
        public List<AppMetadata> GetAvailable()
        {
            return BaseGetAvailableAsync(Guid.Empty).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Returns all available apps.
        /// </summary>
        /// <returns></returns>
        public async Task<List<AppMetadata>> GetAvailableAsync()
        {
            return await BaseGetAvailableAsync(Guid.Empty);
        }

        /// <summary>
        /// Returns an available app
        /// </summary>
        /// <param name="id">The unique id of the app. Notice that this is not the product id as listed in the app catalog.</param>
        /// <returns></returns>
        public AppMetadata GetAvailable(Guid id)
        {
            return BaseGetAvailableAsync(id).GetAwaiter().GetResult();
        }

        public async Task<AppMetadata> GetAvailableAsync(Guid id)
        {
            return await BaseGetAvailableAsync(id);
        }

        /// <summary>
        /// Returns an available app
        /// </summary>
        /// <param name="id">The unique id of the app. Notice that this is not the product id as listed in the app catalog.</param>
        /// <returns></returns>
        private async Task<dynamic> BaseGetAvailableAsync(Guid id)
        {
            dynamic addins = null;

            var accessToken = SPOnlineConnection.AccessToken;

            using (var handler = new HttpClientHandler())
            {
                using (var httpClient = new PnPHttpProvider(handler))
                {
                    string requestUrl = $"{SPOnlineConnection.Url}/_api/web/tenantappcatalog/AvailableApps";
                    if (Guid.Empty != id)
                    {
                        requestUrl = $"{SPOnlineConnection.Url}/_api/web/tenantappcatalog/AvailableApps/GetById('{id}')";
                    }
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
                    request.Headers.Add("accept", "application/json;odata=verbose");
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                    }
                    request.Headers.Add("X-RequestDigest", await RestHelper.GetRequestDigest());

                    // Perform actual post operation
                    HttpResponseMessage response = await httpClient.SendAsync(request, new System.Threading.CancellationToken());

                    if (response.IsSuccessStatusCode)
                    {
                        // If value empty, URL is taken
                        var responseString = await response.Content.ReadAsStringAsync();
                        if (responseString != null)
                        {
                            try
                            {
                                if (Guid.Empty == id)
                                {
                                    var responseJson = JObject.Parse(responseString);
                                    var returnedAddins = responseJson["d"]["results"] as JArray;

                                    addins = JsonConvert.DeserializeObject<List<AppMetadata>>(returnedAddins.ToString());
                                }
                                else
                                {
                                    var responseJson = JObject.Parse(responseString);
                                    var returnedAddins = responseJson["d"];
                                    addins = JsonConvert.DeserializeObject<AppMetadata>(returnedAddins.ToString());
                                }

                            }
                            catch { }
                        }
                    }
                    else
                    {
                        // Something went wrong...
                        throw new Exception(await response.Content.ReadAsStringAsync());
                    }
                }
            }
            return await Task.Run(() => addins);
        }

        #region Private Methods
        private async Task<bool> BaseRequest(Guid id, string method, bool appCatalog = false, Dictionary<string, object> postObject = null)
        {
            var returnValue = false;
            var accessToken = SPOnlineConnection.AccessToken;

            using (var handler = new HttpClientHandler())
            {
                using (var httpClient = new PnPHttpProvider(handler))
                {
                    string requestUrl = $"{SPOnlineConnection.Url}/_api/web/tenantappcatalog/AvailableApps/GetByID('{id}')/{method}";

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
                    request.Headers.Add("accept", "application/json;odata=nometadata");
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                    }
                    request.Headers.Add("X-RequestDigest", await RestHelper.GetRequestDigest());

                    if (postObject != null)
                    {
                        var jsonBody = JsonConvert.SerializeObject(postObject);
                        var requestBody = new StringContent(jsonBody);
                        MediaTypeHeaderValue sharePointJsonMediaType;
                        MediaTypeHeaderValue.TryParse("application/json;odata=nometadata;charset=utf-8", out sharePointJsonMediaType);
                        requestBody.Headers.ContentType = sharePointJsonMediaType;
                        request.Content = requestBody;
                    }

                    // Perform actual post operation
                    HttpResponseMessage response = await httpClient.SendAsync(request, new System.Threading.CancellationToken());

                    if (response.IsSuccessStatusCode)
                    {
                        // If value empty, URL is taken
                        var responseString = await response.Content.ReadAsStringAsync();
                        if (responseString != null)
                        {
                            try
                            {
                                var responseJson = JObject.Parse(responseString);
                                returnValue = true;
                            }
                            catch { }
                        }
                    }
                    else
                    {
                        // Something went wrong...
                        throw new Exception(await response.Content.ReadAsStringAsync());
                    }
                }
            }
            return await Task.Run(() => returnValue);
        }

        public static string GetAppCatalogUrl()
        {
            var result = new RestRequest("search/query?querytext='contentclass:STS_Site+AND+SiteTemplate:APPCATALOG'").Get();
            var resultData = JsonConvert.DeserializeObject<dynamic>(result);
            var rows = resultData["PrimaryQueryResult"]["RelevantResults"]["Table"]["Rows"];
            if(rows.Count > 0)
            {
                var row = rows[0];
                for(var q=0;q<row["Cells"].Count;q++)
                {
                    if(row["Cells"][q]["Key"] == "Path")
                    {
                        return row["Cells"][q]["Value"].Value as string;
                    }
                }
            }
            return null;
        }

        private async Task<AppMetadata> BaseAddRequest(byte[] file, string filename, bool overwrite = false)
        {
            AppMetadata returnValue = null;

            using (var handler = new HttpClientHandler())
            {
                using (var httpClient = new PnPHttpProvider(handler))
                {

                    var requestUrl = $"web/tenantappcatalog/Add(overwrite={(overwrite.ToString().ToLower())}, url='{filename}')";
                    var additionalHeaders = new Dictionary<string, string>();
                    additionalHeaders.Add("binaryStringRequestBody", "true");
                    var response = Helpers.RestHelper.ExecutePostRequest(requestUrl, file, additionalHeaders: additionalHeaders);

                    if (response.IsSuccessStatusCode)
                    {
                        // If value empty, URL is taken
                        var responseString = await response.Content.ReadAsStringAsync();
                        if (responseString != null)
                        {
                            var responseJson = JObject.Parse(responseString);
                            var id = responseJson["d"]["UniqueId"].ToString();
                            var metadataRequestUrl = $"web/tenantappcatalog/AvailableApps/GetById('{id}')";
                            var metadataResponse = Helpers.RestHelper.ExecutePostRequest(metadataRequestUrl);

                            if (metadataResponse.IsSuccessStatusCode)
                            {
                                // If value empty, URL is taken
                                var metadataResponseString = await metadataResponse.Content.ReadAsStringAsync();
                                if (metadataResponseString != null)
                                {
                                    var metadataResponseJson = JObject.Parse(metadataResponseString);
                                    var returnedAddins = metadataResponseJson["d"];
                                    returnValue = JsonConvert.DeserializeObject<AppMetadata>(returnedAddins.ToString());
                                }
                            }
                            else
                            {
                                // Something went wrong...
                                throw new Exception(await metadataResponse.Content.ReadAsStringAsync());
                            }
                        }
                    }
                    else
                    {
                        // Something went wrong...
                        throw new Exception(await response.Content.ReadAsStringAsync());
                    }
                }
            }
            return await Task.Run(() => returnValue);
        }
        #endregion
    }
}
