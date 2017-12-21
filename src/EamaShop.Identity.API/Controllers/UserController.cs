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
using EamaShop.Infrastructures.Enums;
using System.Security.Claims;
using System.Net;

namespace EamaShop.Identity.API.Controllers
{
    /// <summary>
    /// 用户相关接口
    /// </summary>
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
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(IDictionary<string, string>))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Register([FromForm]UserRegisterDTO parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var service = HttpContext.RequestServices.GetRequiredService<IRegisterService>();

            await service.RegisterAsync(
                account: parameters.AccountName,
                 password: parameters.Password,
                 cancellationToken: HttpContext.RequestAborted,
                 headImageUri: parameters.HeadImageUri,
                 nickName: parameters.NickName);

            return Ok();
        }

        /// <summary>
        /// 获取当前的用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserGetDTO))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(IDictionary<string, string>))]
        public async Task<IActionResult> Details()
        {
            var userInfoService = HttpContext
                .RequestServices
                .GetRequiredService<IUserInfoService>();
            var user = await userInfoService.GetByIdAsync(User.GetId(), HttpContext.RequestAborted);

            if (user == null)
            {
                return NotFound(new { Message = "用户不存在" });
            }

            return Ok(new UserGetDTO(user));
        }
        /// <summary>
        /// 修改用户基础信息
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put([FromForm]UserPutDTO parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userInfoService = HttpContext
                .RequestServices
                .GetRequiredService<IUserInfoService>();

            await userInfoService.EditInfo(HttpContext.User.GetId(), (editor) =>
             {
                 // TODO : is it neccessary using AutoMapper instead?
                 editor.City = parameters.City;
                 editor.Country = parameters.Country;
                 editor.NickName = parameters.NickName;
                 editor.Province = parameters.Province;
                 editor.Sexy = parameters.Sexy;
                 editor.HeadImageUri = parameters.HeadImageUri;
             }, HttpContext.RequestAborted);

            return Ok();
        }
        /// <summary>
        /// 修改密码接口
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPut("password")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> ChangePassword([FromForm]UserPasswordPutDTO parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userInfoService = HttpContext
                .RequestServices
                .GetRequiredService<IUserInfoService>();

            await userInfoService
                 .ChangePasswordAsync(
                id: HttpContext.User.GetId(),
                password: parameters.NewPassword,
                token: parameters.Token,
                cancellationToken: HttpContext.RequestAborted);

            return Ok();
        }
        /// <summary>
        /// 绑定手机号码
        /// </summary>
        /// <returns></returns>
        [HttpPut("phone")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> BindPhone([FromForm]UserPhonePutDTO parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userInfoService = HttpContext
                .RequestServices
                .GetRequiredService<IUserInfoService>();

            await userInfoService
                 .BindPhone(
                id: HttpContext.User.GetId(),
                phone: parameters.Phone,
                verifyCode: parameters.VerifyCode,
                cancellationToken: HttpContext.RequestAborted);

            return Ok();
        }
        /// <summary>
        /// 修改用户角色为商户
        /// </summary>
        /// <returns></returns>
        [HttpPut("role/{id}")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Role(long id)
        {
            var userInfoService = HttpContext
               .RequestServices
               .GetRequiredService<IUserInfoService>();

            if (id <= 0)
            {
                return BadRequest();
            }

            await userInfoService.ChangeRole(id, UserRole.Merchant);

            return Ok();
        }
    }
}