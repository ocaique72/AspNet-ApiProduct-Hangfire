using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace apiDesafio.Models
{
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [JsonIgnore]
        public ICollection<CategoryModel> Categories { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public DateTime CreatedAt { get; set; } 
        public bool HasPendingLogUpdate { get; set; }
    }
}
