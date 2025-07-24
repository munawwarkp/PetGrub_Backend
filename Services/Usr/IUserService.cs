using PetGrubBakcend.DTOs;
using PetGrubBakcend.ApiResponse;
using PetGrubBakcend.Entities;

namespace PetGrubBakcend.Services.Usr
{
    public interface IUserService
    {
        Task<ApiResponse<List<UserListDto>>> GetUsers();
        Task<ApiResponse<UserListDto>> GetUserById(int id);
        Task<ApiResponse<object>> BolckOUnblockUserService(int id);

    }
}
