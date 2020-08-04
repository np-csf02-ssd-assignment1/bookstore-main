using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebFrontend.Model
{
    public class Order
    {
        public int OrderID { get; set; }
        public string UserID { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedTime { get; set; }
        public List<ShipmentType> ShipmentID { get; set; }
        public List<PaymentType> PaymentID { get; set; }
        public string ShippingAddress { get; set; }
        public string BillingAddress { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
    }
}
