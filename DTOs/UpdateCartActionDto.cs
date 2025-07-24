using PetGrubBakcend.Enums;

namespace PetGrubBakcend.DTOs
{
    public class UpdateCartActionDto
    {
         public int ProductId {  get; set; }
        public CartQuantityAction Action { get; set; }
    }
}
    