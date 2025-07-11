using PetGrubBakcend.DTOs;
using PetGrubBakcend.ApiResponse;
using PetGrubBakcend.Repositories.AuthRepository;

namespace PetGrubBakcend.Services.AuthServices
{
    public interface IAuthService
    {
        Task<ApiResponse<object>> Register(UserRegistrationDto userRegistrationDto);
        Task<ApiResponse<object>> Login(UserLoginDto userLoginDto);
        Task<ApiResponse<object>> RefreshToken(RefreshTokenDto refreshTokenDto);
        Task<ApiResponse<object>> CreateAdmin(UserRegistrationDto adminDto);


    }
}
