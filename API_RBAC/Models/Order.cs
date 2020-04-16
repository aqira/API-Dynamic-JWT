using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace API_RBAC.Models
{
    [Table("Tb_M_Order")]
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTimeOffset OrderDate { get; set; }

        //M (Order) : O (Customer)
        [ForeignKey("Customer")]
        public string Customer_Id { get; set; }
        public virtual Customer Customer { get; set; }

        // O (Orders) : M (Orderline)
        public ICollection<Orderline> Orderlines { get; set; }
    }
}