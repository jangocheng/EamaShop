using EamaShop.Identity.Services.Respository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using EamaShop.Identity.DataModel;
using EamaShop.Infrastructures.Enums;
using System.Extensions;

namespace EamaShop.Identity.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IUserRespository _respository;
        private readonly IPasswordEncryptor _passwordEncryptor;
        public RegisterService(IUserRespository respository, IPasswordEncryptor passwordEncryptor)
        {
            _respository = respository ?? throw new ArgumentNullException(nameof(respository));
            _passwordEncryptor = passwordEncryptor ?? throw new ArgumentNullException(nameof(passwordEncryptor));
        }

        public async Task RegisterAsync(string account, 
            string password, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await RegisterAsync(account, "", password, "", cancellationToken);
        }

        public async Task RegisterAsync(string account, string headImageUri, string password, string nickName, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (headImageUri == null)
            {
                throw new ArgumentNullException(nameof(headImageUri));
            }

            if (!headImageUri.IsAbsoluteUriString())
            {
                throw new ArgumentException($"headimage must be a valid uri string :{headImageUri}", nameof(headImageUri));
            }
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            if (nickName == null)
            {
                throw new ArgumentNullException(nameof(nickName));
            }
            cancellationToken.ThrowIfCancellationRequested();

            var hasRegistered = await _respository.Contains(x => x.AccountName == account);

            if (hasRegistered)
            {
                var message = "这个用户抢了你想的用户名呢~~~";
                throw new DomainException(message);
            }
            var salt = Guid.NewGuid().ToString();
            password = _passwordEncryptor.Encrypt(password, salt);

            var user = new ApplicationUser()
            {
                AccountName = account,
                Password = password,
                HeadImageUri = headImageUri,
                Role = UserRole.User,
                Sexy = Gender.Male,
                NickName = nickName,
                Salt = salt
            };

            await _respository.AddAsync(user);

            await _respository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
