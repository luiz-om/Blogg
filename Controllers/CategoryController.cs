using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blogg.Data;
using Blogg.Models;
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

        [HttpGet("v1/categories/{id:int}")]
        public async Task<IActionResult> GetById(
            [FromRoute] int id,
            [FromServices] BlogDataContext context)
        {
            var cat = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (cat == null)
                return NotFound();


            return Ok(cat);
        }

        [HttpPost("v1/categories/")]
        public async Task<IActionResult> PostAsync(
            [FromBody] Category model,
            [FromServices] BlogDataContext context
        )
        {
            try
            {
                await context.Categories.AddAsync(model);
                await context.SaveChangesAsync();

                return Created($"v1/categories/{model.Id}", model);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Não foi possível incluir categoria");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, "Falha interna no servidor");
            }
        }

        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
               [FromBody] Category model,
            [FromServices] BlogDataContext context
        )
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (category == null)
                    return NotFound();

                category.Name = model.Name;
                category.Slug = model.Slug;
                context.Categories.Update(category);

                await context.SaveChangesAsync();

                return Ok(category);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Não foi possível alterar categoria");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, "Falha interna no servidor");
            }
        }
        [HttpDelete("v1/categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
                   [FromRoute] int id,
                    [FromServices] BlogDataContext context
               )
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (category == null)
                    return NotFound();

                context.Categories.Remove(category);

                await context.SaveChangesAsync();

                return Ok(category);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Não foi possível excluir categoria");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, "Falha interna no servidor");
            }
        }
    }
}