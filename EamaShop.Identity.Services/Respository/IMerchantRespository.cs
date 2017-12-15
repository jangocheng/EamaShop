using EamaShop.Identity.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EamaShop.Identity.Services.Respository
{
    /// <summary>
    /// 商户信息仓储
    /// </summary>
    public interface IMerchantRespository
    {

        Task<Merchant> AddAsync(Merchant merchant, CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<Merchant>> GetByUIdAsync(long uid, CancellationToken cancellationToken = default(CancellationToken));


    }
}
