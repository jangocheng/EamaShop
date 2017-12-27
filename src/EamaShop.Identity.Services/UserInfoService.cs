using EamaShop.Identity.DataModel;
using EamaShop.Identity.Services.Respository;
using EamaShop.Infrastructures.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EamaShop.Identity.Services
{
    public class UserInfoService : IUserInfoService
    {
        private readonly ILogger<UserInfoService> _logger;
        private readonly IUserRespository _respository;
        private readonly IPasswordEncryptor _passwordEncryptor;
        public UserInfoService(ILogger<UserInfoService> logger,
            IUserRespository respository,
            IPasswordEncryptor passwordEncryptor)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _respository = respository ?? throw new ArgumentNullException(nameof(respository));
            _passwordEncryptor = passwordEncryptor ?? throw new ArgumentNullException(nameof(passwordEncryptor));
        }

        public async Task BindPhone(long id, string phone, string verifyCode, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (phone == null)
            {
                throw new ArgumentNullException(nameof(phone));
            }

            if (verifyCode == null)
            {
                throw new ArgumentNullException(nameof(verifyCode));
            }
            if (!new PhoneAttribute().IsValid(phone))
            {
                throw new ArgumentException($"invalid phone number :{phone}", nameof(phone));
            }
            cancellationToken.ThrowIfCancellationRequested();
            // TODO: check verify code is valid.

            var user = await _respository.FindById(id, cancellationToken);

            if (user == null)
            {
                throw new DomainException("用户不存在");
            }
            _logger.LogTrace("用户绑定手机号操作");

            user.Phone = phone;
            _respository.UpdateUser(user);
            await _respository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        }

        public async Task ChangePasswordAsync(
            long id,
            string password,
            string token,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }
            // TODO: check token and use it.

            var user = await _respository.FindById(id);

            if (user == null)
            {
                throw new DomainException("用户不存在");
            }
            _logger.LogInformation("用户{0}修改了密码", id);

            password = _passwordEncryptor.Encrypt(password, user.Salt);
            user.Password = password;
            _respository.UpdateUser(user);

            await _respository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }

        public async Task ChangeRole(long id, UserRole role)
        {
            var user = await _respository.FindById(id);

            if (user == null)
            {
                throw new DomainException("用户不存在");
            }
            user.Role = user.Role | role;
            _respository.UpdateUser(user);
            await _respository.UnitOfWork.SaveEntitiesAsync();
        }

        public async Task EditInfo(long id, Action<UserEditor> configure,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _respository.FindById(id, cancellationToken);
            if (user == null)
            {
                throw new DomainException("用户不存在");
            }

            using (_logger.BeginScope("用户{0}提交修改用户基础信息的请求,时间{1}", id, DateTime.Now))
            {
                var editor = new EFUserEditor(user, _logger);

                configure(editor);

                _respository.UpdateUser(user);

                await _respository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            }
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (email == null)
            {
                throw new ArgumentNullException(nameof(email));
            }

            return await _respository.FindByIdentifier(email);
        }

        public Task<ApplicationUser> GetByIdAsync(long id, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            return _respository.FindById(id, cancellationToken);
        }

        public async Task<ApplicationUser> GetByPhoneAsync(string phone, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (phone == null)
            {
                throw new ArgumentNullException(nameof(phone));
            }

            return await _respository.FindByIdentifier(phone);
        }

        private class EFUserEditor : UserEditor
        {
            private readonly ApplicationUser _metadata;
            private readonly ILogger _logger;
            public EFUserEditor(ApplicationUser user, ILogger logger)
            {
                _metadata = user ?? throw new ArgumentNullException(nameof(user));
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            }

            public override string NickName
            {
                set
                {
                    var beforeName = _metadata.NickName;
                    _metadata.NickName = value ?? throw new ArgumentNullException(nameof(value));
                    TraceBehavior(nameof(NickName), beforeName, value);
                }
            }
            public override string HeadImageUri
            {
                set
                {
                    if (value == null)
                    {
                        throw new ArgumentNullException(nameof(value));
                    }
                    if (!Uri.IsWellFormedUriString(value, UriKind.Absolute))
                    {
                        throw new InvalidOperationException($"head image uri must be a absolute uri string. value:{value}");
                    }
                    var before = _metadata.HeadImageUri;
                    _metadata.HeadImageUri = value;
                    TraceBehavior(nameof(HeadImageUri), before, value);
                }
            }
            public override Gender Sexy
            {
                set
                {
                    TraceBehavior(nameof(Sexy), _metadata.Sexy, value);
                    _metadata.Sexy = value;
                }
            }
            public override string Country
            {
                set
                {
                    TraceBehavior(nameof(Country), _metadata.Country, value);
                    _metadata.Country = value;
                }
            }
            public override string City
            {
                set
                {
                    TraceBehavior(nameof(City), _metadata.City, value);
                    _metadata.City = value;
                }
            }
            public override string Province
            {
                set
                {
                    TraceBehavior(nameof(Province), _metadata.Province, value);
                    _metadata.Province = value;
                }
            }
            private void TraceBehavior(string callsite, object before, object after)
            {
                _logger.LogTrace("用户修改了{0},修改前：{1}，修改后：{2}", callsite, before, after);
            }
        }
    }
}
