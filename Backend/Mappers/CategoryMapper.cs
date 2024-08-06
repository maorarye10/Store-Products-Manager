using Backend.DTOs.Category;
using Backend.Models;

namespace Backend.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDTO ToCategoryDTO(this Category category)
        {
            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                ProductsNum = category.Products.Count,
            };
        }

        public static Category ToCategoryModel(this ClientCategoryDTO categoryDTO)
        {
            return new Category
            {
                Name = categoryDTO.Name!
            };
        }
    }
}
