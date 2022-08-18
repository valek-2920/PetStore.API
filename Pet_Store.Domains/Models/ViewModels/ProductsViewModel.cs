using Pet_Store.Domains.Models.DataModels;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pet_Store.Domains.Models.ViewModels
{
    public class ProductsViewModel
    {
        public Products Product { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
