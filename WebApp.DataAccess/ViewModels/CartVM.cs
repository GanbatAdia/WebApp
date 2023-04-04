using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.DataAccess.ViewModels
{
    public class CartVM
    {
        public Cart Cart { get; set; } = new Cart();

        [ValidateNever]
        public IEnumerable<Cart> ListOfCart { get; set; } = new List<Cart>();

        [ValidateNever]
        public IEnumerable<SelectListItem>? Products { get; set; }
        public OrderHeader? OrderHeader { get; set; }
    }
}
