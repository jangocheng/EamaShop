using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EamaShop.Infrastructures.Models
{
    public class RedisSet : IDistributedSet
    {
        
        public bool Add(string key, byte[] item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddAsync(string key, byte[] item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public Task ClearAsync()
        {
            throw new NotImplementedException();
        }
    }
}
