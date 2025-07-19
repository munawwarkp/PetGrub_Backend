using System.ComponentModel.DataAnnotations;

namespace PetGrubBakcend.DTOs
{
    public class AddToCartDto
    {
        [Required(ErrorMessage ="Product id is required")]
        public int ProductId {  get; set; }

        public int Quantity { get; set; }  //default is one
    }
}
