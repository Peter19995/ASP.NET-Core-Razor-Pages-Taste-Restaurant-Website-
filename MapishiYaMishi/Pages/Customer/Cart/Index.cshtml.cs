using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mishi.DataAccess.Repository.IRepository;
using Mishi.Models;
using Mishi.Utility;
using System.Security.Claims;

namespace MishiWeb.Pages.Customer.Cart
{
    [Authorize]
    public class IndexModel : PageModel
    {

        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        public double CartTotal { get; set; }           
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CartTotal =0;
        }

        public void OnGet()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if(claim != null) 
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: u=> u.ApplicationUserId == claim.Value, includeProperties: "MenuItem,MenuItem.FoodType,MenuItem.Category");
                foreach(var cart in ShoppingCartList)
                {
                    CartTotal += (cart.MenuItem.Price * cart.Count);
                }
            }
        }

        public IActionResult OnPostPlus( int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            _unitOfWork.ShoppingCart.IncrementCount(cart,1);
            return RedirectToPage("/Customer/Cart/Index");
        }
        public IActionResult OnPostMinus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            if (cart.Count >1)
            {
                _unitOfWork.ShoppingCart.DecrementCount(cart, 1);
            }
            else
            {
                var count = _unitOfWork.ShoppingCart.GetAll(U => U.ApplicationUserId == cart.ApplicationUserId).ToList().Count() - 1;

                _unitOfWork.ShoppingCart.Remove(cart);
                _unitOfWork.Save();
                HttpContext.Session.SetInt32(SD.SessionCart, count);
            }
            return RedirectToPage("/Customer/Cart/Index");
        }

        public IActionResult OnPostRemove(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            var count = _unitOfWork.ShoppingCart.GetAll(U => U.ApplicationUserId == cart.ApplicationUserId).ToList().Count()-1;

            _unitOfWork.ShoppingCart.Remove(cart);
            _unitOfWork.Save();
            HttpContext.Session.SetInt32(SD.SessionCart, count);
            return RedirectToPage("/Customer/Cart/Index");
        }
    }
}