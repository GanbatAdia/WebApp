using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.DataAccess.Data;
using WebApp.Models;

namespace WebApp.DataAccess.Repositories
{
    public class ApplicationUserRepository: Repository<ApplicationUser>, IApplicationUser
    {
        private ApplicationDbContext _context;
        public ApplicationUserRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
        
        public ApplicationUser getUser(string id)
        {
            ApplicationUser appUser= _context.ApplicationUsers.FirstOrDefault(x => x.Id == id);
            return appUser;
        }
    }
}
