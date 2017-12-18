using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EamaShop.Identity.Common;
using EamaShop.Merchant.API.DTO;
using EamaShop.Merchant.API.Infrastructures;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EamaShop.Merchant.API.Controllers
{
    [Route("api/[controller]")]
    public class MerchantController : Controller
    {
        private readonly MerchantContext _context;
        public MerchantController(MerchantContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [ResponseCache(CacheProfileName = "default")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(MerchantDto))]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var store = await _context.FindAsync<Store>(id);

            if (store == null)
            {
                return NotFound();
            }

            return Ok(new MerchantDto(store));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        [ResponseCache(CacheProfileName = "default")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(MerchantDto))]
        public async Task<IActionResult> Create([FromBody]MerchantCreateDto value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entry = await _context.AddAsync(new Store()
            {
                Name = value.Name,
                UId = User.GetId()
            }, HttpContext.RequestAborted);

            return Ok(new MerchantDto(entry.Entity));
        }
    }
}
