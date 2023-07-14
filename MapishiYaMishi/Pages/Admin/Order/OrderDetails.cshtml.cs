using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mishi.DataAccess.Repository.IRepository;
using Mishi.Models;
using Mishi.Models.ViewModel;
using Mishi.Utility;
using Stripe;

namespace MishiWeb.Pages.Admin.Order
{
    public class OrderDetailsModel : PageModel
    {
   
        private readonly IUnitOfWork _unitOfWork;
        public OrderDetailsVM OrderDetailsVM { get; set; }

        public OrderDetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet(int id)
        { 
            OrderDetailsVM = new()
            {
                OrderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == id, includeProperties: "User"),
                OrderDetails = _unitOfWork.OrderDetails.GetAll(u => u.OrderId == id).ToList()
            };
        }

        public IActionResult OnPostOrderCompleted(int id)
        {
            _unitOfWork.OrderHeader.UpdateStatus(id, SD.StatusCompleted);
            _unitOfWork.Save();
            return RedirectToPage("OrderList");
        }
        public IActionResult OnPostOrderCancel(int id)
        {
            _unitOfWork.OrderHeader.UpdateStatus(id, SD.StatusCancelled);
            _unitOfWork.Save();
            return RedirectToPage("OrderList");
        }

        public IActionResult OnPostOrderRefund(int id)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u=>u.Id == id);
            var options = new RefundCreateOptions
            {
                Reason = RefundReasons.RequestedByCustomer,
                PaymentIntent = orderHeader.PaymentIntentId
            };
            var service = new RefundService();
            Refund refund = service.Create(options);


            _unitOfWork.OrderHeader.UpdateStatus(id, SD.StatusRefund);
            _unitOfWork.Save();
            return RedirectToPage("OrderList");
        }
    }
}
