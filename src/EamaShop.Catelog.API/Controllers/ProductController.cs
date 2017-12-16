using EamaShop.Catalog.API.DTO;
using EamaShop.Catalog.API.Respository;
using EamaShop.Infrastructures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EamaShop.Catalog.API.Controllers
{
    /// <summary>
    /// 商品接口
    /// </summary>
    [Produces("application/json")]
    [Route("api/product")]
    public class ProductController : Controller
    {
        private readonly ProductContext _context;
        public ProductController(ProductContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        #region 创建商品
        /// <summary>
        /// 创建商品
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ProductDto))]
        [Authorize(Roles = "Merchant")]
        [Authorize]
        public async Task<IActionResult> Create(ProductCreateDto parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (parameters.PictureUris
                .Any(x => !Uri.IsWellFormedUriString(x, UriKind.Absolute)))
            {
                string message = "There are pictures which is not a validate uri string";
                ModelState.AddModelError("PictureUris", message);
                return BadRequest(ModelState);
            }

            if (!await _context
                .Category
                .AnyAsync(x => x.Id == parameters.CategoryId && x.StoreId == parameters.StoreId, HttpContext.RequestAborted))
            {
                throw new DomainException("该分类不存在~");
            }

            var product = new Product()
            {
                Description = parameters.Description,
                Name = parameters.Name,
                Properties = JsonConvert.SerializeObject(parameters.Properties),
                StoreId = parameters.StoreId,
                CategoryId = parameters.CategoryId,
                PictureUris = JsonConvert.SerializeObject(parameters.PictureUris),
                Specifications = parameters.Specifications.Select(Selector).ToArray()
            };

            await _context.Product.AddAsync(product, HttpContext.RequestAborted);
            await _context.SaveChangesAsync(HttpContext.RequestAborted);

            var result = new ProductDto(product);
            return Ok(result);
        }

        private static Specification Selector(SpecificationCreateDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            return new Specification()
            {
                Name = dto.Name,
                Price = dto.Price,
                StockCount = dto.StockCount
            };
        }
        #endregion

        #region 获取商品详情信息
        /// <summary>
        /// 获取商品详情信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ProductDto))]
        public async Task<IActionResult> Details(long id)
        {
            var product = await _context.Product
                .Include(x => x.Category)
                .Include(x => x.Specifications)
                .FirstOrDefaultAsync(x => x.Id == id);

            return Ok(new ProductDto(product));
        } 
        #endregion
    }
}
