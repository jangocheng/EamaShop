using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EamaShop.Basket.API.Infrastructure;
using EamaShop.Identity.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EamaShop.Basket.API.Controllers
{
    /// <summary>
    /// 购物车
    /// </summary>
    [Route("api/basket")]
    public class BasketController : Controller
    {
        private readonly IBasketService _service;
        public BasketController(IBasketService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        /// <summary>
        /// 获取当前用户的购物车信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Commodity> Get()
        {
            return _service.GetBasketByUId(User.GetId());
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
