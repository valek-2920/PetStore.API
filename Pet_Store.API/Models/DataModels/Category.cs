using System.ComponentModel.DataAnnotations;

namespace Project_PetStore.API.Models.DataModels
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
