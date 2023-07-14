using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mishi.DataAccess.Repository.IRepository;
using Mishi.Models;

namespace MishiWeb.Pages.Admin.MenuItems
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        [BindProperty]
        public MenuItem MenuItem { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> FoodTypeList { get; set; }


        

        public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            MenuItem = new MenuItem();
            _webHostEnvironment = webHostEnvironment;
        }

        public void OnGet( int? id)
        {
            if(id != null)
            {
                //Edit
                MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.Id == id);
            }
            CategoryList = _unitOfWork.Category.GetAll().Select(x => new SelectListItem {
                Text = x.Name,
                Value = x.Id.ToString(),
                });
            FoodTypeList = _unitOfWork.Foodtype.GetAll().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            });

        }

        public async Task<IActionResult> OnPost()
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            if(MenuItem.Id == 0)
            {
                //create
                string fileName_new = Guid.NewGuid().ToString();    
                var uploads = Path.Combine(webRootPath,@"images\menuitems" );
                var extension  = Path.GetExtension(files[0].FileName);
                using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                MenuItem.Image = @"images\menuitems\" + fileName_new + extension;
                _unitOfWork.MenuItem.Add(MenuItem);
                _unitOfWork.Save();
            }
            else
            {
                //edit
                var objFromDb  = _unitOfWork.MenuItem.GetFirstOrDefault(u=>u.Id == MenuItem.Id);

                //check if image is being updated too.
                if (files.Count > 0)
                {
                    string fileName_new = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\menuitems");
                    var extension = Path.GetExtension(files[0].FileName);

                    //delete the old image
                    var oldImagePath = Path.Combine(webRootPath, objFromDb.Image.Trim('\\'));
                    if(System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);    
                    }
                    //new upload
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    MenuItem.Image = @"images\menuitems\" + fileName_new + extension;
                }
                else
                {
                    MenuItem.Image = objFromDb.Image;
                }
                _unitOfWork.MenuItem.Update(MenuItem);
                _unitOfWork.Save();
            }
            return RedirectToPage("./Index");  
        }

    }
}
