﻿namespace Backend.DTOs.Category
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ProductsNum { get; set; }
    }
}
