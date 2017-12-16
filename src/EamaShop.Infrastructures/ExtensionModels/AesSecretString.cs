using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace EamaShop.Infrastructures.ExtensionModels
{
    /// <summary>
    /// 表示被AES加密后的字符串
    /// </summary>
    public struct AesSecretString : IEquatable<AesSecretString>
    {
        public AesSecretString(string secret) : this()
        {
            Secret = secret ?? throw new ArgumentNullException(nameof(secret));
        }

        public string Secret { get; }
        /// <summary>
        /// 解密当前的密文字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public AesSourceString Decrpty(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (key.Length != 32)
            {
                throw new ArgumentException("the length of the key used encrpty must be 32", nameof(key));
            }
            if (Secret == null)
            {
                throw new InvalidOperationException("secret cannot be null");
            }
            using (var aesProvider = Aes.Create())
            {
                aesProvider.Key = Encoding.UTF8.GetBytes(key);
                aesProvider.Mode = CipherMode.ECB;
                aesProvider.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform cryptoTransform = aesProvider.CreateDecryptor())
                {
                    byte[] inputBuffers = Convert.FromBase64String(Secret);
                    byte[] results = cryptoTransform.TransformFinalBlock(inputBuffers, 0, inputBuffers.Length);
                    aesProvider.Clear();
                    return Encoding.UTF8.GetString(results);
                }
            }
        }

        public override bool Equals(object obj)
        {
            return obj is AesSecretString && Equals((AesSecretString)obj);
        }

        public bool Equals(AesSecretString other)
        {
            return Secret == other.Secret;
        }

        public override int GetHashCode()
        {
            var hashCode = -1399856335;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Secret);
            return hashCode;
        }

        public static bool operator ==(AesSecretString string1, AesSecretString string2)
        {
            return string1.Equals(string2);
        }

        public static bool operator !=(AesSecretString string1, AesSecretString string2)
        {
            return !(string1 == string2);
        }

        public static implicit operator AesSecretString (string secret)
        {
            return new AesSecretString(secret);
        }
        public static implicit operator string(AesSecretString source)
        {
            return source.Secret;
        }
    }
}
