using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitofWork _unitOfWork;
        public ProductController(IUnitofWork unitofWork)
        {
            _unitOfWork = unitofWork;
        }
        public IActionResult Index() // http:localhost:Port/Controller=Product/Action=Index
        {
            List<Product>  products = _unitOfWork.Product.GetAll().ToList();
            
            return View(products);
        }
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> categoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem // using Projection to explicitly change type
            {
                Text = u.Name,
                Value = u.Id.ToString(),
            });
            ViewBag.CategoryList = categoryList; // A ViewBag passes data from the controller to the view
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Created product successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id) // This is by default a GET action. Hence, when clicked on the Edit button it runs this function
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            // if id is not null 
            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Product product) {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(product);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(product);
        }
        
    }
}
