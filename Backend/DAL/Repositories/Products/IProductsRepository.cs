using Backend.Helpers;
using Backend.Models;

namespace Backend.DAL.Repositories.Products
{
    public interface IProductsRepository
    {
        Task<ProductsQueryResult> GetProductsAsync(QueryObject queryObject);
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product> AddProductAsync(Product product);
        Task<Product?> UpdateProductAsync(int id, Product product);
        Task DeleteProductAsync (int id);
    }
}
