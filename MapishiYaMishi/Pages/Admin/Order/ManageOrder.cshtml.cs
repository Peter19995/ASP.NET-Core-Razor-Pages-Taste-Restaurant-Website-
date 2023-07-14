using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mishi.DataAccess.Repository.IRepository;
using Mishi.Models;
using Mishi.Models.ViewModel;
using Mishi.Utility;

namespace MishiWeb.Pages.Admin.Order
{
    [Authorize(Roles =$"{SD.ManagerRole},{SD.KitchenRole}")]
    public class ManageOrderModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
       public List<OrderDetailsVM> OrderDetailsVM { get; set; }
        public ManageOrderModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet(int id)
        {
            OrderDetailsVM = new(); 
            List<OrderHeader> orderHeaders = new List<OrderHeader>();
            orderHeaders =_unitOfWork.OrderHeader.GetAll(u=>u.Status == SD.StatusSubmitted || u.Status == SD.StatusInProcess).ToList(); 
            foreach (OrderHeader item in orderHeaders) {
                OrderDetailsVM individual = new OrderDetailsVM()
                {
                    OrderHeader = item,
                    OrderDetails = _unitOfWork.OrderDetails.GetAll(u=>u.OrderId == item.Id).ToList(), 
                };
                OrderDetailsVM.Add(individual);
            }
        }

        public IActionResult OnPostOrderInProcess(int id)
        {
            _unitOfWork.OrderHeader.UpdateStatus(id, SD.StatusInProcess);
            _unitOfWork.Save();
            return RedirectToPage("ManageOrder");   
        }

        public IActionResult OnPostOrderReady(int id)
        {
            _unitOfWork.OrderHeader.UpdateStatus(id, SD.StatusReady);
            _unitOfWork.Save();
            return RedirectToPage("ManageOrder");
        }

        public IActionResult OnPostOrderCancel(int id)
        {
            _unitOfWork.OrderHeader.UpdateStatus(id, SD.StatusCancelled);
            _unitOfWork.Save();
            return RedirectToPage("ManageOrder");
        }
    }
}
