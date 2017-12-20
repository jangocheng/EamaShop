using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace EamaShop.Infrastructures
{
    /// <summary>
    /// 表示用于进行 Aes 加密的源字符串
    /// </summary>
    public struct AesSourceString : IEquatable<AesSourceString>
    {
        public AesSourceString(string sourceString) : this()
        {
            SourceString = sourceString ?? throw new ArgumentNullException(nameof(sourceString));
        }

        public string SourceString { get; }
        /// <summary>
        /// 将当前的对象进行Aes加密
        /// </summary>
        /// <param name="key">密钥 长度必须为32位长度字符串</param>
        /// <returns></returns>
        public string Encrypt(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (key.Length != 32)
            {
                throw new ArgumentException("the length of the key used encrpty must be 32", nameof(key));
            }

            if (SourceString == null)
            {
                throw new InvalidOperationException("source string can not be null");
            }

            using (var provider = Aes.Create())
            {
                provider.Key = Encoding.UTF8.GetBytes(key);
                provider.Mode = CipherMode.ECB;
                provider.Padding = PaddingMode.PKCS7;
                using (var transform = provider.CreateEncryptor())
                {
                    var input = Encoding.UTF8.GetBytes(SourceString);
                    var results = transform.TransformFinalBlock(input, 0, input.Length);
                    return Convert.ToBase64String(results, 0, results.Length);
                }
            }
        }

        #region Impl
        public override bool Equals(object obj)
        {
            return obj is AesSourceString && Equals((AesSourceString)obj);
        }

        public bool Equals(AesSourceString other)
        {
            return SourceString == other.SourceString;
        }

        public override int GetHashCode()
        {
            var hashCode = 153273441;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SourceString);
            return hashCode;
        }

        public static bool operator ==(AesSourceString string1, AesSourceString string2)
        {
            return string1.Equals(string2);
        }

        public static bool operator !=(AesSourceString string1, AesSourceString string2)
        {
            return !(string1 == string2);
        } 

        public static implicit operator AesSourceString(string source)
        {
            return new AesSourceString(source);
        }
        public static implicit operator string(AesSourceString source)
        {
            return source.SourceString;
        }
        #endregion
    }
}
