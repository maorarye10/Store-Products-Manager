using Backend.Models;

namespace Backend.Helpers
{
    public class ProductsQueryResult
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public int TotalCount { get; set; }
    }
}
