using Mishi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mishi.DataAccess.Repository.IRepository
{
    public interface IFoodtypeRespository: IRepository<FoodType>
    {
        void Update(FoodType foodType);
        void Save();
    }
}
 