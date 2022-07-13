using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HUYHOA_PS13016_ASM.Models
{
    public class Roles
    {
        [Key]
        public int RoleID { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập tên"), MaxLength(255)]
        public string RoleName { get; set; }
        public ICollection<Users> Users { get; set; }
    }
}
