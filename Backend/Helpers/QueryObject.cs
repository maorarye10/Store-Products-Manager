using System.ComponentModel.DataAnnotations;

namespace Backend.Helpers
{
    public class QueryObject
    {
        public string? Name { get; set; }
        public int CategoryId { get; set; }
        public string? SortBy { get; set; }
        public bool IsDecsending { get; set; }
        [RegularExpression("([1-9][0-9]*)")]
        public int PageNumber { get; set; } = 1;
        [RegularExpression("([1-9][0-9]*)")]
        public int PageSize { get; set; } = 20;
    }
}
