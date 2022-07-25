using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.Domain.Models.ViewModels
{
    public class NewShoppingCart
    {

        [Required]
        public int Count { get; set; }

        [Required]
        public string Product { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public double Subtotal { get; set; }

    }
}
