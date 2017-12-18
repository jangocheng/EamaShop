using EamaShop.Merchant.API.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Merchant.API.DTO
{
    public class MerchantDto:MerchantCreateDto
    {
        public long Id => _store.Id;
        private readonly Store _store;
        public MerchantDto(Store store)
        {
            _store = store ?? throw new ArgumentNullException(nameof(store));
            Name = _store.Name;
            LogoUri = store.LogoUri;
            Description = store.Description;
        }
    }
}
