using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SharePointPnP.PowerShell.Core.Base;

namespace SharePointPnP.PowerShell.Core.Helpers
{
    public static class RestHelper
    {
        public static async Task<string> GetRequestDigest(string url = null)
        {
            if(url == null)
            {
                url = SPOnlineConnection.Url;
            }
            using (var handler = new HttpClientHandler())
            {
                string responseString = string.Empty;
                var accessToken = SPOnlineConnection.AccessToken;

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

        public static T ExecuteGetRequest<T>(string url, string select = null, string filter = null, string expand = null)
        {
            var returnValue = ExecuteGetRequest(url, select, filter, expand);
            return JsonConvert.DeserializeObject<T>(returnValue);
        }

        public static string ExecuteGetRequest(string endPointUrl, string select = null, string filter = null, string expand = null)
        {
            var url = endPointUrl;
            if (!url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                url = SPOnlineConnection.Url + "/_api/" + endPointUrl;
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
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", SPOnlineConnection.AccessToken);
            var returnValue = client.GetStringAsync(url).GetAwaiter().GetResult();
            return returnValue;
        }

        public static T ExecutePostRequest<T>(string url, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null)
        {
            var returnValue = ExecutePostRequestInternal(url, null, select, filter, expand);
            return JsonConvert.DeserializeObject<T>(returnValue.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }

        public static T ExecutePostRequest<T>(string url, string content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null, string contentType = "application/json")
        {
            HttpContent stringContent = new StringContent(content);
            if (contentType != null)
            {
                stringContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(contentType);
            }

            var returnValue = ExecutePostRequestInternal(url, stringContent, select, filter, expand);
            return JsonConvert.DeserializeObject<T>(returnValue.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }

        public static T ExecutePostRequest<T>(string url, byte[] content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null)
        {
            var byteArrayContent = new ByteArrayContent(content);
            var returnValue = ExecutePostRequestInternal(url, byteArrayContent, select, filter, expand);
            return JsonConvert.DeserializeObject<T>(returnValue.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }

        public static HttpResponseMessage ExecutePostRequest(string endPointUrl, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null)
        {
            return ExecutePostRequestInternal(endPointUrl, null, select, filter, expand, additionalHeaders);
        }

        public static HttpResponseMessage ExecutePostRequest(string endPointUrl, string content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null, string contentType = "application/json")
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
            return ExecutePostRequestInternal(endPointUrl, stringContent, select, filter, expand, additionalHeaders);
        }

        public static HttpResponseMessage ExecutePostRequest(string endPointUrl, byte[] content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null)
        {
            var byteArrayContent = new ByteArrayContent(content);
            return ExecutePostRequestInternal(endPointUrl, byteArrayContent, select, filter, expand, additionalHeaders);
        }

        private static HttpResponseMessage ExecutePostRequestInternal(string endPointUrl, HttpContent content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null)
        {
            var url = endPointUrl;
            if (!url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                url = SPOnlineConnection.Url + "/_api/" + endPointUrl;
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
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", SPOnlineConnection.AccessToken);
            client.DefaultRequestHeaders.Add("X-RequestDigest", GetRequestDigest().GetAwaiter().GetResult());

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
        public static T ExecutePutRequest<T>(string url, string content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null, string contentType = null)
        {
            HttpContent stringContent = new StringContent(content);
            if (contentType != null)
            {
                stringContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(contentType);
            }

            var returnValue = ExecutePutRequestInternal(url, stringContent, select, filter, expand);
            return JsonConvert.DeserializeObject<T>(returnValue.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }

        public static T ExecutePutRequest<T>(string url, byte[] content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null)
        {
            var byteArrayContent = new ByteArrayContent(content);
            var returnValue = ExecutePutRequestInternal(url, byteArrayContent, select, filter, expand);
            return JsonConvert.DeserializeObject<T>(returnValue.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }


        public static HttpResponseMessage ExecutePutRequest(string endPointUrl, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null)
        {
            return ExecutePutRequestInternal(endPointUrl, null, select, filter, expand, additionalHeaders);
        }

        public static HttpResponseMessage ExecutePutRequest(string endPointUrl, string content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null, string contentType = null)
        {
            HttpContent stringContent = new StringContent(content);
            if (contentType != null)
            {
                stringContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(contentType);
            }
            return ExecutePutRequestInternal(endPointUrl, stringContent, select, filter, expand, additionalHeaders);
        }

        public static HttpResponseMessage ExecutePutRequest(string endPointUrl, byte[] content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null)
        {
            var byteArrayContent = new ByteArrayContent(content);
            return ExecutePutRequestInternal(endPointUrl, byteArrayContent, select, filter, expand, additionalHeaders);
        }

        private static HttpResponseMessage ExecutePutRequestInternal(string endPointUrl, HttpContent content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null)
        {
            var url = endPointUrl;
            if (!url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                url = SPOnlineConnection.Url + "/_api/" + endPointUrl;
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
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", SPOnlineConnection.AccessToken);
            client.DefaultRequestHeaders.Add("X-RequestDigest", GetRequestDigest().GetAwaiter().GetResult());

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
        public static T ExecuteMergeRequest<T>(string url, string content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null, string contentType = null)
        {
            HttpContent stringContent = new StringContent(content);
            if (contentType != null)
            {
                stringContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(contentType);
            }

            var returnValue = ExecuteMergeRequestInternal(url, stringContent, select, filter, expand);
            return JsonConvert.DeserializeObject<T>(returnValue.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }

        public static T ExecuteMergeRequest<T>(string url, byte[] content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null)
        {
            var byteArrayContent = new ByteArrayContent(content);
            var returnValue = ExecuteMergeRequestInternal(url, byteArrayContent, select, filter, expand);
            return JsonConvert.DeserializeObject<T>(returnValue.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }


        public static HttpResponseMessage ExecuteMergeRequest(string endPointUrl, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null)
        {
            return ExecuteMergeRequestInternal(endPointUrl, null, select, filter, expand, additionalHeaders);
        }

        public static HttpResponseMessage ExecuteMergeRequest(string endPointUrl, string content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null, string contentType = null)
        {
            HttpContent stringContent = new StringContent(content);
            if (contentType != null)
            {
                stringContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(contentType);
            }
            return ExecuteMergeRequestInternal(endPointUrl, stringContent, select, filter, expand, additionalHeaders);
        }

        public static HttpResponseMessage ExecuteMergeRequest(string endPointUrl, byte[] content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null)
        {
            var byteArrayContent = new ByteArrayContent(content);
            return ExecuteMergeRequestInternal(endPointUrl, byteArrayContent, select, filter, expand, additionalHeaders);
        }

        private static HttpResponseMessage ExecuteMergeRequestInternal(string endPointUrl, HttpContent content, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null)
        {
            var url = endPointUrl;
            if (!url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                url = SPOnlineConnection.Url + "/_api/" + endPointUrl;
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
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", SPOnlineConnection.AccessToken);
            client.DefaultRequestHeaders.Add("IF-MATCH","*");
            client.DefaultRequestHeaders.Add("X-RequestDigest", GetRequestDigest().GetAwaiter().GetResult());
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

        public static HttpResponseMessage ExecuteDeleteRequest(string endPointUrl, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null)
        {
            return ExecuteDeleteRequestInternal(endPointUrl, select, filter, expand, additionalHeaders);
        }

        private static HttpResponseMessage ExecuteDeleteRequestInternal(string endPointUrl, string select = null, string filter = null, string expand = null, Dictionary<string, string> additionalHeaders = null)
        {
            var url = endPointUrl;
            if (!url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                url = SPOnlineConnection.Url + "/_api/" + endPointUrl;
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
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", SPOnlineConnection.AccessToken);
            client.DefaultRequestHeaders.Add("X-RequestDigest", GetRequestDigest().GetAwaiter().GetResult());
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