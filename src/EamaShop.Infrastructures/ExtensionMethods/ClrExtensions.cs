using EamaShop.Infrastructures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace System.Extensions
{
    [DebuggerStepThrough]
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

        public static bool IsAbsoluteUriString(this string source)
        {
            return Uri.IsWellFormedUriString(source, UriKind.Absolute);
        }
    }
}
