namespace apiDesafio.ViewModel
{
    public class ProductJsonViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public List<string> ProductCategories { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool HasPendingLogUpdate { get; set; }
    }

}
