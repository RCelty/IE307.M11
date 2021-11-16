using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneStoreApp.Models.SubModels
{
    public class BrandChoice
    {
        public int ID { get; set; }

        public string DisplayName { get; set; }

        public string Image { get; set; }

        public bool IsSelected { get; set; }
    }
}
