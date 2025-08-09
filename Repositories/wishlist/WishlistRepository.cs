using Microsoft.EntityFrameworkCore;
using PetGrubBakcend.Data;
using PetGrubBakcend.Entities;

namespace PetGrubBakcend.Repositories.wishlist
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly AppDbContext _context;
        public WishlistRepository(AppDbContext context)
        {
            _context = context;

        }

        public async Task<bool> ExistAsync(int wishlistId, int productId)
        {
            return await _context.Wishlist.AnyAsync(w => w.Id == wishlistId && w.ProductId == productId);
        }

        public async Task AddAsync(Wishlist wishlist)
        {
            try
            {
                _context.Add(wishlist);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<List<Product>> GetWishlistedProduct(int userId)
        {
            try
            {
                var res = await _context.Wishlist
                     .Include(w => w.Product)
                     .Where(w => w.UserId == userId)
                     .Select(w => w.Product)
                     .ToListAsync();
                return res;
            }
            catch
            {
                throw;
            }
        }

        public async Task RemoveWishlisted(int userId, int productId)
        {
            var existing = await _context.Wishlist.FirstOrDefaultAsync(w => userId == w.UserId && productId == w.ProductId);

            if (existing != null)
            {
                _context.Wishlist.Remove(existing);
                await _context.SaveChangesAsync();
            }
        }
    }
}
