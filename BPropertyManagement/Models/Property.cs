using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BPropertyManagement.Models
{
    public class Property
    {
        [Key]
        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        
        public string Type { get; set; }

        public string Style { get; set; }

        // Size in Sqft
        public int Size { get; set; }

        //Price in $CAD
        public int ListPrice { get; set; }

        [ForeignKey("Realtor")]
        public int RealtorId { get; set; }
        public virtual Realtor Realtor { get; set; }

    }

    public class PropertyDto
    {
        public int PropertyId { get; set; } 

        public string PropertyName { get; set; }

        public string Type { get; set; }

        public string Style { get; set; }

        public int Size { get; set; }

        public int ListPrice { get; set; }

        public int RealtorId { get; set; }

        public string RealtorName { get; set; }
    }
}