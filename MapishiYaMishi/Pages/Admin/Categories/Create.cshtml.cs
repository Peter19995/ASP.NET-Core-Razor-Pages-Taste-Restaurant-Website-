using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mishi.DataAccess.Data;
using Mishi.DataAccess.Repository.IRepository;
using Mishi.Models;

namespace MapishiYaMishi.Pages.Admin.Categories
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Category Category { get; set; }
        private readonly IUnitOfWork _unitOfWork;

        public CreateModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if (Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Category.Name", "The display Order cannot exactly match teh Name.");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _unitOfWork.Category.Add(Category);
            _unitOfWork.Save();
            TempData["success"] = "Created Successfully";
            return RedirectToPage("Index");
        }
    }
}
