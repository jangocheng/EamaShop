using EamaShop.Infrastructures.ExtensionModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace System.Extensions
{
    public static class ClrExtensions
    {
        public static AesSourceString AsAesSourceString(this string source)
        {
            return source;
        }

        public static AesSecretString AsAesSecretString(this string secret)
        {
            return secret;
        }
    }
}
