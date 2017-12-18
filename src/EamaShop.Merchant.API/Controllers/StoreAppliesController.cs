using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EamaShop.Merchant.API.Infrastructures;
using Microsoft.AspNetCore.Authorization;
using EamaShop.Merchant.API.DTO;

namespace EamaShop.Merchant.API.Controllers
{
    [Produces("application/json")]
    [Route("api/StoreApplies")]
    public class StoreAppliesController : Controller
    {
        private readonly MerchantContext _context;

        public StoreAppliesController(MerchantContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 获取申请单列表 仅获取前10条
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<StoreCreateApply> GetStoreCreateApply()
        {
            return _context.StoreCreateApply.Where(x => x.AuditStatus == AuditStatus.Waiting).Take(10);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStoreCreateApply([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var storeCreateApply = await _context.StoreCreateApply.SingleOrDefaultAsync(m => m.Id == id);

            if (storeCreateApply == null)
            {
                return NotFound(new { Message = "找不到该申请单" });
            }

            return Ok(storeCreateApply);
        }
        /// <summary>
        /// 用于ERP管理的审核接口
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Audit(MerchantAuditDTO parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var apply = await _context.FindAsync<StoreCreateApply>(parameters.AuditId);

            if (apply == null)
            {
                return NotFound(new { Message = "找不到该申请单" });
            }

            apply.AuditTime = DateTime.Now;
            apply.AuditStatus = parameters.Agree ? AuditStatus.Allowed : AuditStatus.NotAllowed;
            apply.Reason = parameters.Reason;
            _context.Update(apply);
            if (apply.IsCreate)
            {
                var store = new Store()
                {
                    Description = apply.Description,
                    LogoUri = apply.LogoUri,
                    Name = apply.Name,
                    UId = apply.UId
                };
                await _context.AddAsync(store);
            }
            else
            {
                var store = await _context.FindAsync<Store>(apply.StoreId);

                store.Description = apply.Description;
                store.LogoUri = apply.LogoUri;
                store.Name = apply.Name;
                _context.Update(store);
            }
            await _context.SaveChangesAsync();

            return Ok();
        }
        
        private bool StoreCreateApplyExists(long id)
        {
            return _context.StoreCreateApply.Any(e => e.Id == id);
        }
    }
}