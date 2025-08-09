using System.Text.Json.Serialization;
using PetGrubBakcend.Enums;

namespace PetGrubBakcend.DTOs
{
    public class UpdateCartActionDto
    {
         public int ProductId {  get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CartQuantityAction Action { get; set; }
    }
}
    