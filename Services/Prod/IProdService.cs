using PetGrubBakcend.ApiResponse;
using PetGrubBakcend.DTOs;
using PetGrubBakcend.Entities;
using PetGrubBakcend.Result;

namespace PetGrubBakcend.Services.Prod
{
    public interface IProdService
    {
        Task<Result<ProductDto>> AddProductAsync(ProductDto productDto);
        Task<List<ProductReadingDto>> GetProducts();
        Task<ApiResponse<ProductReadingDto>> UpdateProduct(int productId,ProductUpdateDto productUpdateDto);

        Task<Result<ProductReadingDto>> GetProductById(int id);
        Task<List<ProductReadingDto>> GetProductsByCategoryName(string name);

        Task<ProductReadingDto> DeleteProductAsync(int id);
        Task<List<ProductReadingDto>> SearchProducts(string str);


    }
}
