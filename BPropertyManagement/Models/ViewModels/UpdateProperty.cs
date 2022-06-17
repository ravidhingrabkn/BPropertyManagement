using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BPropertyManagement.Models.ViewModels
{
    public class UpdateProperty
    {
        public PropertyDto SelectedProperty { get; set; }

        public IEnumerable<RealtorDto> RealtorsOptions { get; set; }
       
    }
}