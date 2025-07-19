using PetGrubBakcend.DTOs;
using PetGrubBakcend.ApiResponse;
using PetGrubBakcend.Repositories.Cart;
using AutoMapper;
using PetGrubBakcend.Entities;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace PetGrubBakcend.Services.Cart
{
    public class CartService:ICartService
    {
        private readonly ICartRepesitory _repository;
        private readonly IMapper _mapper;
        public CartService(ICartRepesitory repesitory,IMapper mapper)
        {
            _repository = repesitory;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<CartReadDto>>> GetCartItems(int userId)
        {
            try
            {
                var cart = await _repository.GetAllCartItem(userId);
                if (cart == null || cart.Count == 0)
                {
                    return new ApiResponse<List<CartReadDto>>
                    {
                        StatusCode = 404,
                        Message = "cart is null"
                        
                    };
                }
                var mappedCart = _mapper.Map<List<CartReadDto>>(cart);


                return new ApiResponse<List<CartReadDto>>
                {
                    StatusCode = 200,
                    Message = "succesfully fetched your cart item",
                    Data = mappedCart
                };

            }
            catch (Exception ex)
            {
                return new ApiResponse<List<CartReadDto>>
                {
                    StatusCode = 500,
                    Message = $"Error while fetching cart item from db : , {ex.Message}"
                };
            }
        }
        public async Task<ApiResponse<CartReadDto>> AddToCart(int productId, int userId)
        {
           
            try
            {

                var cartItem = await _repository.GetCartItem(userId, productId);
                bool existProduct = await _repository.GetProduct(productId);

                if (!existProduct)
                {
                    return new ApiResponse<CartReadDto>
                    {
                        StatusCode = 404,
                        Message = "Product not found"
                    };

                }

                if (cartItem == null)
                {
                    var newItem = new CartItem
                    {
                        UserId = userId,
                        ProductId = productId,
                        Quantity = 1
                    };

                    await _repository.AddToCart(newItem);

                    var dto = _mapper.Map<CartReadDto>(newItem);
                    return new ApiResponse<CartReadDto>
                    {
                        StatusCode = 200,
                        Message = "Product added succesfully",
                        Data = dto
                    };
   
                }
                else
                {
                    cartItem.Quantity += 1;
                    await _repository.UpdateCartItem(cartItem);

                    var updatedDto = _mapper.Map<CartReadDto>(cartItem);

                    return new ApiResponse<CartReadDto>
                    {
                        StatusCode = 200,
                        Message = "Item already exists in cart. Quantity increased",
                        Data = updatedDto
                    };
                }

              
            }
            catch(Exception ex)
            {
                return new ApiResponse<CartReadDto>
                {
                    StatusCode = 500,
                    Message = $"error occured while item adding to cart : {ex.Message}"
                };
            }

            

        }



    }
}

//try
//{
//    var isExist = await _repository.IsExistCartItem(productId, userId);

//    if (!isExist)
//    {
//        var itemToAdd = new CartItem
//        {
//            UserId = userId,
//            ProductId = productId,
//            Quantity = 1,

//        };
//       await _repository.AddToCart(itemToAdd);
//        return new ApiResponse<CartReadDto>
//        {
//            StatusCode = 200,
//            Message = "Product added succesfully"
//        };
//    }
//    var cartItem = await _repository.GetCartItem(userId,productId);

//    var updateCartItem = new CartItem
//    {
//        UserId = cartItem.UserId,
//        ProductId = cartItem.ProductId,
//        Quantity = cartItem.Quantity += 1,
//        Product = cartItem.Product
//    };


//    var data = _mapper.Map<CartReadDto>(updateCartItem);

//    return new ApiResponse<CartReadDto>
//    {
//        StatusCode = 409,
//        Message = "Item already exists in cart,Quantity increased",
//        Data = data
//    };


//}
//catch (Exception ex)
//{
//    return new ApiResponse<CartReadDto>
//    {
//        StatusCode = 500,
//        Message = $"Error occured while adding item to cart : {ex.Message}"
//    };
//