﻿using Pet_Store.Domains.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pet_Store.Domains.Models.ViewModels
{
    public class RoleViewModel
    {
        public IEnumerable<Role> Roles { get; set; }
    }
}
