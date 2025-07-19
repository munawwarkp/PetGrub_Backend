namespace PetGrubBakcend.DTOs
{
    public class CartReadResponseDto
    {
        public IEnumerable<CartReadDto> cartReadDtos { get; set; }
        public decimal TotalCartPrice {  get; set; }
    }
}
