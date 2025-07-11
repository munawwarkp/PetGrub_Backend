using System.ComponentModel.DataAnnotations;

namespace PetGrubBakcend.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string ImageUrl {  get; set; }
        [Required]
        public string Brand {  get; set; }
       
        [Required]
        public decimal Price {  get; set; }
        [Required]
        public string Description { get; set; }

        public int CategoryID {  get; set; }
        public Category Category { get; set; }

    }
}
