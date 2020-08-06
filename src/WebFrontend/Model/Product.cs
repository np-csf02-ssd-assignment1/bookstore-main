using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebFrontend.Data;

namespace WebFrontend.Model
{
    public class Product
    {
        public int ID { get; set; }
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Please enter a valid name")]
        public string Name { get; set; }
        [RegularExpression("^[a-zA-Z., ]*$", ErrorMessage = "Please enter a valid description")]
        public string Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}
