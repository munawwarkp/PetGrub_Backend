using System.ComponentModel.DataAnnotations;

namespace PetGrubBakcend.DTOs
{
    public class ProductReadingDto
    {
        public int Id {  get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string ImageUrl { get; set; } //accepting uploaded image file
        [Required]
        public string Brand { get; set; }

        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
