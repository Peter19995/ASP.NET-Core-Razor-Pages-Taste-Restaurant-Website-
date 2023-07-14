using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mishi.DataAccess.Data;
using Mishi.DataAccess.Repository.IRepository;
using Mishi.Models;

namespace MishiWeb.Pages.Admin.Foodtypes
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public FoodType FoodType { get; set; }

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
          
            if (!ModelState.IsValid)
            {
                return Page();
            }
             _unitOfWork.Foodtype.Add(FoodType);
             _unitOfWork.Save();
            TempData["success"] = "Created Successfully";
            return RedirectToPage("Index");
        }
    }
}
