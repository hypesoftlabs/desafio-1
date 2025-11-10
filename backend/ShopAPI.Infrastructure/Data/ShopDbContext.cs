
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions; 
using ShopAPI.Domain.Entities; 

namespace ShopAPI.Infrastructure.Data
{
    public class ShopDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }


        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {
            Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

   
            modelBuilder.Entity<Product>().ToCollection("products");
            modelBuilder.Entity<Category>().ToCollection("categories");
        }
    }
}