using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PetGrubBakcend.Entities;

namespace PetGrubBakcend.Services.Auth
{
    public class JWTGenerator:IJWTGenerator
    {
        private readonly IConfiguration _configuration;
        public JWTGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.FirstName),
                new Claim(ClaimTypes.Role,user.Role.ToString())
            };

            var token = new JwtSecurityToken(
                    claims : claims,
                    expires : DateTime.UtcNow.AddMinutes(1),
                    signingCredentials : creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
