using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pet_Store.Domains.Models.DataModels
{
    public class Payments
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string firstName { get; set; }

        [Required]
        public string lastName { get; set; }

        [Required]
        public int zipCode { get; set; }

        [Required]
        public long cardNumber { get; set; }

        [Required]
        public DateTime expirationDate { get; set; }

        [Required]
        public int CVV { get; set; }

        [Required]
        public Users user { get; set; }

    }
}
