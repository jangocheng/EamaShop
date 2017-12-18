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
using Microsoft.Extensions.Primitives;

namespace EamaShop.Infrastructures.HttpStandard
{
    public class StandardHttpClient<TDescriptor> : IHttpClient<TDescriptor>
        where TDescriptor : MicroserviceDescriptor
    {
        protected HttpClient HttpClient { get; }
        private readonly ILogger _logger;
        public StandardHttpClient(
            TDescriptor microserviceDescriptor,
            ILogger<StandardHttpClient<TDescriptor>> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            MicroserviceDescriptor = microserviceDescriptor ?? throw new ArgumentNullException(nameof(microserviceDescriptor));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            HttpClient = new HttpClient(new SetAuthorizationHandler(httpContextAccessor))
            {
                BaseAddress = MicroserviceDescriptor.Host
            };
            HttpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue()
            {
                NoCache = true,
                NoStore = true
            };
        }
        public TDescriptor MicroserviceDescriptor { get; }

        public Task<HttpResponseMessage> DeleteAsync(string requestUri, CancellationToken cancellationToken = default(CancellationToken))
        => HttpClient.DeleteAsync(requestUri, cancellationToken);


        public Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken cancellationToken = default(CancellationToken))
            => HttpClient.DeleteAsync(requestUri, cancellationToken);

        public Task<HttpResponseMessage> PostAsync<TParameter>(string requestUri, TParameter parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (requestUri == null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }
            using (_logger.BeginScope(requestUri))
            {
                _logger.LogTrace("准备发送请求");
                var content = JsonConvert.SerializeObject(parameters);
                var body = new StringContent(content, Encoding.UTF8, "application/json");

                _logger.LogTrace($"请求正文：{content}");

                return HttpClient.PostAsync(requestUri, body, cancellationToken);
            }
        }

        public Task<HttpResponseMessage> PutAsync<TParameter>(string requestUri, TParameter parameters, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (requestUri == null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }
            var content = JsonConvert.SerializeObject(parameters);
            var body = new StringContent(content, Encoding.UTF8, "application/json");

            return HttpClient.PutAsync(requestUri, body, cancellationToken);
        }

        private class SetAuthorizationHandler : HttpClientHandler
        {
            private readonly IHttpContextAccessor _httpContextAccessor;

            public SetAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
            {
                _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var authorization = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];

                if (!StringValues.IsNullOrEmpty(authorization) && authorization.Count > 1)
                {
                    var schame = authorization[0];
                    var value = authorization[1];
                    request.Headers.Authorization = new AuthenticationHeaderValue(schame, value);
                }
                return base.SendAsync(request, cancellationToken);
            }
        }
    }
}
