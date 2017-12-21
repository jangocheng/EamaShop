using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EamaShop.Identity;
using EamaShop.Infrastructures;
using EamaShop.Infrastructures.Events;
using EamaShop.Merchant.API.DTO;
using EamaShop.Merchant.API.Infrastructures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EamaShop.Merchant.API.Controllers
{
    /// <summary>
    /// 店铺申请单据操作接口
    /// </summary>
    [Route("api/store/apply")]
    public class StoreApplyController : Controller
    {
        private readonly MerchantContext _context;
        private readonly IDistributedCache _cache;
        public StoreApplyController(MerchantContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get(int pageIndex = 1, int pageSize = 20)
        {
            return Ok(await _context.StoreCreateApply.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToArrayAsync());
        }

        /// <summary>
        /// 获取指定的单据详情信息
        /// </summary>
        /// <param name="id">单据的编号</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var apply = await _context.StoreCreateApply.FindAsync(id);
            return Ok(apply);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(StoreApplyPostDTO parameters)
        {
            var apply = new StoreCreateApply()
            {
                AuditStatus = EamaShop.Infrastructures.Enums.AuditStatus.Waiting,
                CreateTime = DateTime.Now,
                StoreDescription = parameters.StoreDescription,
                StoreName = parameters.StoreName,
                StoreLogoUri = parameters.StoreLogoUri,
                UId = User.GetId(),
                Scopes = JsonConvert.SerializeObject(parameters.Scopes),
                Manager = User.GetNickName()
            };

            await _context.AddAsync(apply, HttpContext.RequestAborted);
            await _context.SaveChangesAsync(HttpContext.RequestAborted);
            return Ok();
        }

        /// <summary>
        /// 审核指定的店铺创建申请
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut]
        [Authorize(Roles = nameof(EamaShop.Infrastructures.Enums.UserRole.Admin))]
        public async Task<IActionResult> Audit([FromForm]StoreApplyPutDTO parameters)
        {
            var apply = await _context.StoreCreateApply
                .SingleOrDefaultAsync(x => x.Id == parameters.AuditId, HttpContext.RequestAborted);

            if (apply == null)
            {
                return NotFound(new { Message = "申请单未找到" });
            }
            // do read and write
            var provider = HttpContext.RequestServices.GetRequiredService<IDistributedLockProvider>();
            var locker = provider.GetLock(parameters.AuditId.ToString());
            if (!await locker.EnterAsync(TimeSpan.FromSeconds(8), HttpContext.RequestAborted))
            {
                return Accepted(new { Message = "当前申请单已经被其他操作员处理" });
            }
            try
            {
                if (parameters.Agree)
                {
                    apply.AuditStatus = EamaShop.Infrastructures.Enums.AuditStatus.Allowed;

                    var store = new Store()
                    {
                        CreateTime = DateTime.Now,
                        Description = apply.StoreDescription,
                        LogoUri = apply.StoreLogoUri,
                        Manager = apply.Manager,
                        UId = apply.UId,
                        Name = apply.StoreName,
                        Status = EamaShop.Infrastructures.Enums.StoreStatus.Closed,
                        Scopes = apply.Scopes
                    };
                    // 创建店铺
                    await _context.Store.AddAsync(store);
                    var client = HttpContext.RequestServices.GetRequiredService<IIdentityClient>();
                    await client.ApiUserRoleByIdPutAsync(apply.UId, "");
                }
                else
                {
                    apply.AuditStatus = EamaShop.Infrastructures.Enums.AuditStatus.NotAllowed;
                }
                apply.AuditTime = DateTime.Now;
                apply.Reason = parameters.Reason;
                await _context.SaveChangesAsync(HttpContext.RequestAborted);
            }
            finally
            {
                await locker.ExitAsync();
            }

            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
