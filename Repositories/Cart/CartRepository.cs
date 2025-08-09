using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using PetGrubBakcend.Data;
using PetGrubBakcend.DTOs;
using PetGrubBakcend.Entities;

namespace PetGrubBakcend.Repositories.Cart
{
    public class CartRepository:ICartRepesitory
    {
        private readonly AppDbContext _context;
        public CartRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<CartItem>> GetAllCartItem(int userId)
        {
          var cart = await  _context.CartItems
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .ToListAsync();

            return cart;
        }

        public async Task<bool> IsExistCartItem(int productId,int userId)
        {
           return await _context.CartItems.Include(u => u.User).AnyAsync(c => c.ProductId == productId && c.UserId == userId);
        }

        public async Task<bool> GetProduct(int productId)
        {
            //return await _context.Products.AnyAsync(p => p.Id == productId);
            var exists = await _context.Products.Where(p => p.Id == productId)
                .Select(p => p.Id).ToListAsync();

            Console.WriteLine($"found id : {string.Join(",",exists)}");
            return exists.Any();

        }

        public async Task<Product?> GetProductWithId(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            return product;
        }

        public async Task<CartItem?> GetCartItem(int userId,int productId)
        {
            var res =  await _context.CartItems.Include(c => c.User).FirstOrDefaultAsync(c => c.ProductId==productId && c.UserId == userId);

            return res;
        }

        public async Task AddToCart(CartItem cartItem)
        {        
            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
            
        }

        public async Task UpdateCartItem(CartItem cartItem)
        {
            _context.CartItems.Update(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveProductFromCart(CartItem cartItem)
        {
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }
        public async Task IncreaseOrDecreaseQuantity(CartItem cartItem)
        {
           await _context.SaveChangesAsync();
        }
        public async Task ClearCart(int userId)
        {
           
            await _context.Database.ExecuteSqlRawAsync("delete from CartItems where UserId = {0}",userId);
            await _context.SaveChangesAsync();
        }


    }


}
