using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EamaShop.Infrastructures.Models
{
    public interface IDistributedSet
    {
        bool Add(string key,byte[] item);

        Task<bool> AddAsync(string key, byte[] item);

        void Clear();
        Task ClearAsync();


    }
}
