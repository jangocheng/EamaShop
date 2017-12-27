using EamaShop.Identity.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
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
        private readonly ISmsSender _smsSender;
        private readonly IUserInfoService _userInfoSvc;
        private readonly IVerifyCodeManager _verifyCodeSvc;
        private readonly IEmailSender _emailSender;
        /// <summary>
        /// Initialize a new <see cref="VerifyCodeController"/> instance.
        /// </summary>
        /// <param name="smsSender"></param>
        /// <param name="userInfoService"></param>
        /// <param name="verifyCodeService"></param>
        /// <param name="emailSender"></param>
        public VerifyCodeController(
            ISmsSender smsSender,
            IUserInfoService userInfoService,
            IVerifyCodeManager verifyCodeService,
            IEmailSender emailSender)
        {
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
            _smsSender = smsSender ?? throw new ArgumentNullException(nameof(smsSender));
            _userInfoSvc = userInfoService ?? throw new ArgumentNullException(nameof(userInfoService));
            _verifyCodeSvc = verifyCodeService ?? throw new ArgumentNullException(nameof(verifyCodeService));
        }
        /// <summary>
        /// 发送验证码给指定的手机号
        /// 接口未实现，默认使用验证码123456
        /// </summary>
        /// <param name="phone">用户的手机号码</param>
        /// <returns></returns>
        [HttpPost("phone")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([Phone][Required]string phone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userInfoSvc.GetByPhoneAsync(phone, HttpContext.RequestAborted);
            if (user == null)
            {
                return BadRequest(new { Message = "用户未注册" });
            }

            var verifyCode = await _verifyCodeSvc.Create(phone);

            await _smsSender.SendAsync(phone, "您的验证码为:" + verifyCode.Content);

            return Ok();
        }
        /// <summary>
        /// 发送邮箱验证码到指定邮箱
        /// 接口未实现，默认使用验证码123456
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost("email")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Email([EmailAddress][Required]string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userInfoSvc.GetByEmailAsync(email, HttpContext.RequestAborted);

            if (user == null)
            {
                return BadRequest(new { Message = "用户未注册" });
            }
            var verifyCode = await _verifyCodeSvc.Create(email);

            await _emailSender.SendAsync(email, "您的验证码为:" + verifyCode.Content);

            return Ok();
        }
    }
}
