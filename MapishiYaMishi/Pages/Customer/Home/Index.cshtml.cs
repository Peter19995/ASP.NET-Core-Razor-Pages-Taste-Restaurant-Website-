using Microsoft.AspNetCore.Mvc.RazorPages;
using Mishi.DataAccess.Repository.IRepository;
using Mishi.Models;

namespace MishiWeb.Pages.Customer.Home
{
	public class IndexModel : PageModel
	{
		public IEnumerable<MenuItem> MenuItemList { get; set; }
		public IEnumerable<Category> CategoryList { get; set; }

		private readonly IUnitOfWork _unitOfWork;

		public IndexModel(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public void OnGet()
		{
			MenuItemList = _unitOfWork.MenuItem.GetAll(includeProperties: "Category,FoodType");
			CategoryList = _unitOfWork.Category.GetAll(orderby: u=>u.OrderBy(c =>c.DisplayOrder));
		}
	}
}
