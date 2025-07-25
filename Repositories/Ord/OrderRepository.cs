using Microsoft.EntityFrameworkCore;
using PetGrubBakcend.ApiResponse;
using PetGrubBakcend.Data;
using PetGrubBakcend.DTOs;
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


        public async Task CreateBulkOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync() ;
        }

        public async Task<List< OrderReadDto>> GetOrderDetails(int userId)
        {
           

            var query = @"select
                            sub.OrderId,sub.Brand+' '+sub.Title as Item,sub.Quantity,sub.Price,sub.ImageUrl as Image
                          from
                            (
                                select OrderId,Brand,Title,Quantity,Price,ImageUrl from Orders
                                left join OrderItems
                                on OrderItems.OrderId=Orders.Id
                                left join Products
                                on OrderItems.ProductId = Products.Id
                                where UserId = {0}
                            ) sub";
            return await _context.Set<OrderReadDto>()
                .FromSqlRaw(query,userId)  
                .ToListAsync();
        }


        public async Task<List<OrderReadDto>> GetOrderDetailsWhole()
        {


            var query = @"select
                            sub.OrderId,sub.Brand+' '+sub.Title as Item,sub.Quantity,sub.Price,sub.ImageUrl as Image
                          from
                            (
                                select OrderId,Brand,Title,Quantity,Price,ImageUrl from Orders
                                left join OrderItems
                                on OrderItems.OrderId=Orders.Id
                                left join Products
                                on OrderItems.ProductId = Products.Id
                            ) sub";
            return await _context.Set<OrderReadDto>()
                .FromSqlRaw(query)
                .ToListAsync();
        }
    }
}
