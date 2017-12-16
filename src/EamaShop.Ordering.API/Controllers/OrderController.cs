using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EamaShop.Ordering.API.Controllers
{
    [Produces("application/json")]
    [Route("api/order")]
    [Authorize]
    public class OrderController : Controller
    {
        /// <summary>
        /// 下单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Place()
        {
            return Ok();
        }
        /// <summary>
        /// 订单列表获取
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> Details()
        {
            return Ok();
        }
        /// <summary>
        /// 获取物流状态
        /// </summary>
        /// <returns></returns>
        [HttpGet("delivery")]
        public async Task<IActionResult> DeliveryStatus()
        {
            return Ok();
        }


    }
}