using EamaShop.Identity.API.Dto;
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
    /// 授权登陆API
    /// </summary>
    [Produces("application/json")]
    [Route("api/authorize")]
    public class AuthorizeController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly IUserTokenFactory _tokenFactory;
        public AuthorizeController(ILoginService loginService, IUserTokenFactory tokenFactory)
        {
            _loginService = loginService ?? throw new ArgumentNullException(nameof(loginService));

            _tokenFactory = tokenFactory ?? throw new ArgumentNullException(nameof(tokenFactory));
        }
        /// <summary>
        /// 使用jwtBearer授权
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpPost("jwtbearer")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(IDictionary<string, string>))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserToken))]
        public async Task<IActionResult> JwtBearer([FromForm]JwtBearerAuthDto parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _loginService.LoginAsync(parameters.Name, parameters.Password);

            var token = _tokenFactory.CreateToken(user);

            return Ok(token);
        }
    }
}
