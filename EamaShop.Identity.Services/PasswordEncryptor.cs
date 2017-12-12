using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Identity.Services
{
    public class PasswordEncryptor : IPasswordEncryptor
    {
        public string Encrypt(string source, string salt)
        {
            // 在这里自定义你的密码组合算法，提升安全性
            // eg. MD5(salt+source+salt)+salt+MD5(source)
            return source + salt;
        }
    }
}
