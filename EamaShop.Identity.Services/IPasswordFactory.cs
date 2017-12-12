using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Identity.Services
{
    public interface IPasswordFactory
    {
        string FromSource(string source,string salt);
    }
}
