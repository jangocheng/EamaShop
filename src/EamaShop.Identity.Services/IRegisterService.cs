using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EamaShop.Identity.Services
{
    /// <summary>
    /// Provides method for register user.
    /// </summary>
    public interface IRegisterService
    {
        /// <summary>
        /// Register a new user by using given account name and password string.
        /// </summary>
        /// <param name="account">Account name</param>
        /// <param name="password"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="DomainException">The user with given account name has already registered</exception>
        Task RegisterAsync(string account, string password,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
