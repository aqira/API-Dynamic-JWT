using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace API_RBAC.Models
{
    [Table("Tb_M_Product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
        public int PurchasePrice { get; set; }
        public int SellingPrice { get; set; }
        public int Weight { get; set; }
        
        //O (Product) : M (Orderline)
        public virtual ICollection<Orderline> Orderlines { get; set; }
    }
}