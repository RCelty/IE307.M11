using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreAdmin.Model
{
    public class Comment
    {
        public int? ID { get; set; }

        public int? ProductID { get; set; }

        public int? CustomerID { get; set; }

        public string CustomerName { get; set; }

        public string Content { get; set; }

        public int? Rating { get; set; }
    }
}
