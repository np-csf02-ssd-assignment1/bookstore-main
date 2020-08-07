using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebFrontend.Model
{
    public class Book : Product
    {
        public int BookID { get; set; }
        [RegularExpression("^[a-zA-Z-]*$", ErrorMessage = "Please enter a valid ISBN")]
        public string ISBN { get; set; }
        public List<Author> Authors { get; set; }
        public List<Publisher> Publishers { get; set; }
        [DataType(DataType.Date)]
        public DateTime PublishedOn { get; set; }
    }
}
