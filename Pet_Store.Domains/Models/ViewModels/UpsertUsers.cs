using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Domains.Models.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pet_Store.Domains.Models.ViewModels
{
    public class UpsertUsers
    {
        public Users Users { get; set; }

        public RegisterInputModel Register { get; set; }
    }
}
