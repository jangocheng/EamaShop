using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EamaShop.Identity.Services
{
    /// <summary>
    /// Provides methods for creating,checking,validating verifycode.
    /// </summary>
    public interface IVerifyCodeManager
    {
        /// <summary>
        /// Create a new verifyCode
        /// </summary>
        /// <returns></returns>
        Task<VerifyCode> Create(string target, int expiredInMilliSeconds = 1000 * 60 * 15, CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// Get a existing verifycode by content.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        Task<VerifyCode> GetAsync(string content, CancellationToken cancellationToken = default(CancellationToken));

        Task Use(VerifyCode verifyCode, CancellationToken cancellationToken = default(CancellationToken));
    }
}
