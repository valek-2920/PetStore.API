using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_PetStore.API.Models.DataModels
{
    public class OrderDetails
    {

        [Key]
        public int OrderDetailsId { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public OrderHeader OrderHeader { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Products Product { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Total { get; set; }

        [Required]
        public double Taxes { get; set; }
    }
}
