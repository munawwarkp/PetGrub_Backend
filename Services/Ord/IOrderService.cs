using PetGrubBakcend.ApiResponse;
using PetGrubBakcend.DTOs;
using PetGrubBakcend.Entities;
namespace PetGrubBakcend.Services.Ord
{
    public interface IOrderService
    {
        Task<ApiResponse<object>> CreateOrderProductSingle(int userId, int productId, int addressId);
        Task<ApiResponse<object>> BulkOrderFromCart(int userId,int addressId);

        Task<ApiResponse<List<OrderReadDto>>> GetOrders(int userId);
        Task<ApiResponse<List<OrderReadDto>>> GetOrdersWhole();


        //Task<ApiResponse<object>> GetOrderDetailsById(int id);
        //Task<ApiResponse<object>> CartDetailsById(int id);

        //razor pay
        //Task<string> RazorPayOrderCreate(long price);
        //Task<bool> RazorPayment();

    }
}
