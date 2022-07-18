using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_PetStore.API.Models.DataModels
{
    public class OrderHeader
    {

        [Key]
        public int OrderId { get; set; }

        [Required]
        public int UserId { get; set; }
        [ForeignKey("OrderUserId")]
        public Users User { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }
        public double OrderTotal { get; set; }
        public string? OrderStatus { get; set; }

        public string? PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }

        //public DateTime ShippingDate { get; set; }
        //public string? TrackingNumber { get; set; }
        //public string? Carrier { get; set; }
        //public DateTime PaymentDueDate { get; set; }

        //Stripe settings
        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }

        //Shipping info
        [Required]
        public int PhoneNumber { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string PostalCode { get; set; }

    }
}
