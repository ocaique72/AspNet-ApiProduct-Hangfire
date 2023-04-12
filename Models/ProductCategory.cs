using Hangfire.Server;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace apiDesafio.Models
{
    public class ProductCategory
    {
        [Key, Column(Order = 0)]
        public int ProductId { get; set; }
        [Key, Column(Order = 1)]
        public int CategoryId { get; set; }   
        public ProductModel Product { get; set; }
        public CategoryModel Category { get; set; }

    }
}