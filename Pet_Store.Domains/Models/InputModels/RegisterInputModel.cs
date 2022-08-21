using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pet_Store.Domains.Models.InputModels
{
    public class RegisterInputModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int Phone { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        [Remote(action: "checkemail", controller: "accounts", ErrorMessage = "Email already exists! Please try again with a different email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password Confirmation")]
        [System.ComponentModel.DataAnnotations.Compare(otherProperty: "Password")]
        public string PasswordConfirmation { get; set; }
    }
}
