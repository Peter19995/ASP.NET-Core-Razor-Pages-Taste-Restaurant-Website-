using Mishi.DataAccess.Data;
using Mishi.DataAccess.Repository.IRepository;

namespace Mishi.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Foodtype = new FoodtypeRespository(_db);
            MenuItem = new MenuItemRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db);
            OrderDetails = new OrderDetailsRepository(_db);
            ApplicationUser  = new ApplicationUserRepository(_db);  
        }

        public ICategoryRepository Category { get; private set; }
        public IFoodtypeRespository Foodtype { get; private set; }
        public IMenuItemRepository MenuItem { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }   
        public IOrderHeaderRepository  OrderHeader { get; private set; }
        public IOrderDetailsRepository OrderDetails { get; private set; }

        public IApplicationUserRepository ApplicationUser { get; private set; }
        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
