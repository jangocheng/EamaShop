using System;
using System.Collections.Generic;
using System.Text;
using EamaShop.Identity.DataModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace EamaShop.Identity.Services
{
    public class UserTokenFactory : IUserTokenFactory
    {
        public UserToken CreateToken(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var identity = TransformAsClaimIdentity(user);
            var securityTokenDescriptor = CreateDescriptor(user, identity);
            securityTokenDescriptor.Expires = securityTokenDescriptor.Expires
                ?? DateTime.Now.AddDays(7);

            var tokenHandler = new JwtSecurityTokenHandler();

            var stoken = tokenHandler.CreateJwtSecurityToken(securityTokenDescriptor);

            var result = tokenHandler.WriteToken(stoken);

            return new UserToken(result, securityTokenDescriptor.Expires.Value);
        }

        protected virtual ClaimsIdentity TransformAsClaimIdentity(ApplicationUser user)
        {
            var claims = new Claim[]
           {
                new Claim(ClaimTypes.Name,user.AccountName),
                new Claim(ClaimTypes.MobilePhone,user.Phone),
                new Claim(ClaimTypes.PrimarySid,user.Id.ToString()),
                new Claim(ClaimTypes.Sid,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Uri,user.HeadImageUri),
                new Claim(ClaimTypes.GivenName,user.NickName),
                new Claim(ClaimTypes.Role,user.Role.ToString())
           };

            return new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
        }

        protected virtual SecurityTokenDescriptor CreateDescriptor(ApplicationUser user, ClaimsIdentity identity)
        {
            var signKeyBytes = Encoding.ASCII.GetBytes(EamaDefaults.JwtBearerSignKey);

            var sskey = new SymmetricSecurityKey(signKeyBytes);

            var cre = new SigningCredentials(sskey, SecurityAlgorithms.HmacSha256Signature);

            return new SecurityTokenDescriptor()
            {
                Subject = identity,
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = cre,
                Audience = EamaDefaults.Audience,
                Issuer = ClaimsIdentity.DefaultIssuer
            };
        }
    }
}
