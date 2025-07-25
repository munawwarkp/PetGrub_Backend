using System.Text.Json.Serialization;

namespace PetGrubBakcend.Entities
{
    public class CartItem
    {
        public int Id { get; set; }

        //foreign keys
        public int UserId {  get; set; }
        public int ProductId {  get; set; }

        public int Quantity { get; set; } 

        //navigation property
        public User User { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }

    }
}
