using System.Numerics;
using PetGrubBakcend.Entities;

namespace PetGrubBakcend.DTOs
{
    public class OrderCreateDto
    {
        public int ProductId {  get; set; }

        public int AddressId {  get; set; }

        public decimal Total { get; set; }
        public string TransactionId { get; set; }

        //navigation property

        public AddressUser? AddressUser { get; set; }

        public Product Product { get; set; }

    

        
    }
}
