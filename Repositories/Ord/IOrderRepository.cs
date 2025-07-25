using PetGrubBakcend.ApiResponse;
using PetGrubBakcend.DTOs;
using PetGrubBakcend.Entities;

namespace PetGrubBakcend.Repositories.Ord
{
    public interface IOrderRepository
    {
        Task<Product?> GetExistingProduct(int productId);
        Task<AddressUser?> GetUserAddress(int userId);
        Task CreateOrderDistinctProduct(Order order);

        Task CreateBulkOrder(Order order);
        Task<List< OrderReadDto>> GetOrderDetails(int userId);
        Task<List<OrderReadDto>> GetOrderDetailsWhole();



        //razor pay
        //Task<string> RazorPayOrderCreate(long price);
        //Task<bool> RazorPayment();
    }
}
