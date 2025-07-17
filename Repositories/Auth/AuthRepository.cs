using PetGrubBakcend.Data;
using PetGrubBakcend.DTOs;
using PetGrubBakcend.ApiResponse;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using PetGrubBakcend.Entities;
using BCrypt.Net;
using PetGrubBakcend.Services.Auth;
using System.Security.Cryptography;
using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;

namespace PetGrubBakcend.Repositories.AuthRepository
{
    public class AuthRepository:IAuthRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IJWTGenerator _jwtToken;
        private readonly ILogger<AuthRepository> _logger;
        public AuthRepository(AppDbContext context, IMapper mapper,IJWTGenerator jwtToken,ILogger<AuthRepository> logger )
        {
            _context = context;
            _mapper = mapper;
            _jwtToken = jwtToken;
            _logger = logger;
        }

        public async Task<ApiResponse<object>> Register(UserRegistrationDto userRegistrationDto)
        {
            try
            {
                //check user already exist
                var isExist = await _context.Users.FirstOrDefaultAsync(u => u.Email == userRegistrationDto.Email);
                if (isExist != null)
                {
                    return new ApiResponse<object> { StatusCode=409,Message="User already exist"};
                }
                
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userRegistrationDto.Password);
                var user = _mapper.Map<User>(userRegistrationDto);
                user.Password = hashedPassword;
                user.RoleId = 2;

                _context.Add(user);
               await _context.SaveChangesAsync();

                return new ApiResponse<object> { StatusCode = 400, Message = "User registered succesfully" };
                
            }catch(DbUpdateException dbEx)
            {
                return new ApiResponse<object>
                {
                    StatusCode = 500,
                    Message = "Database error occured",
                    Error = dbEx.InnerException?.Message ?? dbEx.Message
                };
            }
        }

        public async Task<ApiResponse<object>> Login(UserLoginDto userLoginDto)
        {
            try
            {
                var loginUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == userLoginDto.Email);
                if(loginUser == null)
                {
                    return new ApiResponse<object> { StatusCode = 404, Message = "User not found" };
                }

                if(loginUser.IsBlocked == true)
                {
                    return new ApiResponse<object> { StatusCode = 403, Message = "User is blocked by admin" };
                }

                var verifyPassword = BCrypt.Net.BCrypt.Verify(userLoginDto.Password, loginUser.Password);
                if (!verifyPassword)
                {
                    return new ApiResponse<object> { StatusCode = 401, Message = "Invalid Credentials" };
                }

                var token = _jwtToken.GenerateToken(loginUser);

                var refreshToken = GenerateRefreshToken();
                loginUser.RefreshToken = refreshToken;
                loginUser.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
                
                await _context.SaveChangesAsync();

                return new ApiResponse<object>
                {
                    StatusCode = 200,
                    Message = "Token generated succesfully",
                    Data = new UserResponseDto
                    {
                       UserName = loginUser.FirstName + " " + loginUser.LastName,
                       UserEmail = loginUser.Email,
                       Role = loginUser.RoleId.ToString(),
                       Token = token,
                       Id = loginUser.Id,
                       RefreshToken = refreshToken
                       
                    }
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<object> { StatusCode=500,Message = ex.Message};
            }
        }

        public async Task<ApiResponse<object>> RefreshToken(RefreshTokenDto refreshTokenDto)
        {
            try
            {
              var user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshTokenDto.RefreshToken);
                //if database couldnt find a paritcular refresh token or the refresh token is already expired then return 
              if(user == null || user.RefreshTokenExpiry < DateTime.UtcNow)
                {
                    return new ApiResponse<object>
                    {
                        StatusCode = 401,
                        Message = "Invalid or expired refresh token"
                    };
                }

              //generate new token
              var newAccessToken = _jwtToken.GenerateToken(user);
              var newRefreshToken = GenerateRefreshToken();

                _logger.LogInformation($"refresh token = {newRefreshToken}");

                user.RefreshToken = newRefreshToken;
                user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

                await _context.SaveChangesAsync();

                return new ApiResponse<object>
                {
                    StatusCode = 200,
                    Message = "Token refreshed successfully",
                    Data = new
                    {
                        AccessToken = newAccessToken,
                        RefreshToken = newRefreshToken,
                    }
                };

            }
            catch(Exception ex)
            {
                return new ApiResponse<object>
                {
                    StatusCode = 500,
                    Message = $"server-error:{ex.Message}"
                };
            }
        }


        public async Task<ApiResponse<object>> CreateAdmin(UserRegistrationDto adminDto)
        {
            try
            {
                var admin = await _context.Users.FirstOrDefaultAsync(a => a.Email == adminDto.Email);

                if (admin != null)
                {
                    return new ApiResponse<object> { StatusCode = 409, Message = "Admin already exist" };
                }

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(adminDto.Password);
                var MappedAdmin = _mapper.Map<User>(adminDto);
                MappedAdmin.Password = hashedPassword;
                MappedAdmin.RoleId = 1; //admin role

                _context.Add(MappedAdmin);
                await _context.SaveChangesAsync();
                return new ApiResponse<object>
                {
                    StatusCode = 200,
                    Message = "Admin Registered Successfully",
                    Data = MappedAdmin
                };
            }
            catch(Exception ex)
            {
                return new ApiResponse<object>
                {
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
            
        }

        //refrsh token definition

        private string GenerateRefreshToken()
        {
            var randomBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }
       
    }
}
