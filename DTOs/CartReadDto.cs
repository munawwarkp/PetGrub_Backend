namespace PetGrubBakcend.DTOs
{
    public class CartReadDto
    {
        public int Id { get; set; }
        public int ProductId {  get; set; } //for refernce
        public string Title {  get; set; }
        public string Brand {  get; set; }
        public string Description {  get; set; }
        public string ImageUrl {  get; set; }


        public decimal Price {  get; set; }
        public int Quantity { get; set; }

        public decimal TotalPrice {  get; set; }

    }
}
