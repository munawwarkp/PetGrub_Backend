using System.ComponentModel.DataAnnotations;

namespace PetGrubBakcend.Entities
{
    public class AddressUser
    {
        public int Id { get; set; }
        [Required]
        public string HouseNumber { get; set; }
        [Required]
        public string Area { get; set; }
        public string LandMark { get; set; }
        [Required]
        public string City { get; set; }

        public string PinCode { get; set; }
       
        [Required]
        public string State { get; set; }

        //link to user
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Order> Orders { get; set; }

    }
}
