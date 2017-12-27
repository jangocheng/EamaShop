using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Identity.Services
{
    public class VerifyCode
    {
        private readonly IVerifyCodeManager _verifyCodeSvc;
        public VerifyCode(JObject json, IVerifyCodeManager verifyCodeService)
        {
            if (json == null)
            {
                throw new ArgumentNullException(nameof(json));
            }
            _verifyCodeSvc = verifyCodeService ?? throw new ArgumentNullException(nameof(verifyCodeService));
            ExpiredIn = json.Value<DateTime>(nameof(ExpiredIn));
            Content = json.Value<string>(nameof(Content));
            Target = json.Value<string>(nameof(Target));
            Used = json.Value<bool>(nameof(Used));
        }

        public VerifyCode(IVerifyCodeManager verifyCodeSvc, DateTime expiredIn, string content, string target)
        {
            _verifyCodeSvc = verifyCodeSvc ?? throw new ArgumentNullException(nameof(verifyCodeSvc));
            ExpiredIn = expiredIn;
            Content = content ?? throw new ArgumentNullException(nameof(content));
            Target = target ?? throw new ArgumentNullException(nameof(target));
            Used = false;
        }

        public DateTime ExpiredIn { get; }

        public string Content { get; }

        public string Target { get; }

        public bool Used { get; private set; }

        public void EnsureValidate(string target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (Used)
            {
                throw new DomainException("验证码已经被使用过了");
            }
            if (ExpiredIn < DateTime.Now)
            {
                throw new DomainException("验证码已过期");
            }
            if (!target.Equals(Target))
            {
                throw new DomainException("验证码无效");
            }
        }

        public void Use()
        {
            Used = true;
            _verifyCodeSvc.Use(this);
        }
    }
}
