using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HUYHOA_PS13016_ASM.Models
{
    public class CartDetails
    {
      

        [Key]
        public int CartDetailID { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ShipDate { get; set; }
        [Required]
        [Range(1, float.MaxValue, ErrorMessage = "Total price not valid")]
        public float TotalPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        [ForeignKey("Carts")]
        public int CartID { get; set; }
        [ForeignKey("Foods")]
        public int FoodID { get; set; }

        //navigation property
        public virtual Carts Carts { get; set; }
        public virtual Foods Foods { get; set; }
    }
}
