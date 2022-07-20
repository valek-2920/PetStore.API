using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_PetStore.API.Models.DataModels
{
    public class OrderHeader
    {

        public OrderHeader()
        {
            OrderDate = DateTime.Now;
            ShippingDate = DateTime.Now.AddDays(15);
        }

        [Key]
        public int OrderId { get; set; }

        [Required]
        public Users User { get; set; }

        public DateTime OrderDate { get; set; } 

        public DateTime ShippingDate { get; set; }

        [Required]
        public int PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }


    }
}
