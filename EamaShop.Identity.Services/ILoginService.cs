using EamaShop.Identity.DataModel;
using System;
using System.Threading.Tasks;

namespace EamaShop.Identity.Services
{
    public interface ILoginService
    {
        Task<ApplicationUser> LoginAsync(string name,string password);
    }
}
