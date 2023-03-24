namespace apiDesafio.Models
{
    public class ProductLogModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string ProductJson { get; set; }
    }
}
