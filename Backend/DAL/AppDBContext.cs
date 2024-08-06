using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Backend.DAL
{
    public class AppDBContext: DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>().HasData(
               new Category
               {
                   Id = 1,
                   Name = "Gaming",
               },
               new Category
               {
                   Id = 2,
                   Name = "Ultrabooks",
               },
               new Category
               {
                   Id = 3,
                   Name = "2-in-1",
               },
               new Category
               {
                   Id = 4,
                   Name = "Budget",
               }
            );
            builder.Entity<Product>().HasData(
               new Product
               {
                   Id = 1,
                   Name = "Asus Zephyrus G14 (2024)",
                   Price = 1499,
                   UnitsInStock = 0,
                   CategoryId = 1,
               },
               new Product
               {
                   Id = 2,
                   Name = "MackBook Air (M3)",
                   Price = 1750.99,
                   UnitsInStock = 15,
                   CategoryId = 2,
               },
               new Product
               {
                   Id = 3,
                   Name = "HP Spectre x360 14'",
                   Price = 1099,
                   UnitsInStock = 3,
                   CategoryId = 3,
               },
               new Product
               {
                   Id = 4,
                   Name = "Acer Aspire 3",
                   Price = 299.99,
                   UnitsInStock = 10,
                   CategoryId = 4,
               }
            );

        }
    }
}
