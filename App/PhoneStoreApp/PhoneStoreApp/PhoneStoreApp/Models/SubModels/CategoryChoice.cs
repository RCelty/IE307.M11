using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneStoreApp.Models.SubModels
{
    public class CategoryChoice
    {
        public int? ID { get; set; }
        public string DisplayName { get; set; }
        public bool IsSelected { get; set; }
    }
}
