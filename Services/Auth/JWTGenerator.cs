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
            //header
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            //private claims
            //are custom claims created to share info btw parties
            //consider public claims if your jwt is meant to read by other systems
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.FirstName),
                new Claim(ClaimTypes.Role,user.RoleId==1 ? "admin":"user".ToString())
            };

            var token = new JwtSecurityToken(
                    claims : claims,
                    expires : DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:ExpireMinutes"])),
                    signingCredentials : creds,
                    issuer: "PetGrubBackend");

            return new JwtSecurityTokenHandler().WriteToken(token); //[Base64Header].[Base64Payload].[Base64Signature] //Serializes the token into the compact JWT format:
        }
    }
}
