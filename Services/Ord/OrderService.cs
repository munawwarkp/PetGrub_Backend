using AutoMapper;
using PetGrubBakcend.ApiResponse;
using PetGrubBakcend.DTOs;
using PetGrubBakcend.Entities;
using PetGrubBakcend.Repositories.Address;
using PetGrubBakcend.Repositories.Ord;
using PetGrubBakcend.Services.Address;

namespace PetGrubBakcend.Services.Ord
{
    public class OrderService:IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

       public async Task<ApiResponse<object>> CreateOrderProductSingle(int userId, int productId, int addressId)
        {
            try
            {
                

               var existingProduct =  await  _repository.GetExistingProduct(productId);
               var getAddress = await _repository.GetUserAddress(addressId);


              if(existingProduct != null && getAddress != null)
                {

                    var orderItem = new OrderItem
                    {
                        ProductId = existingProduct.Id,
                        Quantity = 1,
                        UnitPrice = existingProduct.Price,
                        TotalPrice = existingProduct.Price,
                    };

                    var order = new Order
                    {
                        ProductId = existingProduct.Id,
                        AddressId = getAddress.Id,
                        OrderDate = DateTime.UtcNow,
                        UserId = userId,
                        TotalAmount = existingProduct.Price,
                        status = "pending",
                        OrderItems = new List<OrderItem> {orderItem}
                    };

                  await _repository.CreateOrderDistinctProduct(order);

                    var data = new OrderReadDto
                    {
                        OrderId = order.Id,
                        Item = existingProduct.Brand + " " + existingProduct.Title,
                        Quantity = orderItem.Quantity,
                        Price = orderItem.TotalPrice
                    };
                    
                    return new ApiResponse<object>
                    {
                        StatusCode = 200,
                        Message = "order placed succesfully",
                        Data = data
                    };
                }
                else
                {
                    return new ApiResponse<object>
                    {
                        StatusCode = 404,
                        Message = "not found"
                    };
                }
                  
            }
            catch(Exception ex)
            {
                return new ApiResponse<object>
                {
                    StatusCode = 500,
                    Message = $"error occured while placing order : {ex.Message}"
                };
            }
        }

    }
}
