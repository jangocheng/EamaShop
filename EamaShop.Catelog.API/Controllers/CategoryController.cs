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



    }
}