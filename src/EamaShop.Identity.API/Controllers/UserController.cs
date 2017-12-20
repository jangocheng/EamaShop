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
        public async Task<IActionResult> Register([FromBody]UserRegisterDTO parameters)
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
        public IActionResult Details()
        {
            return Ok(User.Claims.ToDictionary(x => x.Type, x => x.Value));
        }
        /// <summary>
        /// 修改用户基础信息
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UserPutDTO parameters)
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
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPut("password")]
        public async Task<IActionResult> ChangePassword([FromBody]UserPasswordPutDTO parameters)
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
        public async Task<IActionResult> BindPhone([FromBody]UserPhonePutDTO parameters)
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
    }
}