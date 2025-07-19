using PetGrubBakcend.ApiResponse;
using PetGrubBakcend.DTOs;

namespace PetGrubBakcend.Services.Cart
{
    public interface ICartService
    {
        Task<ApiResponse<List<CartReadDto>>> GetCartItems(int userId);
        Task<ApiResponse<CartReadDto>> AddToCart(int userId, int productId);

        //Task<ApiResponse<CartReadDto>> AddToCart(AddToCartDto addToCartDto,int userId);

    }
}
