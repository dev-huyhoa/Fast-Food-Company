using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HUYHOA_PS13016_ASM.Models
{
    public enum Gender
    {
        [Display(Name = "Female")]
        Nam = 1,
        [Display(Name = "Male")]
        Nữ = 2
    }
    public class Users
    {
        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập tên"), MaxLength(55)]
        public string UserFullName { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập Email"), Display(Name = "Email")]
        [Column(TypeName = "varchar(50)"), MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Email không hợp lệ")]

        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu"), Display(Name = "Password")]
        [Column(TypeName = "varchar(55)"), MaxLength (50)]
        [DataType (DataType.Password)]
        public string UserPassWord { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Bạn chưa chọn giới tính")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Giá trị không hợp lệ"), Display(Name = "Day of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")] public DateTime UserBirthDay { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập số điện thoại"), Display(Name = "Phone Number")]
        [Column(TypeName = "varchar(15)"), MaxLength(15)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})[-. ]?([0-9]{4})[-. ]?([0-9]{3})$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string UsersPhone { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ"), Display(Name = "Address")]
        [MaxLength(255)]
        public string UserAddress { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập quyền"), Display(Name = "Role")]
        [ForeignKey("Roles")]
        public int RoleID { get; set; }
        public bool IsDelete { get; set; }

        public Roles Roles { get; set; }
        public ICollection<Carts> Carts { get; set; }

    }
}
