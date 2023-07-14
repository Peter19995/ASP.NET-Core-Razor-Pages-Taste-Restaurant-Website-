using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mishi.DataAccess.Repository.IRepository;
using Mishi.Utility;

namespace MishiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public OrderController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;

        }

        [HttpGet]
        [Authorize]
        public IActionResult Get(string status = null)
        {
            var orderList = _unitOfWork.OrderHeader.GetAll(includeProperties: "User");
            if(status == "cancelled")
            {
                orderList = orderList.Where(u => u.Status == SD.StatusCancelled || u.Status == SD.StatusRejected);
            }
            else
            {
                if (status == "completed")
                {
                    orderList = orderList.Where(u => u.Status == SD.StatusCompleted);
                }
                else
                {
                    if (status == "ready")
                    {
                        orderList = orderList.Where(u => u.Status == SD.StatusReady);
                    }
                   
                }
            }
            
            return Json(new { data = orderList });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //Delete the Image
            var objFromDb = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.Id == id);
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, objFromDb.Image.Trim('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            //Remove the item
            _unitOfWork.MenuItem.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Success." });
        }
    }
}
