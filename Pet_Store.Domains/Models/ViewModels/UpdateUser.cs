﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Project_PetStore.API.Models.DataModels;

namespace Pet_Store.Domains.Models.ViewModels
{
    public class UpdateUser
    {
        public Users users { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public int Phone { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Address { get; set; }

     
    
        

    }
}
