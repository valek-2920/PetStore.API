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

        public NewShoppingCart()
        {

        }
        public NewShoppingCart( int count, double subtotal , string producto)
        {
            Count = count;
            Subtotal = subtotal;
            Product = producto;
        }
        [Required]
        public int Count { get; set; }

        [Required]
        public string Product { get; set; }

        [Required]

        public int UserId { get; set; }

        [Required]
        public double Subtotal { get; set; }


    }
}
