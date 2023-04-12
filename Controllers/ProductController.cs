using apiDesafio.Models;
using apiDesafio.ViewModel;
using desafio.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;
using static System.Net.WebRequestMethods;

namespace apiDesafio.Controllers
{
    [ApiController]
    [Route("v1")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("products")]
        public async Task<IActionResult> GetAllProducts([FromServices] AppDbContext _context)
        {
            var products = await _context.Products
                .Include(p => p.Categories)
                .ToListAsync();

            var productViewModels = products.Select(p => new ProductListViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                HasPendingLogUpdate = p.HasPendingLogUpdate,
                // CreatedAt = p.CreatedAt,
                Categories = p.Categories.Select(c => new CategoryViewModel { Id = c.Id, Name = c.Name }).ToList()
            });

            return Ok(productViewModels);
        }

        [HttpGet]
        [Route("products/{id}")]
        public async Task<IActionResult> GetProductById(int id, [FromServices] AppDbContext _context)
        {
            var product = await _context.Products
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var productViewModel = new ProductListViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                HasPendingLogUpdate = product.HasPendingLogUpdate,
                Categories = product.Categories.Select(c => new CategoryViewModel { Id = c.Id, Name = c.Name }).ToList()
            };

            return Ok(productViewModel);
        }


        [HttpGet]
        [Route("pending-product-logs")]
        public async Task<ActionResult> GetPendingLogsAsync(
            [FromServices] AppDbContext context)
        {
            var pendingLogs = await context.Products.AsNoTracking().Where(pl => pl.HasPendingLogUpdate).ToListAsync();
            return Ok(pendingLogs);
        }

        //[FromServices] AppDbContext _context
        [HttpPost]
        [Route("product")]
        public async Task<IActionResult> CreateProduct(
            [FromBody] EditorProductViewModel productViewModel,
            [FromServices] AppDbContext _context)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = new ProductModel
            {
                Name = productViewModel.Name,
                Description = productViewModel.Description,
                Price = productViewModel.Price,
                Categories = new List<CategoryModel>(),
                CreatedAt = DateTime.UtcNow,
                HasPendingLogUpdate = true
            };

            foreach (var categoryId in productViewModel.CategoryIds)
            {
                var category = await _context.Categories.FindAsync(categoryId);
                if (category == null)
                {
                    return BadRequest($"Categoria com o ID {categoryId} não existe.");
                }

                product.Categories.Add(category);
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }



        [HttpPut]
        [Route("products/{id}")]
        public async Task<IActionResult> PutAsync(
        [FromServices] AppDbContext context,
        [FromBody] EditorProductViewModel product,
        [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingProduct = await context
                .Products
                .FirstOrDefaultAsync(x => x.Id == id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            try
            {
                existingProduct.Id = existingProduct.Id;
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
               // existingProduct.Categories = product.Categories;
                existingProduct.CreatedAt = existingProduct.CreatedAt;
                existingProduct.HasPendingLogUpdate = true;

                context.Products.Update(existingProduct);
                await context.SaveChangesAsync();

                return Ok(existingProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete]
        [Route("product/{id}")]
        public async Task<IActionResult> DeleteAsync(
        [FromServices] AppDbContext context,
        [FromRoute] int id)
        {
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            try
            {
                context.Products.Remove(product);
                await context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest();
            }
        }
    }

}
