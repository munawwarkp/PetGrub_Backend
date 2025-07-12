using AutoMapper;
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


    }
}
