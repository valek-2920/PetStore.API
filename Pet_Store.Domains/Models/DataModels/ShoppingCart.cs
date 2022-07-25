using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_PetStore.API.Models.DataModels
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public Products Product { get; set; }

        [Required]
        public Users User { get; set; }

        [NotMapped] 
        public double Subtotal { get; set; }
    }
}
