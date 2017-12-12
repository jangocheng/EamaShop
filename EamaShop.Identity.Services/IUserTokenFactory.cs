using EamaShop.Identity.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Identity.Services
{
    public interface IUserTokenFactory
    {
        UserToken CreateToken(ApplicationUser user);
    }
}
