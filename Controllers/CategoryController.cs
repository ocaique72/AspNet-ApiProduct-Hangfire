
using apiDesafio.Models;
using apiDesafio.ViewModel;
using desafio.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace apiDesafio.Controllers
{
    [ApiController]
    [Route("v1")]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        //isso seria o FromServices
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/categories")]
        public async Task<IActionResult> GetCategoriesWithProductIds(
            [FromServices] AppDbContext _context)
        {
            var categories = await _context.Categories
                .Include(c => c.Products)
                .Select(c => new {
                    Id = c.Id,
                    Name = c.Name,
                    ProductIds = c.Products.Select(p => p.Id).ToList()
                })
                .ToListAsync();

            return Ok(categories);
        }

        [HttpGet]
        [Route("categories/{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }


        [HttpPost]
        [Route("categories")]
        public async Task<IActionResult> CreateCategory(
            [FromBody] CreateCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = new CategoryModel
            {
                Name = model.Name,
                CreatedAt = DateTime.UtcNow

            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return Ok(category);
        }

        [HttpPut]
        [Route("categories/{id}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] UpdateCategoryViewModel category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var existingCategory = await _context.Categories.FindAsync(id);
            if (existingCategory == null)
            {
                return NotFound();
            }
            try
            {
                existingCategory.Id = existingCategory.Id;
                existingCategory.Name = category.Name;
                existingCategory.CreatedAt = existingCategory.CreatedAt;
                existingCategory.UpdatedAt = existingCategory.UpdatedAt;

                _context.Categories.Update(existingCategory);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }


        }

        [HttpDelete]
        [Route("categories/{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
