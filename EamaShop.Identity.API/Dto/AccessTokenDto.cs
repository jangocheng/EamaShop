using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Identity.API.Dto
{
    public class AccessTokenDto
    {
        public string AppId { get; set; }

        public string AccessToken { get; set; }
        /// <summary>
        /// Get seconds produces <see cref="AccessToken"/> expired in.
        /// </summary>
        public int ExpiredIn { get; set; }

        public string RefreshToken { get; set; }

        public int RefreshExpiredIn { get; set; }


    }
}
