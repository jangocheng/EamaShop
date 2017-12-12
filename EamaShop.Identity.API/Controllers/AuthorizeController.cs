using EamaShop.Identity.API.Dto;
using EamaShop.Identity.API.Parameters;
using EamaShop.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EamaShop.Identity.API.Controllers
{
    /// <summary>
    /// 第三方平台应用跳转到前端页面，并带上AppId
    /// </summary>
    [Produces("application/json")]
    [Route("api/authorize")]
    [Authorize]
    public class AuthorizeController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly ILoginService _loginService;

        public AuthorizeController(/*IDistributedCache cache, ILoginService loginService*/)
        {
            //_cache = cache ?? throw new ArgumentNullException(nameof(cache));
            //_loginService = loginService ?? throw new ArgumentNullException(nameof(loginService));
        }
        /// <summary>
        /// 使用jwtBearer授权
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("jwtbearer")]
        public async Task<IActionResult> JwtBearer(JwtBearerAuthDto parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //var user = await _loginService.LoginAsync(parameters.Name, parameters.Password);
            var user = new DataModel.ApplicationUser()
            {
                AccountName = "asd",
                Phone = "15056669295",
                HeadImageUri = "asd",
                Email = "asdasda"
            };
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name,user.AccountName),
                new Claim(ClaimTypes.MobilePhone,user.Phone),
                new Claim(ClaimTypes.PrimarySid,user.Id.ToString()),
                new Claim(ClaimTypes.Sid,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Uri,user.HeadImageUri),
            };
            var identity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
            var cre = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(EamaDefaults.JwtBearerSignKey)), SecurityAlgorithms.HmacSha256Signature);
            var s = Guid.NewGuid().ToString();
            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = identity,
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = cre,
                Audience = "api",
                Issuer = "identity"
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateJwtSecurityToken(securityTokenDescriptor);
            var result = tokenHandler.WriteToken(token);
            return Ok(result);
        }

        #region OAuth
        /// <summary>
        /// 需要用户处于登陆状态才能授权；
        /// 获取用户授权code;
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(string))]
        [HttpGet("connect/oauth/code")]
        public async Task<IActionResult> Code([FromQuery]AuthorizeCodeParameters parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // get appid 
            var uri = new Uri(parameters.RedirectUri);

            var code = Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
            await _cache.SetStringAsync(parameters.AppId, code, HttpContext.RequestAborted);


            return Ok(code);
        }
        /// <summary>
        /// 获取用户授权token
        /// </summary>
        /// <param name="appId">应用appId</param>
        /// <param name="appSecret">应用密钥</param>
        /// <param name="code">应用获取到的code</param>
        /// <returns></returns>
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AccessTokenDto))]
        [HttpGet("connection/oauth/accesstoken")]
        public Task<IActionResult> AccessToken(string appId, string appSecret, string code)
        {
            return null;
        }
        #endregion

    }
}
