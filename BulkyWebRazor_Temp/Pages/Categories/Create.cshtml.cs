using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Category CategoryItem { get; set; }
        private readonly ApplicationDbContext _db;
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {

        }
        public IActionResult OnPost() { 
            if(ModelState.IsValid)
            {
                _db.Add(CategoryItem);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToPage("Index");
            }
            return RedirectToPage("Create");
        }

    }
}
