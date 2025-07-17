using Microsoft.Identity.Client;

namespace PetGrubBakcend.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName {  get; set; }
        public string? LastName { get; set; }
        public string MobileNumber {  get; set; }
        public string Email { get; set; }   
        public string Password { get; set; }


        public bool IsBlocked {  get; set; }=false;
        public string? RefreshToken {  get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }

        
        public int RoleId { get; set; } //forign key to role
        public Role Role { get; set; } //navigation : one user-one role
        public ICollection<Wishlist> Wishlist { get; set; }
    }
 
}
