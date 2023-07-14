using Microsoft.AspNetCore.Mvc;
using Mishi.DataAccess.Repository.IRepository;
using Mishi.Utility;
using System.Security.Claims;

namespace MishiWeb.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdenity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdenity.FindFirst(ClaimTypes.NameIdentifier);
            int count = 0;
            if (claim != null)
            {
                //user is loged in
                if(HttpContext.Session.GetInt32(SD.SessionCart) != null)
                {
                    return View(HttpContext.Session.GetInt32(SD.SessionCart));
                }
                else
                {
                    count = _unitOfWork.ShoppingCart.GetAll(u=>u.ApplicationUserId == claim.Value).ToList().Count;  
                    HttpContext.Session.SetInt32(SD.SessionCart, count);
                    return View(count);
                }
            }
            else
            {
                HttpContext.Session.Clear();
                return View(count);
            }
        }
    }
}
