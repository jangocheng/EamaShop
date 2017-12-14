using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EamaShop.Identity.API.Dto;
using Microsoft.Extensions.DependencyInjection;
using EamaShop.Identity.Services;

namespace EamaShop.Identity.API.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    [Authorize]
    public class UserController : Controller
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var service = HttpContext.RequestServices.GetRequiredService<IRegisterService>();

            await service.RegisterAsync(
                account: parameters.AccountName,
                 password: parameters.Password);

            return Ok();
        }

        /// <summary>
        /// 获取详情信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details()
        {

            return Ok();
        }
    }
}