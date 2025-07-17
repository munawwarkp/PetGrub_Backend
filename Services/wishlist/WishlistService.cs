using AutoMapper;
using Azure;
using PetGrubBakcend.DTOs;
using PetGrubBakcend.Entities;
using PetGrubBakcend.Repositories.wishlist;
using PetGrubBakcend.Result;
using PetGrubBakcend.ApiResponse;

namespace PetGrubBakcend.Services.wishlist
{
    public class WishlistService:IWishlistService
    {
        private readonly IWishlistRepository repository;
        private readonly IMapper _mapper;
        private readonly ILogger<WishlistService> _logger;
        public WishlistService(IWishlistRepository wishlistRepository,IMapper mapper,ILogger<WishlistService> logger)
        {
            repository = wishlistRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<ProductReadingDto>> AddWishlistIfNotExist(int userId,int productId)
        {
            try
            { 
                bool isExist = await repository.ExistAsync(userId,productId);
                if (!isExist)
                {
                    var wishlistDto = new WishListDto
                    {
                       ProductId = productId,
                       UserId = userId
                    };
                    var wishlist = _mapper.Map<Wishlist>(wishlistDto);

                  await repository.AddAsync(wishlist);

                    return new ApiResponse<ProductReadingDto>
                    {
                        StatusCode = 200,
                        Message = "product added to wishlist succesfully",
                    };
                }
                return new ApiResponse<ProductReadingDto>
                {
                    StatusCode = 409,
                    Message = "product already exist"
                };
               
            }
            catch(Exception ex)
            {
                return new ApiResponse<ProductReadingDto> { StatusCode = 500, Message = ex.Message };
            }
           
        }

        public async Task<ApiResponse< List<ProductReadingDto>>> GetWishlistedProduct(int userId)
        {
            try
            {
                var res = await repository.GetWishlistedProduct(userId);

                if(res == null)
                {
                    return new ApiResponse<List<ProductReadingDto>>
                    {
                        StatusCode = 404,
                        Message = "null wishlist"
                    };
                }

                var data = _mapper.Map<List<ProductReadingDto>>(res);
                return new ApiResponse<List<ProductReadingDto>>
                {
                    StatusCode = 200,
                    Message = "successfully fetched wishlist",
                    Data = data
                };
            }
            catch(Exception ex)
            {
              return new ApiResponse<List<ProductReadingDto>>
                {
                    StatusCode = 500,
                    Message = $"{ex.Message}"
                };
            }
          
        }

        public async Task<ApiResponse<object>> RemoveWishlistedServ(int userId,int productId)
        {
            try
            {
                await repository.RemoveWishlisted(userId,productId);
            

                var wishlistProduct = await repository.GetWishlistedProduct(userId);
                var removedProduct = wishlistProduct.FirstOrDefault();

                var returnRemovedProduct = _mapper.Map<ProductReadingDto>(removedProduct);

                return new ApiResponse<object>
                {
                    StatusCode = 200,
                    Message = "wishlist removed succesfully",
                };
                
            }
            catch (Exception ex)
            {
                return new ApiResponse<object>
                {
                    StatusCode = 500,
                    Message = "error while deleting wishlist",
                    Error = ex.Message
                };
            }


        }


    }
}
