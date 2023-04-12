using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace apiDesafio.Models
{
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [JsonIgnore]
        public ICollection<ProductModel> Products { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
        public List<int> ProductIds
        {
            get
            {
                return ProductCategories?.Select(pc => pc.ProductId).ToList();
            }
        }
    }
}
