
using Mishi.Models;

namespace Mishi.DataAccess.Repository.IRepository
{
    public interface IMenuItemRepository : IRepository<MenuItem>
    {
        void Update(MenuItem item);
    }
}
