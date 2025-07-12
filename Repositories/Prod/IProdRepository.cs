using PetGrubBakcend.ApiResponse;
using PetGrubBakcend.DTOs;
using PetGrubBakcend.Entities;

namespace PetGrubBakcend.Repositories.Prod
{
    public interface IProdRepository
    {
        Task<Product> AddProductAsync(Product product);
        
        //get product
        //Task<List<ProductDto>> GetAllProducts();
        

    }
}
