using Microsoft.Win32;
using PetGrubBakcend.DTOs;
using PetGrubBakcend.ApiResponse;
using PetGrubBakcend.Entities;

namespace PetGrubBakcend.Repositories.AuthRepository
{
    public interface IAuthRepository
    {
        Task<ApiResponse<Object>> Register(UserRegistrationDto userRegistrationDto);
        Task<ApiResponse<object>> Login(UserLoginDto userLoginDto);
        Task<ApiResponse<object>> RefreshToken(RefreshTokenDto refreshTokenDto);
        Task<ApiResponse<object>> CreateAdmin(UserRegistrationDto adminDto);
        
    }
}
