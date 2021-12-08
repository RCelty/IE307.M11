using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneStoreApp.Models
{
    public class Bill
    {
        public int? ID { get; set; }

        public long? TotalPrice { get; set; }

        public DateTime? CreationDate { get; set; }

        public int? CustomerID { get; set; }

        public string CustomerName { get; set; }

        public bool? IsDelete { get; set; }

        public List<BillDetail> BillDetails { get; set; }
    }
}
