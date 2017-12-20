using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace System.Security.Claims
{
    /// <summary>
    /// Extensions for claim identity and principal
    /// </summary>
    [DebuggerStepThrough]
    public static class ClaimIdentityExtensions
    {
        /// <summary>
        /// Try get a user data name of 'Id' in current principal.
        /// </summary>
        /// <param name="principal">The principal which provides values</param>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool TryGetId(this ClaimsPrincipal principal, out long id, string name = "Id")
        {
            id = 0;
            var result = principal.FindFirstValue(name);
            if (result == null)
            {
                return false;
            }
            return long.TryParse(result, out id);
        }
        /// <summary>
        /// Get user's Id.
        /// a exception was thrown when 
        /// </summary>
        /// <param name="principal"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static long GetId(this ClaimsPrincipal principal, string name = "Id")
        {
            if(!principal.TryGetId(out var result, name))
            {
                throw new InvalidOperationException($"there are no id was found name of {name}");
            }
            return result;
        }
        public static bool TryGetNickName(this ClaimsPrincipal principal, out string nickName, string name = "NickName")
        {
            nickName = null;
            var result = principal.FindFirstValue(name);
            if (result == null)
            {
                return false;
            }
            nickName = result;
            return true;
        }
        public static string GetNickName(this ClaimsPrincipal principal, string name = "NickName")
        {
            if (!principal.TryGetNickName(out var result, name))
            {
                throw new InvalidOperationException($"there are no id was found name of {name}");
            }
            return result;
        }
    }
}
