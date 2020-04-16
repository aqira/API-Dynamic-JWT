using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace API_RBAC.Models
{
    [Table("Tb_T_Orderline")]
    public class Orderline
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }

        //M (Orderline) : O (Order)
        [ForeignKey("Order")]
        public int Order_Id { get; set; }
        public virtual Order Order { get; set; }

        //M (Orderline) : O (Product)
        [ForeignKey("Product")]
        public int Product_Id { get; set; }
        public virtual Product Product { get; set; }

    }
}