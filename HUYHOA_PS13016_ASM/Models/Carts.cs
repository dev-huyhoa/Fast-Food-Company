using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUYHOA_PS13016_ASM.Models
{
    public class Carts
    {
        [Key]
        public int CartId { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        [MaxLength(255)]
        public string Note { get; set; }
        [Column(TypeName = "float")]
        [Range(0, float.MaxValue, ErrorMessage = "The value not valid")]
        public float TotalPrice { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime OrderDate { get; set; }
        [ForeignKey("Users")]
        public int UserID { get; set; }

        //navigation property
        public virtual Users User { get; set; }
        public virtual ICollection<CartDetails> CartDetails { get; set; }

    }
}
