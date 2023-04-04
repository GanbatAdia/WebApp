using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.DataAccess.Repositories
{
    public interface ICartRepository: IRepository<Cart>
    {
        void Update(Cart cart);
        void IncrementCartItem(Cart cart, int countBy);
        void DecrementCartItem(Cart cart, int countBy);
    }
}
