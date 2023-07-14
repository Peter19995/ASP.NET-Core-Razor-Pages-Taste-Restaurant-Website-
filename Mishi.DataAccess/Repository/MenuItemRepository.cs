using Mishi.DataAccess.Data;
using Mishi.DataAccess.Repository.IRepository;
using Mishi.Models;

namespace Mishi.DataAccess.Repository
{
    internal class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
    {
        private readonly ApplicationDbContext _db;
        public MenuItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(MenuItem item)
        {
            var objFromDb = _db.MenuItem.FirstOrDefault(u => u.Id == item.Id);
            objFromDb.Name = item.Name;
            objFromDb.Description = item.Description;
            objFromDb.Price = item.Price;
            objFromDb.CategoryId = item.CategoryId;
            objFromDb.FoodTypeId = item.FoodTypeId;
            if(objFromDb.Image != null)
            {
                objFromDb.Image = item.Image;
            }
            
        }
    }
}
