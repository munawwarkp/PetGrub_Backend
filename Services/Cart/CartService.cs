using PetGrubBakcend.DTOs;
using PetGrubBakcend.ApiResponse;
using PetGrubBakcend.Repositories.Cart;
using AutoMapper;
using PetGrubBakcend.Entities;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Security.Cryptography.Xml;
using PetGrubBakcend.Enums;
using Microsoft.AspNetCore.Http.HttpResults;

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
                    var product = await _repository.GetProductWithId(productId);
                    updatedDto.Title = product.Title;
                    updatedDto.Brand = product.Brand;
                    updatedDto.Description = product.Description;
                    updatedDto.ImageUrl = product.ImageUrl;
                    updatedDto.Price = product.Price;
                    updatedDto.TotalPrice = product.Price * updatedDto.Quantity;

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

        public async Task<ApiResponse<CartReadDto>> RemoveFromCart(int userId, int productId)
        {
            try
            {
                var isExistCart = await _repository.IsExistCartItem(productId,userId);

                if (!isExistCart)
                {
                    return new ApiResponse<CartReadDto>
                    {
                        StatusCode = 404,
                        Message = "product not found in your cart"
                    };
                }

                //get cart item if exist
                var cartItem = await _repository.GetCartItem(userId, productId);

                await _repository.RemoveProductFromCart(cartItem);

                //for returning data
                var data = _mapper.Map<CartReadDto>(cartItem);

                return new ApiResponse<CartReadDto>
                {
                    StatusCode = 200,
                    Message = "Remove cart item succesfully",
                    Data = data
                };

            }
            catch(Exception ex)
            {
                return new ApiResponse<CartReadDto>
                {
                    StatusCode = 500,
                    Message = $"Error occured while deleting cart item : {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<CartReadDto>> IncreaseOrDecreaseQuantity(UpdateCartActionDto updateCartActionDto,int userId)
        {
            try
            {
                int productId = updateCartActionDto.ProductId;

                Console.WriteLine($"product id : {userId}");

                bool isExist = await _repository.IsExistCartItem(productId,userId);
                if (!isExist)
                {
                    return new ApiResponse<CartReadDto>
                    {
                        StatusCode = 404,
                        Message = "product not found"
                    };
                }

               var cartItem = await _repository.GetCartItem(userId, productId);

                switch (updateCartActionDto.Action)
                {
                    case CartQuantityAction.Increment:
                        cartItem.Quantity += 1;
                        break;
                    case CartQuantityAction.Decrement:
                        cartItem.Quantity -= 1;
                        break;

                }
                await _repository.UpdateCartItem(cartItem);

               var data = _mapper.Map<CartReadDto>(cartItem);

                return new ApiResponse<CartReadDto>
                {
                    StatusCode = 200,
                    Message = $"{cartItem.Quantity} changed",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<CartReadDto>
                {
                    StatusCode = 500,
                    Message = $"error occured while changig quantity : {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<object>> ClearCart(int userId)
        {
            try
            {
                var cartItems = await _repository.GetAllCartItem(userId);
                if (cartItems.Count == 0 || cartItems == null)
                {
                    return new ApiResponse<object>
                    {
                        StatusCode = 200,
                        Message = "Your cart is already empty"
                    };
                }


                await _repository.ClearCart(userId);
                return new ApiResponse<object>
                {
                    StatusCode = 200,
                    Message = "All items have been removed from your cart",

                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<object>
                {
                    StatusCode = 500,
                    Message = $"error occured while clearing cart : {ex.Message}"
                };
            }

           
        }

    }
}

