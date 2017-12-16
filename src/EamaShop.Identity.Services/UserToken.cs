using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Identity.Services
{
    public struct UserToken
    {
        public UserToken(string token, DateTime expiredIn) : this()
        {
            Token = token ?? throw new ArgumentNullException(nameof(token));
            ExpiredIn = expiredIn;
        }
        /// <summary>
        /// 用户访问的授权token
        /// </summary>
        public string Token { get; }

        public DateTime ExpiredIn { get; }

        public override string ToString()
        {
            return Token;
        }
    }
}
