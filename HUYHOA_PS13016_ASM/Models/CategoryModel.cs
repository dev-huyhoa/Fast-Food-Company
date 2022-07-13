using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUYHOA_PS13016_ASM.Models
{
    public class CategoryModel
    {
        [Key]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public ICollection<Foods> Foods { get; set; }
    }
}
