using Microsoft.EntityFrameworkCore;
using PetGrubBakcend.Data;
using PetGrubBakcend.DTOs;
using PetGrubBakcend.Entities;

namespace PetGrubBakcend.Repositories.Usr
{
    public class UserRepository:IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> ListUsers()
        {
           return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUser(int id)
        {
           return await _context.Users.FindAsync(id);
        }


        public async Task<User?> BolckOUnblockUser(int id)
        {
           var user =  await _context.Users.FindAsync(id);
            return user;
        }

        public async Task ImplementBlcokOUnblockUser()
        {
            await _context.SaveChangesAsync();
        }
    }
}
