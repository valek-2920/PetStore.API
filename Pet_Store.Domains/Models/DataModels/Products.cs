using Pet_Store.Domains.Models.DataModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pet_Store.Domains.Models.DataModels
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        public Files Files { get; set; }

    }
}
