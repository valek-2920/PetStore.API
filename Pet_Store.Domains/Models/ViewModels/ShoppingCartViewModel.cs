using Project_PetStore.API.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pet_Store.Domains.Models.ViewModels
{
    public class ShoppingCartViewModel
    {

        public IEnumerable<ShoppingCart> ListCart { get; set; }

        public OrderHeader OrderHeader { get; set; }
    }
}
