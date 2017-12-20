using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EamaShop.Ordering.Service;
using EamaShop.Ordering.API.Dto;
using System.Security.Claims;

namespace EamaShop.Ordering.API.Controllers
{
    [Produces("application/json")]
    [Route("api/order")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderFactory<OrderEntity> _factory;
        public OrderController(IOrderFactory<OrderEntity> factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }
        /// <summary>
        /// 下单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Place(OrderPlaceDTO parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(parameters);
            }

            var receiving = new OrderPlaceReceiving(parameters.Country,
                parameters.City,
                parameters.Province,
                parameters.Area,
                parameters.Street,
                parameters.HouseNumber,
                parameters.Receiver,
                parameters.ContactPhone);

            var products = parameters.Products
                .Select(x => new OrderPlaceProduct(x.ProductId, x.SpecificationId));

            var context = new OrderPlaceContext(User.GetId(), receiving, products, parameters.Remarks);
            var order = await _factory.Place(context);
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