using System.ComponentModel.DataAnnotations;

namespace PetGrubBakcend.DTOs
{
    public class UserRegistrationDto
    {
        [Required(ErrorMessage ="First Name is required.")]
        [MaxLength(30,ErrorMessage ="FirstName should not exceed 30 character.")]
        public string FirstName { get; set; }

        public string? LastName { get; set; }

        [Required(ErrorMessage ="Mobile number is required.")]
        [MaxLength(15,ErrorMessage ="mobile number should not exceed 15 character.")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage ="Email is required!")]
        [EmailAddress(ErrorMessage ="Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password is required")]
        [MinLength(8,ErrorMessage ="Password must be atleast 8 characters long.")]
        public string Password { get; set; }

        [Compare("Password",ErrorMessage = "Password and Confirm Password do not match.")]
        public string ConfirmPassword {  get; set; }
    }
}
