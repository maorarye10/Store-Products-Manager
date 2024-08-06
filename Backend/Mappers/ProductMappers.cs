using Backend.DTOs.Product;
using Backend.Helpers;
using Backend.Models;

namespace Backend.Mappers
{
    public static class ProductMappers
    {
        public static ProductsQueryResultDTO ToProductsQueryResultDTO(this ProductsQueryResult queryResult)
        {
            return new ProductsQueryResultDTO
            {
                Products = queryResult.Products.Select(p => p.ToProductDTO()).ToList(),
                TotalCount = queryResult.TotalCount
            };
        }
        public static ProductDTO ToProductDTO(this Product product)
        {
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                UnitsInStock = product.UnitsInStock,
                CategoryId = product.CategoryId,
            };
        }

        public static Product ToProductModel(this ClientProductDTO productDTO)
        {
            return new Product
            {
                Name = productDTO.Name,
                Price = productDTO.Price,
                UnitsInStock = productDTO.UnitsInStock,
                CategoryId = productDTO.CategoryId
            };
        }
    }
}
