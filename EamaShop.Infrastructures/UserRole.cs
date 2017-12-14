using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Infrastructures
{
    [Flags]
    public enum UserRole
    {
        User = 1,
        Merchant = 4,
        Admin = 6
    }
}
