using Microsoft.EntityFrameworkCore;
using PetGrubBakcend.ApiResponse;
using PetGrubBakcend.Data;
using PetGrubBakcend.Entities;

namespace PetGrubBakcend.Repositories.Ord
{
    public class OrderRepository:IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetExistingProduct(int productId)
        {
            return await _context.Products.FindAsync(productId);

        }

        public async Task<AddressUser?> GetUserAddress(int addressId)
        {
            return await _context.Addresses.FindAsync(addressId);
        }


        public async Task CreateOrderDistinctProduct(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync() ;
        }


        //public  Task<ApiResponse<object>> GetOrderDetailsById(int id)
        // {
        //     _context.Order
        // }
        //  public Task<ApiResponse<object>> CartDetailsById(int id)
        // {

        // }
    }
}
