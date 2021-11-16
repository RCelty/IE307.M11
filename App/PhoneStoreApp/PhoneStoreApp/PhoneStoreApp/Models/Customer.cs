using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneStoreApp.Models
{
    public class Customer
    {
        public int? ID { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string DisplayName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
    }
}
