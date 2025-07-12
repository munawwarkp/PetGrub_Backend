using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetGrubBakcend.ApiResponse;
using PetGrubBakcend.CloudinaryS; 
using PetGrubBakcend.DTOs;
using PetGrubBakcend.Services.Prod;

namespace PetGrubBakcend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProdService _prodService;
        public ProductController(IProdService prodService)
        {
            _prodService = prodService;
        }

        [HttpPost("Add-Product")]
        public async Task<ApiResponse<ProductDto>> AddProd(ProductDto addProductDto)
        {
            try
            {
               var result = await _prodService.AddProductAsync(addProductDto);
                //if(result == null)
                //{
                //    return new ApiResponse<ProductDto> { Message = "result is null" };
                //}
                return new ApiResponse<ProductDto>
                {
                    StatusCode = 200,
                    Message = "product added successfully",
                    Data = result.Data  
                    
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ProductDto>
                {
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }

      
       
    }
}
