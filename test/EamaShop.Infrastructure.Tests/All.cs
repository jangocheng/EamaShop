using EamaShop.Identity.Common;
using EamaShop.Infrastructures;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Extensions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace EamaShop.Infrastructure.Tests
{
    public class All
    {
        [Fact]
        public void Test1()
        {
            // 1 , 11,001

            UserRole role = UserRole.User | UserRole.Merchant | UserRole.Admin;
            if ((role & UserRole.Merchant) != 0)
            {

            }
            var newrole= role | UserRole.Merchant;
            var nnRole = role ^ UserRole.User;
            var nnnRole = nnRole & UserRole.User;
            var ff = role.HasFlag(UserRole.Merchant);
            var role2 = role & UserRole.Merchant;
            var i = (int)role;
            var s = role.ToString();
            var flag = Enum.TryParse<UserRole>(s, out var result);
            var newer = (UserRole)i;
            var a = (object)null;
            //var key = "12121212121212121212121212121212";
            //var source = "this is source string";

            //var secret = source.AsAesSourceString().Encrypt(key);

            //var ss = secret.AsAesSecretString().Decrpty(key);


            //var signKeyBytes = Encoding.ASCII.GetBytes(EamaDefaults.JwtBearerSignKey);
            //var sskey = new SymmetricSecurityKey(signKeyBytes);
            //var cre = new SigningCredentials(sskey, SecurityAlgorithms.HmacSha256Signature);

            //var tokenKeyBytes = Encoding.ASCII.GetBytes(EamaDefaults.JwtBearerTokenKey);
            //var tokenKey = new SymmetricSecurityKey(tokenKeyBytes);
            //var tokenCre = new EncryptingCredentials(tokenKey,
            //    SecurityAlgorithms.Aes128KW,
            //    SecurityAlgorithms.Aes128CbcHmacSha256);

            //var desc = new SecurityTokenDescriptor()
            //{
            //    Subject = new ClaimsIdentity(),
            //    IssuedAt = DateTime.UtcNow,
            //    Expires = DateTime.UtcNow.AddDays(7),
            //    SigningCredentials = cre,
            //    Audience = EamaDefaults.Audience,
            //    Issuer = ClaimsIdentity.DefaultIssuer,
            //    EncryptingCredentials = tokenCre
            //};
            //var tokenHandler = new JwtSecurityTokenHandler();

            //var stoken = tokenHandler.CreateJwtSecurityToken(desc);

            //var result = tokenHandler.WriteToken(stoken);
        }
    }
}