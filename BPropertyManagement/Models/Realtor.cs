using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BPropertyManagement.Models
{
    public class Realtor
    {
        [Key]
        public int RealtorId { get; set; }

        public string RealtorName { get; set;}

        public string Phone { get; set; }

        public string Email { get; set; }

        
    }
    public class RealtorDto
    {
        public int RealtorId { get; set; }

        public string RealtorName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        
    }
}