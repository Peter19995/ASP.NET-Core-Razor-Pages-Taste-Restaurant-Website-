using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mishi.DataAccess.Data;
using Mishi.DataAccess.Repository.IRepository;
using Mishi.Models;

namespace MishiWeb.Pages.Admin.Foodtypes
{
    public class IndexModel : PageModel
    {

        public IEnumerable<FoodType> FoodTypes { get; set; }

        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {
            FoodTypes = _unitOfWork.Foodtype.GetAll();
        }
    }
}
