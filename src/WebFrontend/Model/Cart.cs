using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebFrontend.Model
{
    public class Cart
    {
        [Key]
        public int ItemId { get; set; }
        public int CartId { get; set; }
        public string UserId { get; set; }
        public int Quantity { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        public int ProductId { get; set; }
        public bool Paid { get; set; }
    }
}
