using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Models.EF;

namespace API.Models.DTO
{
    public class BillDetailDTO
    {
        public int? ID { get; set; }

        public int? TotalCount { get; set; }

        public int? ProductID { get; set; }
        
        public int? BillID { get; set; }

        public BillDetailDTO()
        {

        }

        public BillDetailDTO(BillDetail billDetail)
        {
            ID = billDetail.ID;
            TotalCount = billDetail.TotalCount;
            ProductID = billDetail.ProductID;
            BillID = billDetail.BillID;
        }
    }
}