using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EamaShop.Catalog.API.Respository;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Net;
using EamaShop.Catalog.API.DTO;
using Microsoft.AspNetCore.Authorization;
using EamaShop.Infrastructures;
using EamaShop.Identity.Common;
using EamaShop.Infrastructures.Enums;

namespace EamaShop.Catalog.API.Controllers
{

    [Produces("application/json")]
    [Route("api/Category")]
    public class CategoryController : Controller
    {
        private ProductContext _context;
        public CategoryController(ProductContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        [HttpGet]
        public async Task<IActionResult> List(long storeId)
        {
            var categories = await _context.Category
                 .Where(x => x.StoreId == storeId)
                 .ToArrayAsync();

            return Ok(categories);
        }
        [HttpPost]
        [Authorize(Roles = nameof(UserRole.Merchant))]
        public async Task<IActionResult> Create([FromBody]CategoryCreateDto parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var parentId = 0L;
            var level = 0;
            if (parameters.ParentId != null)
            {
                var parentCategory = await _context.Category.FirstOrDefaultAsync(x => x.Id == parameters.ParentId);
                if (parentCategory == null)
                {
                    ModelState.AddModelError("ParentId", "no parent category was found");
                    return BadRequest(ModelState);
                }
                parentId = parentCategory.Id;
                level = parentCategory.Level + 1;
            }

            var category = new Category()
            {
                Name = parameters.Name,
                ParentId = parentId,
                StoreId = parameters.StoreId,
                Level = level
            };

            await _context.AddAsync(category, HttpContext.RequestAborted);
            await _context.SaveChangesAsync(HttpContext.RequestAborted);

            return Ok();
        }

    }
}