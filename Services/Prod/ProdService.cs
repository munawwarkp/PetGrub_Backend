using AutoMapper;
using PetGrubBakcend.ApiResponse;
using PetGrubBakcend.CloudinaryS;
using PetGrubBakcend.DTOs;
using PetGrubBakcend.Entities;
using PetGrubBakcend.Repositories.Prod;
using PetGrubBakcend.Result;

namespace PetGrubBakcend.Services.Prod
{
    public class ProdService:IProdService
    {
        private readonly IProdRepository _prodRepository;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;
        public ProdService(IProdRepository prodRepository,IMapper mapper,ICloudinaryService cloudinaryService )
        {
            _prodRepository = prodRepository;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
        }

        public  async Task<Result<ProductDto>> AddProductAsync(ProductDto productDto)
        {
            try
            {
                var product = _mapper.Map<Product>(productDto);

                if (productDto.Image != null)
                {
                    var uploadImage = await _cloudinaryService.UploadImageAsync(productDto.Image);
                    product.ImageUrl = uploadImage;
                }
                var savedProduct = await _prodRepository.AddProductAsync(product);

                //reverse mapping done here bcs genric type of Result class is ProductDto,
                //for passing a dto,we need to reverse map here
                var resDto = _mapper.Map<ProductDto>(savedProduct);

                Console.WriteLine("Saved Product ID: " + savedProduct.Id);
                Console.WriteLine("Returning DTO: " + resDto?.Title);

                return Result<ProductDto>.Success(resDto);

                //return new Result<ProductDto>
                //{
                //    isSuccess = true,
                //    Data = productDto,
                //};
            }
            catch (Exception ex)
            {
                return Result<ProductDto>.Failure(ex.Message);
            }   
   
        }


        public async Task<ApiResponse<ProductReadingDto>> UpdateProduct(int productId, ProductUpdateDto productUpdateDto)
        {
            try
            {
              var product =  await _prodRepository.GetExistingProduct(productId);
                if(product == null)
                {
                    return new ApiResponse<ProductReadingDto>
                    {
                        StatusCode = 404,
                        Message = "Product not found"
                    };
                }

                var cartExist = await _prodRepository.GetExistingCategory(product.CategoryID);
                if(cartExist == null)
                {
                    return new ApiResponse<ProductReadingDto>
                    {
                        StatusCode = 404,
                        Message = "Category not found"
                    };
                }

                // Conditionally update only if value is provided
                if (!string.IsNullOrWhiteSpace(productUpdateDto.Title))
                    product.Title = productUpdateDto.Title;

                if (!string.IsNullOrWhiteSpace(productUpdateDto.Brand))
                    product.Brand = productUpdateDto.Brand;

                if (productUpdateDto.Price.HasValue)
                    product.Price = productUpdateDto.Price.Value;

                if (!string.IsNullOrWhiteSpace(productUpdateDto.Description))
                    product.Description = productUpdateDto.Description;


                //product.Title = productUpdateDto.Title;
                //product.Brand = productUpdateDto.Brand;
                //product.Price = productUpdateDto.Price;
                //product.Description = productUpdateDto.Description;


                if (productUpdateDto.Image != null)
                {
                    var uploadImage = await _cloudinaryService.UploadImageAsync(productUpdateDto.Image);
                    product.ImageUrl = uploadImage;
                }
                await _prodRepository.UpdateProduct(product);

                var data = _mapper.Map<ProductReadingDto>(product);

                return new ApiResponse<ProductReadingDto>
                {
                    StatusCode = 200,
                    Message = "product updated succesfully",
                    Data = data
                };
            }
            catch(Exception ex)
            {
                return new ApiResponse<ProductReadingDto>
                {
                    StatusCode = 500,
                    Message = $"error occured while updating product : {ex.Message}"
                };
            }
        }


        public async Task<List<ProductReadingDto>> GetProducts()
        {
            try
            {
                var products = await _prodRepository.GetProducts();
                //if(products == null || products.Count == 0)
                //{
                    
                //}
                var mappedData = _mapper.Map<List<ProductReadingDto>>(products);
                return mappedData;
            }
            catch (Exception ex)
            {
                throw;
            }
           
        }


        public async Task<Result<ProductReadingDto>> GetProductById(int id)
        {
            try
            {
                var produdct = await _prodRepository.GetProductById(id);
                if (produdct == null)
                {
                    return new Result<ProductReadingDto>
                    {
                        isSuccess = true,
                        ErrorMessage = "nulllll!,product not found with corresponding id"
                    };
                }

                var mappedData = _mapper.Map<ProductReadingDto>(produdct);
               
                return Result<ProductReadingDto>.Success(mappedData);

                //return new Result<ProductDto>
                //{
                //    isSuccess = true,
                //    Data = mappedData
                //};
            }
            catch(Exception ex)
            {
                return Result<ProductReadingDto>.Failure($"error : {ex.Message}");
            }
            
        }

        public async Task<List<ProductReadingDto>> GetProductsByCategoryName(string name)
        {
            try
            {
                var productsByCat = await _prodRepository.GetProductsByCategoryName(name);
                var mappedProducts = _mapper.Map<List<ProductReadingDto>>(productsByCat);
                return mappedProducts;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<ProductReadingDto> DeleteProductAsync(int id)
        {
            try
            {
                var itemToDel = await _prodRepository.GetProductById(id);
                if (itemToDel == null)
                {
                    throw new KeyNotFoundException($"product with id {id} not found");
                }
                var dto = _mapper.Map<ProductReadingDto>(itemToDel);
                await _prodRepository.DeleteProductAsync(itemToDel);
                return dto;
            }
            catch(Exception ex)
            {
                throw;
            }
           
        }

        public async Task<List<ProductReadingDto>> SearchProducts(string str)
        {
            var products = await _prodRepository.SearchProducts(str);
            if(products == null)
            {
                throw new  KeyNotFoundException("no product found");
            }
            var mappedProducts =  _mapper.Map<List<ProductReadingDto>>(products);
            return mappedProducts;
            
        }

    }
}
