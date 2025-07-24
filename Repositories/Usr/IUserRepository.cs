using PetGrubBakcend.DTOs;
using PetGrubBakcend.Entities;

namespace PetGrubBakcend.Repositories.Usr
{
    public interface IUserRepository
    {
        Task<List<User>> ListUsers();
        Task<User?> GetUser(int id);
        Task<User?> BolckOUnblockUser(int id);
        Task ImplementBlcokOUnblockUser();
    }
}
