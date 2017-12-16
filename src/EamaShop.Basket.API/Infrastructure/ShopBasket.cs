using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EamaShop.Basket.API.Infrastructure
{
    /// <summary>
    /// 购物车
    /// </summary>
    public class ShopBasket : ICollection<Commodity>
    {
        public const string USER_BASKET = "user_{0}_shopbasket";
        private readonly RedisHelper _cache;
        private readonly string _key;
        public ShopBasket(RedisHelper cache,
            long uid)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));

        }
        int ICollection<Commodity>.Count => (int)_cache.SetCount(_key);
        bool ICollection<Commodity>.IsReadOnly => false;

        void ICollection<Commodity>.Add(Commodity item)
        {
            _cache.SetAdd(_key, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(item)));
        }

        void ICollection<Commodity>.Clear()
        {
            _cache.SetClear(_key);
        }

        bool ICollection<Commodity>.Contains(Commodity item)
        {
            return _cache.SetContains(_key, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(item)));   
        }

        void ICollection<Commodity>.CopyTo(Commodity[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Commodity> GetEnumerator()
        {

            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        bool ICollection<Commodity>.Remove(Commodity item)
        {
            throw new NotImplementedException();
        }
    }
}
