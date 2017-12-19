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
using EamaShop.Identity.Common;
using EamaShop.Infrastructures.Enums;

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
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]UserRegisterDto parameters)
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
        public IActionResult Details()
        {
            return Ok(User.Claims.ToDictionary(x => x.Type, x => x.Value));
        }
        [HttpPut("role")]
        public async Task<IActionResult> Role([FromBody]IEnumerable<UserRole> roles)
        {
            var service = HttpContext.RequestServices.GetRequiredService<IRoleService>();

            await service.ChangeRole(User.GetId(), roles.ToArray());

            return Ok();
        }
    }
}