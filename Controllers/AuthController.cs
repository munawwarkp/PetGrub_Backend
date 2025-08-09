using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetGrubBakcend.DTOs;
using PetGrubBakcend.Services.AuthServices;
using PetGrubBakcend.ApiResponse;

namespace PetGrubBakcend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService service)
        {
            _authService = service;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> Register([FromBody]UserRegistrationDto userRegistrationDto)
        {
           var response = await _authService.Register(userRegistrationDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]UserLoginDto userLoginDto)
        {
            var response =  await _authService.Login(userLoginDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("Refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var response = await _authService.RefreshToken(refreshTokenDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("Admin-SignUp")]
        public async Task<IActionResult> AdminSignUp(UserRegistrationDto userRegistrationDto)
        {
            var response = await _authService.CreateAdmin(userRegistrationDto);
            return StatusCode(response.StatusCode,response);
        }
    }
}
