using System.Text.Json.Serialization;

namespace PetGrubBakcend.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CartQuantityAction
    {
        Increment,
        Decrement
    }
}
