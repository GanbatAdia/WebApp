using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.DataAccess.Data;
using WebApp.Models;

namespace WebApp.DataAccess.Repositories
{
    internal class CartRepository : Repository<Cart>, ICartRepository
    {
        private ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(Cart cart)
        {
            var cartDb = _context.Carts.FirstOrDefault(x => x.Id == cart.Id);
            if (cartDb != null)
            {
                cartDb.ProductId = cart.ProductId;
                cartDb.Product = cart.Product ;
                cartDb.ApplicationUser = cart.ApplicationUser;
                cartDb.ApplicationUserId = cart.ApplicationUserId;
                
            }
        }
       
        public void IncrementCartItem(Cart cart, int countBy)
        {
            var cartDb = _context.Carts.FirstOrDefault(x => x.Id == cart.Id);
            if (cartDb != null)
            {
                cartDb.Count = cart.Count + countBy;

            }
        }
        public void DecrementCartItem(Cart cart, int countBy)
        {
            var cartDb = _context.Carts.FirstOrDefault(x => x.Id == cart.Id);
            if (cartDb != null)
            {
                cartDb.Count = cart.Count - countBy;

            }
        }
    }
}
