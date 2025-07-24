using System.ComponentModel.DataAnnotations;

namespace PetGrubBakcend.Entities
{
    public class Order
    {
        public int Id { get; set; }

        //foreign keys
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public int? ProductId { get; set; }


        //order details
        [Required]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        [Required]
        public decimal TotalAmount {  get; set; }
        [Required]
        public string? status {  get; set; }
        public string? PaymentMethod {  get; set; }


        //navigation
        public User? User { get; set; }

        public AddressUser? ShippingAddress { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; }
        public Product? Product { get; set; }
       
    }
}
