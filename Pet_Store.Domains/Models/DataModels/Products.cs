using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

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
        public string Files { get; set; }

    }
}
