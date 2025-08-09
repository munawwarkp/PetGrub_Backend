namespace PetGrubBakcend.DTOs
{
    public class RazorOrderResponseDto
    {
        public string OrderId { get; set; }
        public long Amount { get; set; }
        public string Currency { get; set; } = "INR";
        public string Key { get; set; }
    }
}
