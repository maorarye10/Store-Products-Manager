using Backend.Helpers;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.DAL.Repositories.Products
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly AppDBContext _context;
        public ProductsRepository(AppDBContext context)
        {
            _context = context;
        }
        //ADD
        public async Task<Product> AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        //DELETE
        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product != null)
            {
                _context.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        //GET
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public async Task<ProductsQueryResult> GetProductsAsync(QueryObject queryObject)
        {
            var query = _context.Products.AsQueryable();

            //filtering
            if (!string.IsNullOrWhiteSpace(queryObject.Name))
                query = query.Where(p => p.Name.Contains(queryObject.Name));
            if (queryObject.CategoryId > 0)
                query = query.Where(p => p.CategoryId == queryObject.CategoryId);

            //sorting
            if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
            {
                if (queryObject.SortBy.Equals(nameof(Product.Name), StringComparison.OrdinalIgnoreCase))
                    query = queryObject.IsDecsending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name);
                if (queryObject.SortBy.Equals(nameof(Product.Price), StringComparison.OrdinalIgnoreCase))
                    query = queryObject.IsDecsending ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price);
                if (queryObject.SortBy.Equals(nameof(Product.UnitsInStock), StringComparison.OrdinalIgnoreCase))
                    query = queryObject.IsDecsending ? query.OrderByDescending(p => p.UnitsInStock) : query.OrderBy(p => p.UnitsInStock);
                if (queryObject.SortBy.Equals("categoryName", StringComparison.OrdinalIgnoreCase))
                    query = queryObject.IsDecsending ? query.OrderByDescending(p => p.Category!.Name) : query.OrderBy(p => p.Category!.Name);
            }
            else if (queryObject.IsDecsending)
                query = query.OrderByDescending(p => p.Id);

            var totalCount = await query.CountAsync();

            //Pagination
            var skipNum = (queryObject.PageNumber - 1) * queryObject.PageSize;
            var products = await query.Skip(skipNum).Take(queryObject.PageSize).Include(p => p.Category).ToListAsync();


            var productsQueryResult = new ProductsQueryResult
            {
                Products = products,
                TotalCount = totalCount,
            };
            return productsQueryResult;
        }

        //UPDATE
        public async Task<Product?> UpdateProductAsync(int id, Product product)
        {
            var foundProduct = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (foundProduct != null)
            {
                foundProduct.Name = product.Name;
                foundProduct.Price = product.Price;
                foundProduct.CategoryId = product.CategoryId;
                foundProduct.UnitsInStock = product.UnitsInStock;

                await _context.SaveChangesAsync();
            }

            return foundProduct;
        }
    }
}
