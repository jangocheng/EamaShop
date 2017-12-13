using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Infrastructures
{
    [Flags]
    public enum UserRole
    {
        User,
        UserAndMerchant
    }
}
