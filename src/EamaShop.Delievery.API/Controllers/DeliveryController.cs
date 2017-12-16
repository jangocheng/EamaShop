using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EamaShop.Delievery.API.Controllers
{
    /// <summary>
    /// 后端微服务接口，前端不应该调用，此接口未发布对外访问，有关详情信息，请在 api gateway kong中进行配置
    /// </summary>
    [Route("api/delivery")]
    public class DeliveryController : Controller
    {
        
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            return Ok();
        }
        
        [HttpGet]
        public async Task<IActionResult> Details()
        {
            return Ok();
        }

        
    }
}
