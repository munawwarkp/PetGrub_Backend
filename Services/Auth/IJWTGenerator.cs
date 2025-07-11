using PetGrubBakcend.Entities;

namespace PetGrubBakcend.Services.Auth
{
    public interface IJWTGenerator
    {
       string GenerateToken(User user);
    }
}
