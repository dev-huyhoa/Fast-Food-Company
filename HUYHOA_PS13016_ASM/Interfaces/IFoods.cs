using HUYHOA_PS13016_ASM.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HUYHOA_PS13016_ASM.Interfaces
{
    public interface IFoods
    {
         Task<List<Foods>> GetFoodAllAsync();

        List<Foods> GetFoodAll();

        Foods GetFood(int id);
    }
}
