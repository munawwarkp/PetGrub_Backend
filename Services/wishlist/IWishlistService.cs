using System.ComponentModel;
using PetGrubBakcend.ApiResponse;
using PetGrubBakcend.DTOs;
using PetGrubBakcend.Entities;
using PetGrubBakcend.Result;

namespace PetGrubBakcend.Services.wishlist
{
    public interface IWishlistService
    {
        Task<ApiResponse<ProductReadingDto>> AddWishlistIfNotExist(int userId,int productId);
        Task<ApiResponse<List<ProductReadingDto>>> GetWishlistedProduct(int userId);
        Task<ApiResponse<object>> RemoveWishlistedServ(int userId,int productId);

    }
}
