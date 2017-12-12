using EamaShop.Catelog.API.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Catelog.API.Controllers
{
    /// <summary>
    /// 商品接口
    /// </summary>
    [Produces("application/json")]
    [Route("api/product")]
    [Authorize]
    public class ProductController:Controller
    {
        /// <summary>
        /// 创建商品
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDto parameters)
        {
            return Ok();
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            return Ok();
        }
    }
}
