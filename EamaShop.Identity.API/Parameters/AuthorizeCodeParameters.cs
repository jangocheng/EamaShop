using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Identity.API.Parameters
{
    /// <summary>
    /// 获取用户授权code凭证接口所需的参数上下文
    /// </summary>
    public class AuthorizeCodeParameters
    {
        /// <summary>
        /// AppId
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string AppId { get; set; }
        /// <summary>
        /// 请求需要授权的权限范围
        /// </summary>
        [Required]
        [MinLength(1)]
        public string[] Scopes { get; set; }
        /// <summary>
        /// 检查该域名是否属于该AppId
        /// </summary>
        [Url]
        public string RedirectUri { get; set; }


    }
}
