using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blogg.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blogg.Controllers
{
    [ApiController]
    public class CategoryController : HomeController
    {
        [HttpGet("v1/categories")]
        public async Task<IActionResult> GetTodos([FromServices] BlogDataContext context)
        {
            var cat = await context.Categories.ToListAsync();
            return Ok(cat);
        }
    }
}