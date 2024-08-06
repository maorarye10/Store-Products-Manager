namespace Backend.DTOs.Product
{
    public class ProductsQueryResultDTO
    {
        public List<ProductDTO> Products { get; set; } = new List<ProductDTO>();
        public int TotalCount { get; set; }
    }
}
