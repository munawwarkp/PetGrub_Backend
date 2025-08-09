using Microsoft.EntityFrameworkCore;
using PetGrubBakcend.DTOs;
using PetGrubBakcend.Entities;

namespace PetGrubBakcend.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> option):base(option) { }

        public DbSet<User> Users {  get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Wishlist> Wishlist { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<AddressUser> Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

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

            modelBuilder.Entity<User>()
                .Property(u => u.IsBlocked)
                .HasDefaultValue(false);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Wishlist>()
                .HasOne(w => w.User)
                .WithMany(u => u.Wishlist)
                .HasForeignKey(w => w.UserId);

            modelBuilder.Entity<Wishlist>()
                .HasOne(w => w.Product)
                .WithMany(p => p.wishlists)
                .HasForeignKey(w => w.ProductId);

            modelBuilder.Entity<Wishlist>()
                .HasIndex(w => new { w.UserId, w.ProductId })
                .IsUnique();
            //modelBuilder.Entity<CartReadDto>()
            //    .HasNoKey();

            //modelBuilder.Entity<CartReadDto>()
            //    .Property(c => c.Price)
            //    .HasColumnType("decimal(18,2)");

            //modelBuilder.Entity<CartReadDto>()
            //    .Property(c => c.TotalPrice)
            //    .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<CartItem>()
                .HasOne(c => c.User)
                .WithMany(u => u.CartItems)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<CartItem>()
                .HasOne(c => c.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(c => c.ProductId);

            modelBuilder.Entity<AddressUser>()
                .HasOne(a => a.User)
                .WithMany(u => u.Address)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.ShippingAddress)
                .WithMany(a => a.Orders)
                .HasForeignKey(o => o.AddressId)
                .OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2);           
            modelBuilder.Entity<OrderItem>()
                .HasOne(oItem => oItem.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oItem => oItem.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(o => o.Product)
                .WithMany()
                .HasForeignKey(o => o.ProductId);
            modelBuilder.Entity<OrderItem>()
                .Property(o => o.UnitPrice)
                .HasPrecision(18,2);
            modelBuilder.Entity<OrderItem>()
                .Property(o => o.TotalPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderReadDto>().HasNoKey().ToView(null); //for using raw sql








            ////avoid cascade delete issue
            //modelBuilder.Entity<Order>()
            //    .HasOne(o => o.User)
            //    .WithMany()
            //    .OnDelete(deleteBehavior: DeleteBehavior.Restrict);
            //modelBuilder.Entity<Order>()
            //    .HasOne(o => o.ShippingAddress)
            //    .WithMany()
            //    .OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<OrderItem>()
            //    .HasOne(o => o.Product)
            //    .WithMany()
            //    .OnDelete(DeleteBehavior.Restrict);


            //modelBuilder.Entity<AddToCartDto>()
            //    .Property(c => c.Quantity)
            //    .HasDefaultValue(1);
            //modelBuilder.Entity<AddToCartDto>()
            //    .HasNoKey();

            //modelBuilder.Entity<CartReadResponseDto>()
            //    .Property(c => c.TotalCartPrice)
            //    .HasColumnType("decimal(18,2)");
        }

    }
}
