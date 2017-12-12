using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EamaShop.Identity.DataModel;
using Microsoft.EntityFrameworkCore;
using EamaShop.Identity.Services.Respository;

namespace EamaShop.Identity.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRespository _respostory;
        private readonly IPasswordEncryptor _passwordFactory;
        public LoginService(IUserRespository respostory, IPasswordEncryptor passwordFactory)
        {
            _respostory = respostory ?? throw new ArgumentNullException(nameof(respostory));
            _passwordFactory = passwordFactory ?? throw new ArgumentNullException(nameof(passwordFactory));
        }
        public async Task<ApplicationUser> LoginAsync(string name, string password)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            var user = await _respostory.FindByIdentifier(name);

            if (user == null)
            {
                throw new DomainException("用户不存在");
            }
            var result = _passwordFactory.Encrypt(password, user.Salt);

            if (user.Password != result)
            {
                throw new DomainException("密码错误");
            }
            user.LastLoginTime = DateTime.Now;
            _respostory.UpdateUser(user);
            await _respostory.UnitOfWork.SaveEntitiesAsync();

            return user;
        }
    }
}
