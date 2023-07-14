using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mishi.DataAccess.Data;
using Mishi.DataAccess.Repository;
using Mishi.DataAccess.Repository.IRepository;
using Mishi.Models;

namespace MapishiYaMishi.Pages.Admin.Categories
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public Category Category { get; set; }

        private readonly IUnitOfWork _unitOfWork;

        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet(int id)
        {
            Category = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
        }
        public async Task<IActionResult> OnPost()
        {
            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == Category.Id);
            if (categoryFromDb != null)
            {
                _unitOfWork.Category.Remove(categoryFromDb);
                _unitOfWork.Save();
                TempData["success"] = "Deleted Successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
