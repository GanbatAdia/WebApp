using Microsoft.AspNetCore.Mvc;
using WebApp.DataAccess.Repositories;
using WebApp.DataAccess.ViewModels;

namespace WebApp.Web.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private IUnitOfWork _unitofWork;
        public AccountController(IUnitOfWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
