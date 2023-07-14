using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mishi.DataAccess.Data;
using Mishi.DataAccess.Repository;
using Mishi.DataAccess.Repository.IRepository;
using Mishi.Models;

namespace MishiWeb.Pages.Admin.Foodtypes
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public FoodType FoodType { get; set; }

        private readonly IUnitOfWork _unitOfWork;

        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet(int id)
        {
            FoodType = _unitOfWork.Foodtype.GetFirstOrDefault(u => u.Id == id);
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _unitOfWork.Foodtype.Update(FoodType);
            _unitOfWork.Save();
             TempData["success"] = "Updated Successfully";
            return RedirectToPage("Index");
        }
    }
}
