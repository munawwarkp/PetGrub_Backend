using PetGrubBakcend.ApiResponse;
using PetGrubBakcend.DTOs;
using PetGrubBakcend.Result;

namespace PetGrubBakcend.Services.Prod
{
    public interface IProdService
    {
        Task<Result<ProductDto>> AddProductAsync(ProductDto productDto);

    }
}
