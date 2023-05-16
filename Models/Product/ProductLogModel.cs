using System.ComponentModel.DataAnnotations;

namespace apiDesafio.Models
{
    public class ProductLogModel
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string ProductJson { get; set; }
    }
}
