using PetGrubBakcend.DTOs;
using PetGrubBakcend.ApiResponse;
using PetGrubBakcend.Repositories.AuthRepository;

namespace PetGrubBakcend.Services.AuthServices
{
    public class AuthService:IAuthService
    {
        private readonly  IAuthRepository _authRepository;
        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        public async Task<ApiResponse<object>> Register(UserRegistrationDto userRegistrationDto)
        {
            return await _authRepository.Register(userRegistrationDto);    
        }

        public async Task<ApiResponse<object>> Login(UserLoginDto userLoginDto)
        {
            return await _authRepository.Login(userLoginDto);
        }
        public async Task<ApiResponse<object>> RefreshToken(RefreshTokenDto refreshTokenDto)
        {
            return await _authRepository.RefreshToken(refreshTokenDto);
        }
        public async Task<ApiResponse<object>> CreateAdmin(UserRegistrationDto adminDto)
        {
            return await _authRepository.CreateAdmin(adminDto);
        }


    }
}
