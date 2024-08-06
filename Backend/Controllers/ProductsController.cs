using Backend.DAL.Repositories.Categories;
using Backend.DAL.Repositories.Products;
using Backend.DTOs.Product;
using Backend.Helpers;
using Backend.Mappers;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _productsRepository;
        private readonly ICategoriesReposiory _categoriesRepository;
        public ProductsController(IProductsRepository productsRepository, ICategoriesReposiory categoriesReposiory)
        {
            _productsRepository = productsRepository;
            _categoriesRepository = categoriesReposiory;
        }

        //GET OPERATIONS

        //api/products?name="..."&...
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] QueryObject query)
        {
            try
            {
                var productsQueryResult = await _productsRepository.GetProductsAsync(query);
                var productsQueryResultDTO = productsQueryResult.ToProductsQueryResultDTO();
                for (int i = 0; i < productsQueryResultDTO.Products.Count; i++)
                    productsQueryResultDTO.Products[i].CategoryName = productsQueryResult.Products[i].Category?.Name;

                return Ok(productsQueryResultDTO);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Error = e.Message});
            }
        }

        //api/products/8
        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById([FromRoute] int id)
        {
            try
            {
                var product = await _productsRepository.GetProductByIdAsync(id);
                if (product == null)
                    return NotFound();
                var productDTO = product.ToProductDTO();
                productDTO.CategoryName = product.Category?.Name;
                return Ok(productDTO);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Error = e.Message });
            }
        }

        //POST OPERATIONS

        [HttpPost]
        public async Task<IActionResult> AddProduct( [FromBody]ClientProductDTO clientProductDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return UnprocessableEntity(ModelState);

                var isCategoryExists = await _categoriesRepository.IsCategoryExistsAsync(clientProductDTO.CategoryId);
                if (!isCategoryExists)
                    return BadRequest(new { Error = "Category not found" });

                var product = clientProductDTO.ToProductModel();
                await _productsRepository.AddProductAsync(product);
                var productDTO = product.ToProductDTO();
                var category = await _categoriesRepository.GetCategoryByIdAsync(productDTO.CategoryId);
                productDTO.CategoryName = category?.Name;

                return Ok(productDTO);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Error = e.Message });
            }
        }

        // PUT OPERATIONS

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] ClientProductDTO clientProductDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return UnprocessableEntity(ModelState);

                var isCategoryExists = await _categoriesRepository.IsCategoryExistsAsync(clientProductDTO.CategoryId);

                if (!isCategoryExists)
                    return BadRequest(new { Error = "Category not found" });

                var product = clientProductDTO.ToProductModel();
                var updatedProduct = await _productsRepository.UpdateProductAsync(id, product);

                if (updatedProduct == null)
                    return NotFound();

                var productDTO = updatedProduct.ToProductDTO();
                productDTO.CategoryName = updatedProduct.Category?.Name;
                return Ok(productDTO);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Error = e.Message });
            }
        }

        // DELETE OPERATIONS

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            try
            {
                await _productsRepository.DeleteProductAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Error = e.Message });
            }
        }
    }
}
