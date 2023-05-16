namespace apiDesafio.ViewModel
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ProductListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool HasPendingLogUpdate { get; set; }
        //public DateTime CreatedAt { get; set; }
        // DateTime UpdatedAt { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        //public List<int> CategoryIds { get; set; }
    }
}
