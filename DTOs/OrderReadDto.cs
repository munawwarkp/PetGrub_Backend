namespace PetGrubBakcend.DTOs
{
    public class OrderReadDto
    {
        public int OrderId {  get; set; }
        public string Item {  get; set; }
        public int Quantity {  get; set; }
        public decimal Price {  get; set; }
    }
}
