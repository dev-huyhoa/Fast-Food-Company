using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUYHOA_PS13016_ASM.Models
{
    public class Products
    {
        private DataContext db = new DataContext();
        public List<Foods> FindAll()
        {
            var a = db.Foods.ToList();
            return a;
        }
        public Foods Find(int id)
        {
            var a = db.Foods.Find(id);
            return a;
        }
    }
}
