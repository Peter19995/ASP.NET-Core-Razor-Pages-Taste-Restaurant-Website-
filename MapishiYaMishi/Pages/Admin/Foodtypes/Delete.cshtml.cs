using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mishi.DataAccess.Data;
using Mishi.DataAccess.Repository;
using Mishi.DataAccess.Repository.IRepository;
using Mishi.Models;

namespace MishiWeb.Pages.Admin.Foodtypes
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public FoodType FoodType { get; set; }

        private readonly IUnitOfWork _unitOfWork;

        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet(int id)
        {
            FoodType = _unitOfWork.Foodtype.GetFirstOrDefault(u => u.Id == id);
        }
        public async Task<IActionResult> OnPost()
        {

            var foodTypeDb = _unitOfWork.Foodtype.GetFirstOrDefault(u => u.Id == FoodType.Id);
            if (foodTypeDb != null)
            {
                _unitOfWork.Foodtype.Remove(foodTypeDb);
                _unitOfWork.Save();
                 TempData["success"] = "Deleted Successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
