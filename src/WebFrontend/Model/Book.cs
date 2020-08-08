using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebFrontend.Attributes;

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
        [AllowedExtensions(new string[] { ".pdf" })]
        public IFormFile PDFFile { get; set; }
    }
}
