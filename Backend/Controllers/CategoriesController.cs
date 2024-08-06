using Backend.DAL.Repositories.Categories;
using Backend.DTOs.Category;
using Backend.Mappers;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesReposiory _categoriesReposiory;
        public CategoriesController(ICategoriesReposiory categoriesReposiory)
        {
            _categoriesReposiory = categoriesReposiory;
        }

        //GET OPERATIONS

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await _categoriesReposiory.GetCategoriesAsync();
                var categoriesDTOs = categories.Select(c => c.ToCategoryDTO());
                return Ok(categoriesDTOs);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Error = e.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] int id)
        {
            try
            {
                var category = await _categoriesReposiory.GetCategoryByIdAsync(id);

                if (category == null)
                    return NotFound();

                var categoryDTO = category.ToCategoryDTO();
                return Ok(categoryDTO);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Error = e.Message });
            }
        }

        // POST OPERATIONS

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] ClientCategoryDTO clientCategoryDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return UnprocessableEntity(ModelState);

                var category = clientCategoryDTO.ToCategoryModel();
                await _categoriesReposiory.AddCategoryAsync(category);

                var categoryDTO = category.ToCategoryDTO();
                return Ok(categoryDTO);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Error = e.Message });
            }
        }

        //PUT OPERATIONS

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] ClientCategoryDTO clientCategoryDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return UnprocessableEntity(ModelState);

                var category = clientCategoryDTO.ToCategoryModel();
                var updatedCategory = await _categoriesReposiory.UpdateCategoryAsync(id, category);

                if (updatedCategory == null)
                    return NotFound();

                var categoryDTO = updatedCategory.ToCategoryDTO();
                return Ok(categoryDTO);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Error = e.Message });
            }
        }

        //DELETE OPERATIONS

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            try
            {
                await _categoriesReposiory.DeleteCategoryAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Error = e.Message });
            }
        }
    }
}
