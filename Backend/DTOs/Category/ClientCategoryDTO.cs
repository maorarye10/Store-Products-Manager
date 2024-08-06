using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Category
{
    public class ClientCategoryDTO
    {
        [Required, Length(3, 30, ErrorMessage = "Category name must contain at least 3 characters and maximum 30 characters")]
        public string? Name { get; set; }
    }
}
