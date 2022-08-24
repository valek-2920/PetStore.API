using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pet_Store.Domains.Models.DataModels
{
    public class ShoppingCart
    {
        [Key]
        [Display(Name = "Carrito#")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Cantidad")]
        public int Count { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        [ValidateNever]
        public Products Product { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        [ValidateNever]
        public Users User { get; set; }

        [Display(Name = "Subtotal")]
        public double Subtotal { get; set; }

    }
}
