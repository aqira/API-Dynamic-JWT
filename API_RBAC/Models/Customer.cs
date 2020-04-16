using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace API_RBAC.Models
{
    [Table("Tb_M_Customer")]
    public class Customer
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string PhoneNumber { get; set; }
        
        //O (Customer): M (Orders)
        public ICollection<Order> Orders { get; set; }
    }
}