using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.Domain.Models.ViewModels
{
    public class NewProduct
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int ListPrice { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public string Category { get; set; }

    }
}