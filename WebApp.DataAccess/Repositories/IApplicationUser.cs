using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.DataAccess.Repositories
{
    public interface IApplicationUser: IRepository<ApplicationUser>
    {
        ApplicationUser getUser(string id);
    }
}
