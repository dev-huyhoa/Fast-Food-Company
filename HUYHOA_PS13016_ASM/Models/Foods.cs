using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HUYHOA_PS13016_ASM.Models
{
    public class Foods
    {
        [Key]
        public int FoodID { get; set; }


        [Required(ErrorMessage = "Bạn chưa nhập tên")]
        [Column(TypeName = "varchar(50)"), MaxLength(50)]
        public string FoodName { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập số lượng")]
        public int FoodAmout { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập giá")]
        public int FoodPrice { get; set; }

        [Required(ErrorMessage = "Bạn chưa chọn hình ảnh"), Display(Name = "Images")]
        [Column(TypeName = "varchar(255)"), MaxLength(255)]
        public string FoodImage { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [MaxLength(300)]
        public string FoodNote { get; set; }
        public bool IsDelete { get; set; }
        public CategoryModel Category { get; set; }
    }
}
