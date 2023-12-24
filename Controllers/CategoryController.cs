using Blogg.Data;
using Blogg.Models;
using Blogg.ViewModels;
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
            try
            {
                var cat = await context.Categories.ToListAsync();
                return Ok(new ResultViewModel<List<Category>>(cat));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("05X04 - Falha interna no servidor"));
            }

        }

        [HttpGet("v1/categories/{id:int}")]
        public async Task<IActionResult> GetById(
            [FromRoute] int id,
            [FromServices] BlogDataContext context)
        {
            try
            {
                var cat = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (cat == null)
                    return NotFound(new ResultViewModel<Category>("Conteudo não encontrado"));

                return Ok(new ResultViewModel<Category>(cat));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("05X04 - Falha interna no servidor"));

            }
        }

        [HttpPost("v1/categories/")]
        public async Task<IActionResult> PostAsyncs(
            [FromBody] EditorCategoryViewModel model,
            [FromServices] BlogDataContext context
        )
        {

            try
            {
                if (!ModelState.IsValid) // Verifica a validação do modelo
                {
                    return BadRequest(ModelState);
                }

                var category = new Category
                {
                    Id = 0,
                    Name = model.Name,
                    Slug = model.Slug.ToLower(),
                };

                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                return Created($"v1/categories/{category.Id}", category);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, ex.InnerException);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, "Falha interna no servidor");
            }
        }

        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
               [FromBody] EditorCategoryViewModel model,
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
                [FromServices] BlogDataContext context)
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