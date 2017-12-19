using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Ordering.Service
{
    public interface IOrderProductProvider
    {
        /// <summary>
        /// Get products by given ids.
        /// </summary>
        /// <param name="identifiers"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        bool TryGetProducts(long[] identifiers,out IEnumerable<ProductDto> product);
    }
}
