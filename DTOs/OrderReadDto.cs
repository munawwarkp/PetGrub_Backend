namespace PetGrubBakcend.DTOs
{
    public class OrderReadDto
    {
        public int OrderId {  get; set; }
        public string Item {  get; set; }
        public int Quantity {  get; set; }
        public decimal UnitPrice {  get; set; }
        public decimal TotalPrice {  get; set; }
        public string Image {  get; set; }
    }
}
