using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net;
using Newtonsoft.Json;

namespace EamaShop.Infrastructures.HttpStandard
{
    public class StandardHttpClient : IHttpClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<StandardHttpClient> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly DiagnosticListener _diagnostic;
        public StandardHttpClient(ILogger<StandardHttpClient> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _client = new HttpClient();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            //_diagnostic = diagnostic ?? throw new ArgumentNullException(nameof(diagnostic));
        }

        Task<HttpResponseMessage> IHttpClient.DeleteAsync(string requestUri,
              string authorizationToken,
              string authorizationMethod,
              CancellationToken cancellationToken)
        {
            if (requestUri == null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }
            cancellationToken.ThrowIfCancellationRequested();
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, requestUri);

            using (var scope = _logger.BeginScope(requestMessage))
            {

                SetHeaders(requestMessage);

                if (authorizationToken != null)
                {
                    requestMessage.Headers.Authorization =
                        new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
                }

                return _client.SendAsync(requestMessage, cancellationToken);
            }
        }

        Task<HttpResponseMessage> IHttpClient.GetAsync(string requestUri, string authorizationToken, string authorizationMethod, CancellationToken cancellationToken)
        {
            if (requestUri == null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }
            cancellationToken.ThrowIfCancellationRequested();
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

            using (var scope = _logger.BeginScope(requestMessage))
            {

                SetHeaders(requestMessage);

                if (authorizationToken != null)
                {
                    requestMessage.Headers.Authorization =
                        new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
                }

                return _client.SendAsync(requestMessage, cancellationToken);
            }
        }

        Task<HttpResponseMessage> IHttpClient.PostAsync<Parameter>(string requestUri, Parameter parameter, string authorizationToken, string authorizationMethod, CancellationToken cancellationToken)
        {
            if (requestUri == null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }
            cancellationToken.ThrowIfCancellationRequested();
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri);
            requestMessage.Content =
                new StringContent(JsonConvert.SerializeObject(parameter), Encoding.UTF8, "application/json");
            using (var scope = _logger.BeginScope(requestMessage))
            {

                SetHeaders(requestMessage);

                if (authorizationToken != null)
                {
                    requestMessage.Headers.Authorization =
                        new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
                }

                return _client.SendAsync(requestMessage, cancellationToken);
            }
        }

        Task<HttpResponseMessage> IHttpClient.PutAsync< Parameter>(string requestUri, Parameter parameter, string authorizationToken, string authorizationMethod, CancellationToken cancellationToken)
        {
            if (requestUri == null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }
            cancellationToken.ThrowIfCancellationRequested();
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, requestUri);
            requestMessage.Content =
                new StringContent(JsonConvert.SerializeObject(parameter), Encoding.UTF8, "application/json");
            using (var scope = _logger.BeginScope(requestMessage))
            {

                SetHeaders(requestMessage);

                if (authorizationToken != null)
                {
                    requestMessage.Headers.Authorization =
                        new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
                }

                return _client.SendAsync(requestMessage, cancellationToken);
            }
        }

        private void SetHeaders(HttpRequestMessage requestMessage)
        {
            var authorization = _httpContextAccessor
                .HttpContext.Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(authorization))
            {
                requestMessage.Headers.Add("Authorization", new List<string> { authorization });
            }

            requestMessage.Headers.CacheControl = new CacheControlHeaderValue()
            {
                NoCache = true,
                NoStore = true
            };
        }
    }
}
