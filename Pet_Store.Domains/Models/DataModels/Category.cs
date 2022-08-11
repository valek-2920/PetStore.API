using System.ComponentModel.DataAnnotations;

namespace Pet_Store.Domains.Models.DataModels
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
