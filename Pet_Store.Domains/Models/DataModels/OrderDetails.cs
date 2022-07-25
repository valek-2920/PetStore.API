using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_PetStore.API.Models.DataModels
{
    public class OrderDetails
    {

        [Key]
        public int OrderDetailsId { get; set; }

        [Required]
        public OrderHeader OrderHeader { get; set; }

        [Required]
        public List<Products> Product { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Total { get; set; }

    }
}
