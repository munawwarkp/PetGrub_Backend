using PetGrubBakcend.ApiResponse;
using PetGrubBakcend.DTOs;
using PetGrubBakcend.Repositories.Prod;

namespace PetGrubBakcend.Services.Prod
{
    public class ProdService:IProdService
    {
        private readonly IProdRepository _prodRepository;
        public ProdService(IProdRepository prodRepository)
        {
            _prodRepository = prodRepository;
        }

        public  async Task<ApiResponse<object>> AddProduct(ProductDto addProductDto)
        {
            return await  _prodRepository.AddProduct(addProductDto);
        }


    }
}
