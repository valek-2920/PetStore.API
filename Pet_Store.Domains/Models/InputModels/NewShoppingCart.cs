using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pet_Store.Domains.Models.InputModels
{
    public class NewShoppingCart
    {

        [Required]
        public int Count { get; set; }

        [Required]
        public string Product { get; set; }

        [Required]
        public int UserId { get; set; }

    }
}
