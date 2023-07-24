using System;
using System.Linq;
using Newtonsoft.Json;
using System.Threading;
using apiDesafio.Models;
using Microsoft.EntityFrameworkCore;
using desafio.Data;


public class ProductUpdateChecker
{
    public static async Task ProductLogUpdateJobAsync()
    {
        try
        {
            using (var dbContext = new AppDbContext())
            {
                var productsToUpdate = await dbContext.Products.Where(p => p.HasPendingLogUpdate).ToListAsync();
                foreach (var product in productsToUpdate)
                {
                    Console.Write(productsToUpdate);
                    var productLog = new ProductLogModel
                    {
                        ProductId = product.Id,
                        UpdatedAt = DateTime.Now,
                        ProductJson = JsonConvert.SerializeObject(product)
                    };

                    dbContext.ProductLogs.Add(productLog);
                    product.HasPendingLogUpdate = false;

                    await dbContext.SaveChangesAsync();
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error ao tentar integrar produto { e.Message}");
        }
    }


}

