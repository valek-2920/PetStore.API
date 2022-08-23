using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Pet_Store.Domains.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Pet_Store.Domains.Models.ViewModels
{
    public class ShoppingCartViewModel
    {

        public IEnumerable<ShoppingCart> ListCart { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ShoppingList { get; set; }

        public OrderHeader OrderHeader { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public Products Product { get; set; }

        public List<Products> products { get; set; }
    }
}
