using System;
using System.Collections.Generic;
using System.Text;
using EamaShop.Identity.DataModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Linq;
using Newtonsoft.Json;

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

            var tokenHandler = new JwtSecurityTokenHandler();

            var stoken = tokenHandler.CreateJwtSecurityToken(securityTokenDescriptor);

            var result = tokenHandler.WriteToken(stoken);

            return new UserToken(result, securityTokenDescriptor.Expires.Value);
        }

        protected virtual ClaimsIdentity TransformAsClaimIdentity(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var properties = user.GetType()
                .GetTypeInfo()
                .GetRuntimeProperties()
                .Where(x => x.GetCustomAttribute<ClaimIgnoreFieldAttribute>() == null)
                .ToArray();

            var claims = new List<Claim>();

            foreach (var p in properties)
            {
                var value = p.GetValue(user);
                if (value == null) continue;
                if (value.GetType().IsPrimitive ||
                    value is string ||
                    value is DateTime ||
                    value is DateTimeOffset ||
                    value is Guid)
                {
                    claims.Add(Create(p.Name, value.ToString(), p.PropertyType.Name));
                }
                else
                {
                    value = JsonConvert.SerializeObject(value);
                    claims.Add(Create(p.Name, value.ToString(), p.PropertyType.Name));
                }
            }

            var roles = user.Role.ToString().Split(',');
            claims.Add(Create(ClaimTypes.Name, user.AccountName, ClaimValueTypes.String));
            Array.ForEach(roles, r =>
            {
                claims.Add(Create(ClaimTypes.Role, r, ClaimValueTypes.String));
            });

            return new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
        }

        private Claim Create(string type, string value, string dataType)
        {
            return new Claim(type, value, dataType, ClaimsIdentity.DefaultIssuer);
        }

        private SecurityTokenDescriptor CreateDescriptor(ApplicationUser user, ClaimsIdentity identity)
        {
            var signKeyBytes = Encoding.ASCII.GetBytes(EamaDefaults.JwtBearerSignKey);
            var sskey = new SymmetricSecurityKey(signKeyBytes);
            var cre = new SigningCredentials(sskey, SecurityAlgorithms.HmacSha256Signature);

            var tokenKeyBytes = Encoding.ASCII.GetBytes(EamaDefaults.JwtBearerTokenKey);
            var tokenKey = new SymmetricSecurityKey(tokenKeyBytes);
            var tokenCre = new EncryptingCredentials(tokenKey,
                SecurityAlgorithms.Aes128KW,
                SecurityAlgorithms.Aes128CbcHmacSha256);

            return new SecurityTokenDescriptor()
            {
                Subject = identity,
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = cre,
                Audience = EamaDefaults.Audience,
                Issuer = ClaimsIdentity.DefaultIssuer,
                EncryptingCredentials = tokenCre
            };
        }


    }
}
