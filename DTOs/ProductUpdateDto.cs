using System.ComponentModel.DataAnnotations;
using PetGrubBakcend.Entities;

namespace PetGrubBakcend.DTOs
{
    public class ProductUpdateDto
    {
        public string? Title { get; set; }
        public IFormFile ?Image { get; set; } //accepting uploaded image file
        public string? Brand { get; set; }

        public decimal? Price { get; set; }
        public string? Description { get; set; }
        
        public int? CategoryId {  get; set; }
    }
}
