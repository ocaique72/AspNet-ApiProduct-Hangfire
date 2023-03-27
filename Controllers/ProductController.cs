using apiDesafio.Models;
using apiDesafio.ViewModel;
using desafio.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apiDesafio.Controllers
{
    [ApiController]
    [Route("v1")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("products")]
        public async Task<IActionResult> GetAllAsync(
            //fromServices pegando do dbContext/ database
            [FromServices] AppDbContext context)

        {
            //AsNoTracking nao verifica status dos obj ganhando performace
            var products = await context.Products.AsNoTracking().ToListAsync();
            return Ok(products);
        }

        [HttpGet]
        [Route("product/{id}")]
        public async Task<IActionResult> GetByIdAsync(
            //fromServices pegando do dbContext/ database
            [FromServices] AppDbContext context, [FromRoute] int id)

        {
            //AsNoTracking nao verifica status dos obj ganhando performace
            var product = await context.Products.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return product == null ? NotFound() : Ok(product);

        }

        [HttpGet]
        [Route("pending-product-logs")]
        public async Task<ActionResult> GetPendingLogsAsync(
            [FromServices] AppDbContext context)
        {
            var pendingLogs = await context.Products.AsNoTracking().Where(pl => pl.HasPendingLogUpdate).ToListAsync();
            return Ok(pendingLogs);
        }

        [HttpPost("products")]
        public async Task<IActionResult> CreateAsync(
            [FromServices] AppDbContext context,
            [FromBody] CreateProductViewModel product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var products = new ProductModel
            {
                Id = product.GetHashCode(),
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CreatedAt = DateTime.Now,
                HasPendingLogUpdate = false,
                CategoryId = product.CategoryId,
            };

            try
            {
                //salva na memoria
                await context.Products.AddAsync(products);
                //salva no baco
                await context.SaveChangesAsync();
                //precisa passar parametro para nao dar erro
                return Created($"v1/products/{products.Id}", product);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("products/{id}")]
        public async Task<IActionResult> PutAsync(
        [FromServices] AppDbContext context,
        [FromBody] UpdateProductViewModel product,
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
                existingProduct.CategoryId = product.CategoryId;
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
