using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mishi.DataAccess.Repository.IRepository;
using Mishi.Models;
using Mishi.Utility;
using System.Security.Claims;

namespace MishiWeb.Pages.Customer.Home
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        //public IEnumerable<Category> Categories { get; set; }

        private readonly IUnitOfWork _unitOfWork;

        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public ShoppingCart ShoppingCart { get; set; }
        public void OnGet(int id)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
           
            ShoppingCart = new()
            {
                ApplicationUserId = claim.Value,
                MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(x => x.Id == id, includeProperties: "Category,FoodType"),
                MenuItemId = id,
            };

        }

         
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                ShoppingCart shoppingCartFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(filter: u => u.ApplicationUserId == ShoppingCart.ApplicationUserId && u.MenuItemId == ShoppingCart.MenuItemId);
                if(shoppingCartFromDb == null)
                {
                    _unitOfWork.ShoppingCart.Add(ShoppingCart);
                    _unitOfWork.Save();
                    HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCart.GetAll(U=>U.ApplicationUserId == ShoppingCart.ApplicationUserId).ToList().Count());
                }
                else
                {
                    _unitOfWork.ShoppingCart.IncrementCount(shoppingCartFromDb,ShoppingCart.Count);
                }

               

                return RedirectToPage("Index");
            }
            return Page();
           
        }
    }
}
 