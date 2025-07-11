using System.ComponentModel.DataAnnotations;

namespace PetGrubBakcend.DTOs
{
    public class UserLoginDto
    {
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage ="Invalid email adress")]
        public string Email {  get; set; }

        [Required(ErrorMessage ="Password is required")]
        [MinLength(8,ErrorMessage = "Password must be atleast 8 characters long")]
        public string Password { get; set; }
    }
}
