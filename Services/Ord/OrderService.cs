using System.Collections;
using AutoMapper;
using PetGrubBakcend.ApiResponse;
using PetGrubBakcend.Data;
using PetGrubBakcend.DTOs;
using PetGrubBakcend.Entities;
using PetGrubBakcend.Repositories.Address;
using PetGrubBakcend.Repositories.Cart;
using PetGrubBakcend.Repositories.Ord;
using PetGrubBakcend.Services.Address;
using PetGrubBakcend.Services.Cart;

namespace PetGrubBakcend.Services.Ord
{
    public class OrderService:IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICartRepesitory _cartRepesitory;
        private readonly AppDbContext _context;
        public OrderService(IOrderRepository repository,IMapper mapper,ICartRepesitory cartRepesitory,AppDbContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _cartRepesitory = cartRepesitory;
            _context = context;
        }

       public async Task<ApiResponse<object>> CreateOrderProductSingle(int userId,int productId, int addressId)
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
                        status = "pending",    // status set roughly,not logically,later fix
                        OrderItems = new List<OrderItem> {orderItem}
                    };

                  await _repository.CreateOrderDistinctProduct(order);

                    var data = new OrderReadDto
                    {
                        OrderId = order.Id,
                        Item = existingProduct.Brand + " " + existingProduct.Title,
                        Quantity = orderItem.Quantity,
                        UnitPrice = orderItem.UnitPrice
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
        public async Task<ApiResponse<object>> BulkOrderFromCart(int userId, int addressId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var cartedItems = await _cartRepesitory.GetAllCartItem(userId);

                if (cartedItems == null || !cartedItems.Any())
                {
                    return new ApiResponse<object>
                    {
                        StatusCode = 404,
                        Message = "Cart is null"
                    };
                }

                var order = new Order
                {
                    UserId = userId,
                    AddressId = addressId,
                    status = "order placed", //status set not logically,fix later
                    OrderDate = DateTime.UtcNow,
                    OrderItems = cartedItems.Select(ci => new OrderItem
                    {
                        ProductId = ci.Product.Id,
                        Quantity = ci.Quantity,
                        UnitPrice = ci.Product.Price,
                        TotalPrice = ci.Product.Price * ci.Quantity
                    }).ToList()

                };

                await _repository.CreateBulkOrder(order);
                await _cartRepesitory.ClearCart(userId);

                await transaction.CommitAsync();


                return new ApiResponse<object>
                {
                    StatusCode = 200,
                    Message = "succesfully ordered",
                    Data = cartedItems
                };
                

            }
            catch(Exception ex)
            {
                await transaction.RollbackAsync();
                return new ApiResponse<object>
                {
                    StatusCode = 500,
                    Message = $"error occured while placing bulk ordert from cart : {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<List< OrderReadDto>>> GetOrders(int userId)
        {
            try
            {
                var orders = await _repository.GetOrderDetails(userId);

                if (orders == null)
                {
                    return new ApiResponse<List<OrderReadDto>>
                    {
                        StatusCode = 404,
                        Message = "No orders yet",
                        Data = null
                    };
                }

                return new ApiResponse<List<OrderReadDto>>
                {
                    StatusCode = 200,
                    Message = "Your ordered list is here",
                    Data = orders
                };

            }
            catch (Exception ex)
            {
                return new ApiResponse<List<OrderReadDto>>
                {
                    StatusCode = 500,
                    Message = $"exception occured while fetching order details : {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<List<OrderReadDto>>> GetOrdersWhole()
        {
            try
            {
                var orders = await _repository.GetOrderDetailsWhole();

                if (orders == null)
                {
                    return new ApiResponse<List<OrderReadDto>>
                    {
                        StatusCode = 404,
                        Message = "No orders yet",
                        Data = null
                    };
                }

                return new ApiResponse<List<OrderReadDto>>
                {
                    StatusCode = 200,
                    Message = "Your ordered list is here",
                    Data = orders
                };

            }
            catch (Exception ex)
            {
                return new ApiResponse<List<OrderReadDto>>
                {
                    StatusCode = 500,
                    Message = $"exception occured while fetching order details : {ex.Message}"
                };
            }
        }


    }
}
