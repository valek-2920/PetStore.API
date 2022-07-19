using Project_PetStore.API.Models.DataModels;
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
        public double ListPrice { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int Category { get; set; }

    }
}