using System.ComponentModel.DataAnnotations;

namespace PetGrubBakcend.DTOs
{
    public class AddCategoryDto
    {
        [Required]
        public string Name { get; set; }
    }
}
