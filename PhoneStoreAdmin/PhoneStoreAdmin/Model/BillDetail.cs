using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreAdmin.Model
{
    public class BillDetail
    {
        public int? ID { get; set; }

        public int? TotalCount { get; set; }

        public int? ProductID { get; set; }

        public string ProductName { get; set; }

        public int? Price { get; set; }

        public int? BillID { get; set; }
    }
}
