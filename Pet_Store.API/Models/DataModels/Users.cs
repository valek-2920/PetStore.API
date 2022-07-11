using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_PetStore.API.Models.DataModels
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public int Phone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        /*Foreign keys*/
        [Required]
        public int RolesId { get; set; }

        [ForeignKey("RolesId")]
        [ValidateNever]
        public UserRoles Role { get; set; }

        [Required]
        public int DirectionId { get; set; }

        [ForeignKey("DirectionId")]
        [ValidateNever]
        public UserDirection Directions { get; set; }


    }
}
