namespace PetGrubBakcend.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }

        //foreign keys
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        //item details
        public int Quantity {  get; set; }
        public decimal UnitPrice {  get; set; }
        public decimal TotalPrice {  get; set; }

        //navigation
       
        public Order Order { get; set; }

        public Product Product { get; set; }


    }
}
