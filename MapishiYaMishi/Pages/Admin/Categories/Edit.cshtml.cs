using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mishi.DataAccess.Data;
using Mishi.DataAccess.Repository.IRepository;
using Mishi.Models;

namespace MapishiYaMishi.Pages.Admin.Categories
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Category Category { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        private readonly IUnitOfWork _unitOfWork;

        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet(int id)
        {
            Category = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
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
            _unitOfWork.Category.Update(Category);
            _unitOfWork.Save();
            TempData["success"] = "Updated Successfully";
            return RedirectToPage("Index");
        }
    }
}
