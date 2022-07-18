using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_PetStore.API.Models.DataModels;

namespace Pet_Store.Domains.Models.ViewModels
{
    public class SetRoles
    {
        public UserRoles userRoles { get; set; }
        public string RolName { get; set; }

    }
}
