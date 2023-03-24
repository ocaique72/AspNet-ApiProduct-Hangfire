
using apiDesafio.Models;
using apiDesafio.ViewModel;
using desafio.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [Route("categories")]
        public async Task<ActionResult> GetAllAsync()
        {
            var categories = await _context.Categories.AsNoTracking().ToListAsync();
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
        public async Task<ActionResult> CreateAsync([FromBody] CreateCategoryViewModel category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var categorys = new CategoryModel
            {
                Id = category.GetHashCode(),
                Name = category.Name,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _context.Categories.AddAsync(categorys);
            await _context.SaveChangesAsync();

            return Created($"v1/categories", category);
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
