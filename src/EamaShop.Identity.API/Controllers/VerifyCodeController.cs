using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Identity.API.Controllers
{
    /// <summary>
    /// 验证码接口
    /// </summary>
    [Produces("application/json")]
    [Route("api/verifycode")]
    public class VerifyCodeController : Controller
    {
        /// <summary>
        /// 发送验证码给指定的对象
        /// 接口未实现，默认使用验证码123456
        /// </summary>
        /// <param name="name">用户唯一标识符</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(string name)
        {
            await Task.CompletedTask;

            // do post
            return Ok();
        }

        
    }
}
