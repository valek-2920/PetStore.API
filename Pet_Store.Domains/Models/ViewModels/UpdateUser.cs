using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Project_PetStore.API.Models.DataModels;

namespace Pet_Store.Domains.Models.ViewModels
{
    public class UpdateUser
    {
        [Required]
        public int Id { get; set; }

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
        public string Address { get; set; }

    }
}
