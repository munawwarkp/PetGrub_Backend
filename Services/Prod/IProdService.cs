using PetGrubBakcend.ApiResponse;
using PetGrubBakcend.DTOs;

namespace PetGrubBakcend.Services.Prod
{
    public interface IProdService
    {
        Task<ApiResponse<object>> AddProduct(ProductDto addProductDto);

    }
}
