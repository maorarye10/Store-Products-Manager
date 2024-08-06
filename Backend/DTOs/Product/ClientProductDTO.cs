using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Product
{
    public class ClientProductDTO
    {
        [Required, MinLength(3, ErrorMessage = "Product name must contain at least 3 characters")]
        public string Name { get; set; } = string.Empty;
        [Required, RegularExpression("[+]?([0-9]+(?:[\\.][0-9]*)?|\\.[0-9]+)", ErrorMessage = "Product units must be a positive number")]
        public double Price { get; set; }
        [Required, RegularExpression("([1-9][0-9]*)", ErrorMessage = "Product units must be a positive number")]
        public int UnitsInStock { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
