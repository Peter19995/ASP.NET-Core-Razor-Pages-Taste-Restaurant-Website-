using Mishi.DataAccess.Data;
using Mishi.DataAccess.Repository.IRepository;
using Mishi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mishi.DataAccess.Repository
{
    public class FoodtypeRespository: Repository<FoodType>,IFoodtypeRespository
    {
        private readonly ApplicationDbContext _db;
        public FoodtypeRespository(ApplicationDbContext db):base(db) 
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(FoodType foodType)
        {
            var objFromDb = _db.FoodTypes.FirstOrDefault(u => u.Id == foodType.Id);
            objFromDb.Name = foodType.Name;
         
        }
    }
}
