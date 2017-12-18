using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EamaShop.Identity.Common;
using EamaShop.Merchant.API.DTO;
using EamaShop.Merchant.API.Infrastructures;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Get(long id)
        {
            var store = await _context.FindAsync<Store>(id);

            if (store == null)
            {
                return NotFound();
            }

            return Ok(new MerchantDto(store));
        }

        /// <summary>
        /// 申请创建店铺
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody]MerchantCreateDto value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entry = await _context.AddAsync(new StoreCreateApply()
            {
                Name = value.Name,
                UId = User.GetId(),
                Description = value.Description,
                LogoUri = value.LogoUri,
                IsCreate = true,
                StoreId = null,
                CreateTime = DateTime.Now,
                AuditStatus = AuditStatus.Waiting
            }, HttpContext.RequestAborted);

            await _context.SaveChangesAsync();

            return Ok(new { Message = "您的申请已提交" });
        }
        
    }
}
