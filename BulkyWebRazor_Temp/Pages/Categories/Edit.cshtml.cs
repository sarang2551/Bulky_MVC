using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Category CategoryItem{ get; set; }
        private readonly ApplicationDbContext _db;
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int? id)
        {
            Category obj = _db.Categories.FirstOrDefault(c => c.Id == id);
            CategoryItem = obj;

        }
        public IActionResult OnPost()
        {
            if(ModelState.IsValid)
            {
                _db.Update(CategoryItem);
                _db.SaveChanges();
                TempData["success"] = "Category editted successfully";
                return RedirectToPage("Index");
            }
            return RedirectToPage("Edit");
        }
    }
}
