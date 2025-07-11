using System.ComponentModel.DataAnnotations;
using PetGrubBakcend.Entities;

namespace PetGrubBakcend.DTOs
{
    public class ProductDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public IFormFile Image { get; set; } //accepting uploaded image file
        [Required]
        public string Brand { get; set; }

        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int CategoryId {  get; set; }
    }
}
