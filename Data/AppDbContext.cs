using Microsoft.EntityFrameworkCore;
using PetGrubBakcend.Entities;

namespace PetGrubBakcend.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> option):base(option) { }

        public DbSet<User> Users {  get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "User" }
                );

            //set decimal precision for the price of the product
            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasColumnType("decimal(18,2)");
        }

    }
}
