using apiDesafio.Models;
using apiDesafio.ViewModel;
using Dapper;
using desafio.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace apiDesafio.Controllers
{

    // CODIGO FUNCIONANDO COM ENTITY FRAMEWORK
    [ApiController]
    [Route("v1")]
    [Authorize]
    public class ProductLogController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductLogController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("product-logs")]
        public async Task<ActionResult> GetAllAsync()
        {
            var productLogs = await _context.ProductLogs.AsNoTracking().ToListAsync();
            return Ok(productLogs);
        }

        [HttpGet]
        [Route("product-logs/{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var productLog = await _context.ProductLogs.FindAsync(id);
            if (productLog == null)
            {
                return NotFound();
            }
            return Ok(productLog);
        }

         [HttpPost]
         [Route("product-logs")]
         public async Task<ActionResult> CreateAsync([FromBody] EditorProductLogViewModel productLog)
         {
             if (!ModelState.IsValid)
             {
                 return BadRequest();
             }

             var productLogs = new ProductLogModel
             {
                 Id = productLog.GetHashCode(),
                 ProductId = productLog.ProductId,
                 ProductJson = productLog.ProductJson,
                 UpdatedAt = DateTime.Now
             };

             await _context.ProductLogs.AddAsync(productLogs);
             await _context.SaveChangesAsync();

             return Created($"v1/product-logs/{productLogs.Id}", productLogs);
         }
        

       /* [HttpPost]
        [Route("product-logs")]
        public async Task<ActionResult> CreateAsync([FromBody] CreateProductLogViewModel productLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            using (var connection = new SqlConnection("DataSource=app.db;Cache=Shared"))
            {
                var productLogs = new ProductLogModel
                {
                    Id = productLog.GetHashCode(),
                    ProductId = productLog.ProductId,
                    ProductJson = productLog.ProductJson,
                    UpdatedAt = DateTime.Now
                };

                var sql = @"INSERT INTO ProductLogs (Id, ProductId, ProductJson, UpdatedAt) 
                    VALUES (@Id, @ProductId, @ProductJson, @UpdatedAt)";

                await connection.ExecuteAsync(sql, productLogs);

                return Created($"v1/product-logs/{productLogs.Id}", productLogs);
            }
        }
       */

        [HttpPut]
        [Route("product-logs/{id}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] EditorProductLogViewModel productLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingProductLog = await _context.ProductLogs.FindAsync(id);
            if (existingProductLog == null)
            {
                return NotFound();
            }
            existingProductLog.Id = existingProductLog.Id;
            existingProductLog.ProductId = productLog.ProductId;
            existingProductLog.UpdatedAt = DateTime.Now;
            existingProductLog.ProductJson = productLog.ProductJson;

            _context.ProductLogs.Update(existingProductLog);
            await _context.SaveChangesAsync();

            return Ok();
        }

       /* [HttpDelete]
        [Route("product-logs/{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var productLog = await _context.ProductLogs.FindAsync(id);
            if (productLog == null)
            {
                return NotFound();
            }
            _context.ProductLogs.Remove(productLog);
            await _context.SaveChangesAsync();
            return Ok();
        } */



        //dapper
         [HttpDelete]
         [Route("product-logs/{id}")]
         public async Task<ActionResult> DeleteAsync(int id)
         {
            using (var connection = new SQLiteConnection("DataSource=app.db;Cache=Shared"))
            {
                var productLog = await connection.QueryFirstOrDefaultAsync<ProductLogModel>("SELECT * FROM ProductLogs WHERE Id = @Id", new { Id = id });
                if (productLog == null)
                {
                    return NotFound();
                }
                await connection.ExecuteAsync("DELETE FROM ProductLogs WHERE Id = @Id", new { Id = id });
                return Ok();
            }
        } 

    }

}