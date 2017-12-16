using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Identity.Services
{
    /// <summary>
    /// 提供一种将明文密码加密的算法
    /// </summary>
    public interface IPasswordEncryptor
    {
        /// <summary>
        /// 将明文密码和指定盐值混合，加密为密文文本
        /// </summary>
        /// <param name="source"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        string Encrypt(string source,string salt);
    }
}
