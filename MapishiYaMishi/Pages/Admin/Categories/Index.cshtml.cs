using Microsoft.AspNetCore.Mvc.RazorPages;
using Mishi.DataAccess.Data;
using Mishi.DataAccess.Repository.IRepository;
using Mishi.Models;

namespace MapishiYaMishi.Pages.Admin.Categories
{
    public class IndexModel : PageModel
    {
        public IEnumerable<Category> Categories { get; set; }

        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {
            Categories = _unitOfWork.Category.GetAll();
        }
    }
}
