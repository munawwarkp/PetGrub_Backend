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
        public async Task<ApiResponse<object>> AddProd(ProductDto addProductDto)
        {
            return await _prodService.AddProduct(addProductDto);     
        }

      
       
    }
}
