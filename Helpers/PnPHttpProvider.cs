using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SharePointPnP.PowerShell.Core.Helpers
{
      public class PnPHttpProvider : HttpClient
    {
        readonly int retryCount;
        readonly int delay;
        private string userAgent;

        /// <summary>
        /// Constructor without HttpMessageHandler
        /// </summary>
        /// <param name="retryCount">Number of retries, defaults to 10</param>
        /// <param name="delay">Incremental delay increase in milliseconds</param>
        /// <param name="userAgent">User-Agent string to set</param>
        public PnPHttpProvider(int retryCount = 10, int delay = 500, string userAgent = null) : this(new HttpClientHandler(), retryCount, delay, userAgent)
        {
        }

        /// <summary>
        /// Constructor with HttpMessageHandler
        /// </summary>
        /// <param name="innerHandler">HttpMessageHandler instance to pass along</param>
        /// <param name="retryCount">Number of retries, defaults to 10</param>
        /// <param name="delay">Incremental delay increase in milliseconds</param>
        /// <param name="userAgent">User-Agent string to set</param>
        public PnPHttpProvider(HttpMessageHandler innerHandler, int retryCount = 10, int delay = 500, string userAgent = null) : base(innerHandler)
        {
            this.retryCount = retryCount;
            this.delay = delay;
            this.userAgent = userAgent;
        }

        /// <summary>
        /// Perform async http request
        /// </summary>
        /// <param name="request">Http request to execute</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>Response object from http request</returns>
        public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Retry logic variables
            int retryAttempts = 0;
            int backoffInterval = this.delay;

            // Loop until we need to retry
            while (retryAttempts < this.retryCount)
            {
                try
                {
                    // Add the PnP User Agent string
                   //request.Headers.UserAgent.TryParseAdd(string.IsNullOrEmpty(userAgent) ? $"{PnPCoreUtilities.PnPCoreUserAgent}" : userAgent);

                    // Make the request
                    Task<HttpResponseMessage> result = base.SendAsync(request, cancellationToken);

                    // And return the response in case of success
                    return (result);
                }
                // Or handle any ServiceException
                catch (Exception ex)
                {
                    // Check if the is an InnerException
                    // And if it is a WebException
                    var wex = ex.InnerException as WebException;
                    if (wex != null)
                    {
                        var response = wex.Response as HttpWebResponse;
                        // Check if request was throttled - http status code 429
                        // Check is request failed due to server unavailable - http status code 503
                        if (response != null &&
                            (response.StatusCode == (HttpStatusCode)429 || response.StatusCode == (HttpStatusCode)503))
                        {
                            //Add delay for retry
                            Task.Delay(backoffInterval).Wait();

                            //Add to retry count and increase delay.
                            retryAttempts++;
                            backoffInterval = backoffInterval * 2;
                        }
                        else
                        {
                            throw;
                        }
                    }
                    throw;
                }
            }

            throw new Exception($"Maximum retry attempts {this.retryCount}, has be attempted.");
        }
    }
}