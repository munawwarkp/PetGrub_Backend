namespace PetGrubBakcend.DTOs
{
    public class AddressReadDto
    {
        public int Id { get; set; }
        public string HouseNumber { get; set; }
        public string Area { get; set; }
        public string LandMark { get; set; }
        public string City { get; set; }

        public string PinCode { get; set; }
        public string State { get; set; }
    }
}
