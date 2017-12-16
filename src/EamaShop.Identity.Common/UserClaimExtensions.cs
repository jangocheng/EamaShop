using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace EamaShop.Identity.Common
{
    public static class UserClaimExtensions
    {
        public static bool TryGetId(this ClaimsPrincipal principal, out long id)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }
            id = 0;
            if (principal.Claims == null)
            {
                return false;
            }
            var source = principal.Claims.FirstOrDefault(x => x.Type == "Id");

            if (source == null)
            {
                return false;
            }
            return long.TryParse(source.Value, out id);
        }

        public static long GetId(this ClaimsPrincipal principal)
        {
            if(!principal.TryGetId(out var id))
            {
                throw new InvalidOperationException("no id was found");
            }
            return id;
        }
    }
}
