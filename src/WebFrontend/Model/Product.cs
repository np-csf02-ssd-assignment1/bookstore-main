using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFrontend.Model
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string description { get; set; }
        public decimal Price { get; set; }
    }
}
