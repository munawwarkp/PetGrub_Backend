using System.ComponentModel.DataAnnotations;

namespace PetGrubBakcend.DTOs
{
    public class AddressCreateDto
    {
        public string HouseNumber { get; set; }
        public string Area { get; set; }
        public string LandMark { get; set; }
        public string City { get; set; }

        public string PinCode { get; set; }
        public string State { get; set; }
    }
}
