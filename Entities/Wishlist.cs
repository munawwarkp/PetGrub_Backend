using System.ComponentModel.DataAnnotations;

namespace PetGrubBakcend.Entities
{
    public class Wishlist
    {
        [Required]
        public int Id { get; set; }

        //foreign key
        [Required]
        public int ProductId {  get; set; }
        [Required]
        public int UserId {  get; set; }

        public User User { get; set; } //one per user
    }
}
