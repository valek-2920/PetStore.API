using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pet_Store.Domains.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pet_Store.Domains.Models.ViewModels
{
    public class UsersViewModel
    {
        public Users Users { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}
