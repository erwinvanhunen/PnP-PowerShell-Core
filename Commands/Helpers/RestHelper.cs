using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SharePointPnP.PowerShell.Core.Base;
using SharePointPnP.PowerShell.Core.Model;

namespace SharePointPnP.PowerShell.Core.Helpers
{
    public static class RestHelper
    {
        public static async Task<string> GetRequestDigest(SPOnlineConnection context, string url = null)
        {
            if (url == null)
            {
                url = context.Url;
            }
            using (var handler = new HttpClientHandler())
            {
                string responseString = string.Empty;
                var accessToken = context.AccessToken;

                using (var httpClient = new PnPHttpProvider(handler))
                {
                    string requestUrl = String.Format("{0}/_api/contextinfo", url);
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
                    request.Headers.Add("accept", "application/json;odata=verbose");
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                    }
                    HttpResponseMessage response = await httpClient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        responseString = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        throw new Exception(await response.Content.ReadAsStringAsync());
                    }
                }
                var contextInformation = JsonConvert.DeserializeObject<dynamic>(responseString);

                string formDigestValue = contextInformation.d.GetContextWebInformation.FormDigestValue;
                return await Task.Run(() => formDigestValue);
            }
        }

        public static T ExecuteGetRequest<T>(SPOnlineConnection context, string url, string select = null, string filter = null, string expand = null) where T : IClientSideObject
        {
            var returnValue = ExecuteGetRequest(context, url, select, filter, expand);

            dynamic returnObject = JsonConvert.DeserializeObject<T>(returnValue);

            if (returnObject.GetType().IsGenericType)
            {
                foreach (var item in returnObject.Items)
                {
                    item.Context = context;
                }
            }
            if (returnObject is ClientSideObject)
            {
                ((IClientSideObject)returnObject).Context = context;
            }
            return returnObject;
        }

        public static string ExecuteGetRequest(SPOnlineConnection context, string endPointUrl, string select = null, string filter = null, string expand = null)
        {
            var url = endPointUrl;
            if (!url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                url = context.Url + "/_api/" + endPointUrl;
            }
            var restparams = new System.Collections.Generic.List<string>();
            if (!string.IsNullOrEmpty(select))
            {
                restparams.Add($"$select={select}");
            }
            if (!string.IsNullOrEmpty(filter))
            {
                restparams.Add($"$filter={filter}");
            }
            if (!string.IsNullOrEmpty(expand))
            {
                restparams.Add($"$expand={expand}");
            }
            if (restparams.Any())
            {
                url += $"?{string.Join("&", restparams)}";
            }
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", context.AccessToken);
            var returnValue = client.GetStringAsync(url).GetAwaiter().GetResult();
            return returnValue;
        }

        public static T ExecutePostRequest<T>(SPOnlineConnection context, string url, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null)
        {
            var returnValue = ExecutePostRequestInternal(context, url, null, select, filter, expand);
            return JsonConvert.DeserializeObject<T>(returnValue.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }

        public static T ExecutePostRequest<T>(SPOnlineConnection context, string url, string content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null, string contentType = "application/json")
        {
            HttpContent stringContent = new StringContent(content);
            if (contentType != null)
            {
                stringContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(contentType);
            }

            var returnValue = ExecutePostRequestInternal(context, url, stringContent, select, filter, expand);
            return JsonConvert.DeserializeObject<T>(returnValue.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }

        public static T ExecutePostRequest<T>(SPOnlineConnection context, string url, byte[] content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null)
        {
            var byteArrayContent = new ByteArrayContent(content);
            var returnValue = ExecutePostRequestInternal(context, url, byteArrayContent, select, filter, expand);
            return JsonConvert.DeserializeObject<T>(returnValue.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }

        public static HttpResponseMessage ExecutePostRequest(SPOnlineConnection context, string endPointUrl, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null)
        {
            return ExecutePostRequestInternal(context, endPointUrl, null, select, filter, expand, additionalHeaders);
        }

        public static HttpResponseMessage ExecutePostRequest(SPOnlineConnection context, string endPointUrl, string content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null, string contentType = "application/json")
        {
            HttpContent stringContent = null;
            if (!string.IsNullOrEmpty(content))
            {
                stringContent = new StringContent(content);
            }
            if (contentType != null)
            {
                stringContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(contentType);
            }
            return ExecutePostRequestInternal(context, endPointUrl, stringContent, select, filter, expand, additionalHeaders);
        }

        public static HttpResponseMessage ExecutePostRequest(SPOnlineConnection context, string endPointUrl, byte[] content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null)
        {
            var byteArrayContent = new ByteArrayContent(content);
            return ExecutePostRequestInternal(context, endPointUrl, byteArrayContent, select, filter, expand, additionalHeaders);
        }

        private static HttpResponseMessage ExecutePostRequestInternal(SPOnlineConnection context, string endPointUrl, HttpContent content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null)
        {
            var url = endPointUrl;
            if (!url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                url = context.Url + "/_api/" + endPointUrl;
            }
            var restparams = new System.Collections.Generic.List<string>();
            if (!string.IsNullOrEmpty(select))
            {
                restparams.Add($"$select={select}");
            }
            if (!string.IsNullOrEmpty(filter))
            {
                restparams.Add($"$filter=({filter})");
            }
            if (!string.IsNullOrEmpty(expand))
            {
                restparams.Add($"$expand={expand}");
            }
            if (restparams.Any())
            {
                url += $"?{string.Join("&", restparams)}";
            }

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", context.AccessToken);
            client.DefaultRequestHeaders.Add("X-RequestDigest", GetRequestDigest(context).GetAwaiter().GetResult());

            if (additionalHeaders != null)
            {
                foreach (var key in additionalHeaders.Keys)
                {
                    client.DefaultRequestHeaders.Add(key, additionalHeaders[key]);
                }
            }
            var returnValue = client.PostAsync(url, content).GetAwaiter().GetResult();
            return returnValue;
        }

        #region PUT
        public static T ExecutePutRequest<T>(SPOnlineConnection context, string url, string content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null, string contentType = null)
        {
            HttpContent stringContent = new StringContent(content);
            if (contentType != null)
            {
                stringContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(contentType);
            }

            var returnValue = ExecutePutRequestInternal(context, url, stringContent, select, filter, expand);
            return JsonConvert.DeserializeObject<T>(returnValue.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }

        public static T ExecutePutRequest<T>(SPOnlineConnection context, string url, byte[] content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null)
        {
            var byteArrayContent = new ByteArrayContent(content);
            var returnValue = ExecutePutRequestInternal(context, url, byteArrayContent, select, filter, expand);
            return JsonConvert.DeserializeObject<T>(returnValue.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }


        public static HttpResponseMessage ExecutePutRequest(SPOnlineConnection context, string endPointUrl, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null)
        {
            return ExecutePutRequestInternal(context, endPointUrl, null, select, filter, expand, additionalHeaders);
        }

        public static HttpResponseMessage ExecutePutRequest(SPOnlineConnection context, string endPointUrl, string content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null, string contentType = null)
        {
            HttpContent stringContent = new StringContent(content);
            if (contentType != null)
            {
                stringContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(contentType);
            }
            return ExecutePutRequestInternal(context, endPointUrl, stringContent, select, filter, expand, additionalHeaders);
        }

        public static HttpResponseMessage ExecutePutRequest(SPOnlineConnection context, string endPointUrl, byte[] content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null)
        {
            var byteArrayContent = new ByteArrayContent(content);
            return ExecutePutRequestInternal(context, endPointUrl, byteArrayContent, select, filter, expand, additionalHeaders);
        }

        private static HttpResponseMessage ExecutePutRequestInternal(SPOnlineConnection context, string endPointUrl, HttpContent content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null)
        {
            var url = endPointUrl;
            if (!url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                url = context.Url + "/_api/" + endPointUrl;
            }
            var restparams = new System.Collections.Generic.List<string>();
            if (!string.IsNullOrEmpty(select))
            {
                restparams.Add($"$select={select}");
            }
            if (!string.IsNullOrEmpty(filter))
            {
                restparams.Add($"$filter=({filter})");
            }
            if (!string.IsNullOrEmpty(expand))
            {
                restparams.Add($"$expand={expand}");
            }
            if (restparams.Any())
            {
                url += $"?{string.Join("&", restparams)}";
            }

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", context.AccessToken);
            client.DefaultRequestHeaders.Add("X-RequestDigest", GetRequestDigest(context).GetAwaiter().GetResult());

            if (additionalHeaders != null)
            {
                foreach (var key in additionalHeaders.Keys)
                {
                    client.DefaultRequestHeaders.Add(key, additionalHeaders[key]);
                }
            }
            var returnValue = client.PutAsync(url, content).GetAwaiter().GetResult();
            return returnValue;
        }
        #endregion

        #region MERGE
        public static T ExecuteMergeRequest<T>(SPOnlineConnection context, string url, string content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null, string contentType = null)
        {
            HttpContent stringContent = new StringContent(content);
            if (contentType != null)
            {
                stringContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(contentType);
            }

            var returnValue = ExecuteMergeRequestInternal(context, url, stringContent, select, filter, expand);
            return JsonConvert.DeserializeObject<T>(returnValue.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }

        public static T ExecuteMergeRequest<T>(SPOnlineConnection context, string url, byte[] content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null)
        {
            var byteArrayContent = new ByteArrayContent(content);
            var returnValue = ExecuteMergeRequestInternal(context, url, byteArrayContent, select, filter, expand);
            return JsonConvert.DeserializeObject<T>(returnValue.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }


        public static HttpResponseMessage ExecuteMergeRequest(SPOnlineConnection context, string endPointUrl, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null)
        {
            return ExecuteMergeRequestInternal(context, endPointUrl, null, select, filter, expand, additionalHeaders);
        }

        public static HttpResponseMessage ExecuteMergeRequest(SPOnlineConnection context, string endPointUrl, string content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null, string contentType = null)
        {
            HttpContent stringContent = new StringContent(content);
            if (contentType != null)
            {
                stringContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(contentType);
            }
            return ExecuteMergeRequestInternal(context, endPointUrl, stringContent, select, filter, expand, additionalHeaders);
        }

        public static HttpResponseMessage ExecuteMergeRequest(SPOnlineConnection context, string endPointUrl, byte[] content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null)
        {
            var byteArrayContent = new ByteArrayContent(content);
            return ExecuteMergeRequestInternal(context, endPointUrl, byteArrayContent, select, filter, expand, additionalHeaders);
        }

        private static HttpResponseMessage ExecuteMergeRequestInternal(SPOnlineConnection context, string endPointUrl, HttpContent content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null)
        {
            var url = endPointUrl;
            if (!url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                url = context.Url + "/_api/" + endPointUrl;
            }
            var restparams = new System.Collections.Generic.List<string>();
            if (!string.IsNullOrEmpty(select))
            {
                restparams.Add($"$select={select}");
            }
            if (!string.IsNullOrEmpty(filter))
            {
                restparams.Add($"$filter=({filter})");
            }
            if (!string.IsNullOrEmpty(expand))
            {
                restparams.Add($"$expand={expand}");
            }
            if (restparams.Any())
            {
                url += $"?{string.Join("&", restparams)}";
            }

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", context.AccessToken);
            client.DefaultRequestHeaders.Add("IF-MATCH", "*");
            client.DefaultRequestHeaders.Add("X-RequestDigest", GetRequestDigest(context).GetAwaiter().GetResult());
            client.DefaultRequestHeaders.Add("X-HTTP-Method", "MERGE");
            if (additionalHeaders != null)
            {
                foreach (var key in additionalHeaders.Keys)
                {
                    client.DefaultRequestHeaders.Add(key, additionalHeaders[key]);
                }
            }
            var returnValue = client.PostAsync(url, content).GetAwaiter().GetResult();
            return returnValue;
        }
        #endregion

        #region DELETE

        public static HttpResponseMessage ExecuteDeleteRequest(SPOnlineConnection context, string endPointUrl, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null)
        {
            return ExecuteDeleteRequestInternal(context, endPointUrl, select, filter, expand, additionalHeaders);
        }

        private static HttpResponseMessage ExecuteDeleteRequestInternal(SPOnlineConnection context, string endPointUrl, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null)
        {
            var url = endPointUrl;
            if (!url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                url = context.Url + "/_api/" + endPointUrl;
            }
            var restparams = new System.Collections.Generic.List<string>();
            if (!string.IsNullOrEmpty(select))
            {
                restparams.Add($"$select={select}");
            }
            if (!string.IsNullOrEmpty(filter))
            {
                restparams.Add($"$filter=({filter})");
            }
            if (!string.IsNullOrEmpty(expand))
            {
                restparams.Add($"$expand={expand}");
            }
            if (restparams.Any())
            {
                url += $"?{string.Join("&", restparams)}";
            }

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", context.AccessToken);
            client.DefaultRequestHeaders.Add("X-RequestDigest", GetRequestDigest(context).GetAwaiter().GetResult());
            client.DefaultRequestHeaders.Add("X-HTTP-Method", "DELETE");
            if (additionalHeaders != null)
            {
                foreach (var key in additionalHeaders.Keys)
                {
                    client.DefaultRequestHeaders.Add(key, additionalHeaders[key]);
                }
            }
            var returnValue = client.DeleteAsync(url).GetAwaiter().GetResult();
            return returnValue;
        }
        #endregion
    }

}