using PetGrubBakcend.Entities;

namespace PetGrubBakcend.Repositories.Cart
{
    public interface ICartRepesitory
    {
        Task<List<CartItem>> GetAllCartItem(int userId);
        Task<bool> IsExistCartItem(int productId,int userId);
        //Task<bool> IsExistProduct(int productId);


        Task<bool>  GetProduct(int productId);
        Task<Product?> GetProductWithId(int productId);
       Task<CartItem> GetCartItem(int userId, int productId);


        Task AddToCart(CartItem cartItem);
        Task UpdateCartItem(CartItem cartItem);

        Task RemoveProductFromCart(CartItem cartItem);
        Task IncreaseOrDecreaseQuantity(CartItem cartItem);

        Task ClearCart(int userId);

    }
}
