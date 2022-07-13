using HUYHOA_PS13016_ASM.Interfaces;
using HUYHOA_PS13016_ASM.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUYHOA_PS13016_ASM.Services
{
    public class FoodSvc : IFoods
    {
        protected DataContext _dataContext;
        public FoodSvc(DataContext context)
        {
            _dataContext = context;
        }
        public async Task<List<Foods>> GetFoodAllAsync()
        {
            var dataContext = _dataContext.Foods.Include(m => m.Category);
            return await dataContext.ToListAsync(); 
        }

        public List<Foods> GetFoodAll()
        {
            List<Foods> list = new List<Foods>();
             list = _dataContext.Foods.Include(m => m.Category).ToList();
            return list;
        }
        public Foods GetFood(int id)
        {
            Foods food = null;
            food = _dataContext.Foods.Find(id);
            return food;
        }
        //public async Task<Foods> GetFoodAsync()
        //{

        //}
    }
}
