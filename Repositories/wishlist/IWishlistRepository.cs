using PetGrubBakcend.Entities;

namespace PetGrubBakcend.Repositories.wishlist
{
    public interface IWishlistRepository
    {
        Task<bool> ExistAsync(int wishlistId,int productId);

        Task AddAsync(Wishlist wishlist);

        Task<List<Product>> GetWishlistedProduct(int userId);

        Task RemoveWishlisted(int userId,int productId);
    }
}
