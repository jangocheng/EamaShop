using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EamaShop.Identity.Services
{
    public interface ICreateMerchantService
    {
        Task Create(long uid,string name);
    }
}
