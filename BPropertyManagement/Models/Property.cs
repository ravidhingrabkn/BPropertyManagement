using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BPropertyManagement.Models
{
    public class Property
    {
        [Key]
        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        
        public string Type { get; set; }

        public string Style { get; set; }

        public int Size { get; set; }

        public int ListPrice { get; set; }

    }
}