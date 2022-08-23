using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pet_Store.Domains.Models.InputModels
{
    public class ResetPasswordInputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password Confirmation")]
        [Compare(otherProperty: "Password")]
        public string PasswordConfirmation { get; set; }
    }
}
