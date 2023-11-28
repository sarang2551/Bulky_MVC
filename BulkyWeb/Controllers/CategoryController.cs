using BulkyWeb.Data;
using Microsoft.AspNetCore.Mvc;
using BulkyWeb.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db= db;
        }
        public IActionResult Index() // http:localhost:Port/Controller=Category/Action=Index
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList); // returns a html file from the Views folder. Program searches directory named Category
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Display Order and Name cannot be the same");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Created category successfully";
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
            Category? categoryFromDb = _db.Categories.FirstOrDefault(u=>u.Id==id);
            if(categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb); // passes a Category object that auto populates the values in the Edit table
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if(ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Edited category successfully";
                return RedirectToAction("Index");
            }
            return View();
            
        }

        [ActionName("Delete")]
        public IActionResult DeleteCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            // if found then delete
            _db.Categories.Remove(categoryFromDb);
            _db.SaveChanges();
            TempData["success"] = "Deleted category successfully";
            return RedirectToAction("Index");

        }


    }
}
