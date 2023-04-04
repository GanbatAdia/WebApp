using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.DataAccess.ViewModels
{
    public class AccountVM
    {
        public ApplicationUser ApplicationUser { get; set; }
        public string role { get; set; }
    }
}
