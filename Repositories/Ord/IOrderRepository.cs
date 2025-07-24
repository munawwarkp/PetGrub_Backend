using PetGrubBakcend.ApiResponse;
using PetGrubBakcend.Entities;

namespace PetGrubBakcend.Repositories.Ord
{
    public interface IOrderRepository
    {
        Task<Product?> GetExistingProduct(int productId);
        Task<AddressUser?> GetUserAddress(int userId);
        Task CreateOrderDistinctProduct(Order order);


        //Task GetOrderDetailsById(int id);
        //Task CartDetailsById(int id);

        //razor pay
        //Task<string> RazorPayOrderCreate(long price);
        //Task<bool> RazorPayment();
    }
}
