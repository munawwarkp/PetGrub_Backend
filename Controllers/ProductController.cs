using System.Collections.Concurrent;
using System.Security.Cryptography.Xml;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Policy = "AdminOnly")]
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

        [Authorize(Policy ="AdminOnly")]
        [HttpPatch("{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId,[FromForm]ProductUpdateDto productUpdateDto)
        {
           var res = await _prodService.UpdateProduct(productId, productUpdateDto);
            return StatusCode(res.StatusCode, res);
        }



        [Authorize]
        [HttpGet("GetProducts")]
        public async Task<ApiResponse<List<ProductReadingDto>>> GetAll()
        {
            try
            {
                var res = await _prodService.GetProducts();
                if (res == null)
                {
                    return new ApiResponse<List<ProductReadingDto>>
                    {
                        StatusCode = 404,
                        Message = "Products not found",
                    };
                }
                return new ApiResponse<List<ProductReadingDto>>
                {
                    StatusCode = 200,
                    Message = "products fetched succesfully",
                    Data = res
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<ProductReadingDto>>
                {
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }


        [HttpGet("Product-By-Id")]
        [Authorize]
        public async Task<ApiResponse<ProductReadingDto>> GetById(int id)
        {
            try
            {
                var res = await _prodService.GetProductById(id);
                return new ApiResponse<ProductReadingDto>
                {
                    StatusCode = 200,
                    Message = res.ErrorMessage,
                    Data = res.Data
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ProductReadingDto>
                {
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
           
        }

        [HttpGet("Product-by-Category")]
        [Authorize]
        public async Task<ApiResponse<List<ProductReadingDto>>> GetByCategory(string name)
        {
            try
            {
                var res = await _prodService.GetProductsByCategoryName(name);
                if (res == null || res.Count==0)
                {
                    return new ApiResponse<List<ProductReadingDto>>
                    {
                        StatusCode = 404,
                        Message = "category not found"
                    };
                }

                return new ApiResponse<List<ProductReadingDto>>
                {
                    StatusCode = 200,
                    Message = "succesfully fetched products by category",
                    Data = res
                };
            }
            catch(Exception ex)
            {
                return new ApiResponse<List<ProductReadingDto>>
                {
                    StatusCode = 500,
                    Error = ex.Message
                };
            }
          
        }

        [HttpDelete("Delete")]
        [Authorize(Roles ="admin")]
        public async Task<ApiResponse<ProductReadingDto>> DeleteProduct(int id)
        {
            try
            {
              var res = await _prodService.DeleteProductAsync(id);

                return new ApiResponse<ProductReadingDto>
                {
                    StatusCode = 200,
                    Data = res,
                    Message = $"Succesfully deleted product with id {id}"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ProductReadingDto> { StatusCode=500,Message = $"error while deleting product {id} : {ex.Message}" };
            }

        }

        [HttpGet("Search")]
        [Authorize]
        public async Task<ApiResponse<List<ProductReadingDto>>> Search(string str)
        {
            try
            {
                var res = await _prodService.SearchProducts(str);
                return new ApiResponse<List<ProductReadingDto>>
                {
                    StatusCode = 200,
                    Message = "Product searching success",
                    Data = res
                };
            }
            catch(Exception ex)
            {
                return new ApiResponse<List<ProductReadingDto>>
                {
                    StatusCode = 500,
                    Message = ex.Message
                };
            } 
        }
    }
}
