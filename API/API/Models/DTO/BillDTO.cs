using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Models.EF;

namespace API.Models.DTO
{
    public class BillDTO
    {
        public int? ID { get; set; }

        public long? TotalPrice { get; set; }

        public DateTime? CreationDate { get; set; }

        public int? CustomerID { get; set; }

        public bool? IsDelete { get; set; }

        public List<BillDetailDTO> BillDetailDTOs { get; set; }

        public BillDTO()
        {

        }

        public BillDTO(Bill bill)
        {
            ID = bill.ID;
            TotalPrice = bill.TotalPrice;
            CreationDate = bill.CreationDate;
            CustomerID = bill.CustomerID;
            IsDelete = bill.IsDelete;
            BillDetailDTOs = bill.BillDetails.Select(b => new BillDetailDTO(b)).ToList();
        }
    }
}