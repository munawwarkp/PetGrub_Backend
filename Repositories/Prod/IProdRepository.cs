using PetGrubBakcend.ApiResponse;
using PetGrubBakcend.DTOs;

namespace PetGrubBakcend.Repositories.Prod
{
    public interface IProdRepository
    {
        Task<ApiResponse<object>> AddProduct(ProductDto addProductDto);
       


    }
}
