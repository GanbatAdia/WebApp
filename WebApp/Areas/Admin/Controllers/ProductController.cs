using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.DataAccess.Repositories;
using WebApp.DataAccess.ViewModels;

namespace WebApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Employee")]
    public class ProductController : Controller
    {
        private IUnitOfWork _unitofWork;
        private IWebHostEnvironment _hostingEnviroment;

        public ProductController(IUnitOfWork unitofWork, IWebHostEnvironment hostEnvironment)
        {
            _unitofWork = unitofWork;
            _hostingEnviroment = hostEnvironment;
        }
        #region APICALL
        public IActionResult AllProducts()
        {
            var products = _unitofWork.Product.GetAll(includeProperties: "Category");
            return Json(new { data = products });
        }
        #endregion
        public IActionResult Index()
        {
            //ProductVM productVM = new ProductVM();
            //productVM.Products = _unitofWork.Product.GetAll();
            return View();
        }
        [HttpGet]
        public IActionResult CreateUpdate(int? id)
        {
            ProductVM vm = new ProductVM()
            {
                Product = new(),
                Categories = _unitofWork.Category.GetAll().Select(x =>
                new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };

            if (id == null || id == 0)
            {
                return View(vm);
            }
            else
            {
                vm.Product = _unitofWork.Product.GetT(x => x.Id == id);
                if (vm.Product == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(vm);
                }
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUpdate(ProductVM vm, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string fileName = String.Empty;
                if (file != null)
                {
                    string uploadDir = Path.Combine(_hostingEnviroment.WebRootPath, "ProductImage");
                    fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                    string filePath = Path.Combine(uploadDir, fileName);

                    if(vm.Product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(_hostingEnviroment.WebRootPath, vm.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath); 
                        }
                    }
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    vm.Product.ImageUrl = @"\ProductImage\"+fileName;
                }
                if (vm.Product.Id == 0)
                {
                    _unitofWork.Product.Add(vm.Product);
                    TempData["success"] = "Product Created Done!";
                }
                else
                {
                    _unitofWork.Product.Update(vm.Product);
                    TempData["success"] = "Product Update Done!";
                }

                _unitofWork.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        #region DeleteAPICALL
        [HttpDelete]
        [Authorize(Roles ="Admin")]
        public IActionResult Delete(int? id)
        {
            var product = _unitofWork.Product.GetT(x => x.Id == id);
            if (product == null)
            {
                return Json(new {success = false, message = "Error in Fetching Data"});
            }
            else
            {
                var oldImagePath = Path.Combine(_hostingEnviroment.WebRootPath, product.ImageUrl.TrimStart('\\'));
                if(System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
                _unitofWork.Product.Delete(product);
                _unitofWork.Save();
                return Json(new { success = true, message = "Product Deleted" });
            }
            
        }
        #endregion
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public IActionResult CreateData(int? id)
        //{
        //    var product = _unitofWork.Product.GetT(x => x.Id == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    _unitofWork.Product.Delete(product);
        //    _unitofWork.Save();
        //    TempData["success"] = "Product Deleted Done";
        //    return RedirectToAction("Index");
        //}

    }
}
