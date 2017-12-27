using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EamaShop.Identity.Services
{
    public class VerifyCodeManager : IVerifyCodeManager
    {
        private readonly ILogger _logger;
        private readonly IDistributedCache _cache;
        private readonly IHostingEnvironment _env;
        public VerifyCodeManager(ILogger<VerifyCodeManager> logger,
            IDistributedCache cache,
            IHostingEnvironment hostingEnvironment = null)
        {
            _env = hostingEnvironment;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }
        public async Task<VerifyCode> Create(string target, int expiredInMilliSeconds = 900000, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            using (_logger.BeginScope("Create Verify Code for target :{0}", target))
            {
                var vfc = _env != null && _env.IsDevelopment()
                    ? "123456"
                    : new Random().Next(10).ToString();

                var result = new VerifyCode(this, DateTime.Now.AddMilliseconds(expiredInMilliSeconds), vfc, target);

                await _cache.SetStringAsync(vfc, JsonConvert.SerializeObject(result), cancellationToken);

                return result;
            }
        }

        public async Task<VerifyCode> GetAsync(string content, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            var source = await _cache.GetStringAsync(content, cancellationToken);
            return new VerifyCode(JObject.Parse(source), this);
        }

        public async Task Use(VerifyCode verifyCode, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (verifyCode == null)
            {
                throw new ArgumentNullException(nameof(verifyCode));
            }
            await _cache.SetStringAsync(verifyCode.Content, JsonConvert.SerializeObject(verifyCode), cancellationToken);
        }
    }
}
