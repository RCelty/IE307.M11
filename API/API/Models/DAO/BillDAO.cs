using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using API.Models.DTO;
using API.Models.EF;

namespace API.Models.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BillDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        PhoneStoreEntities1 db = new PhoneStoreEntities1();

        public async Task<List<BillDTO>> GetAllBill()
        {
            var resulList = (await db.Bills
                        .ToListAsync())
                        .Select(b => new BillDTO(b))
                        .ToList();
            resulList = resulList.FindAll(b => b.IsDelete == false);
            return resulList;
        }

        public async Task<int> CreateBill(BillDTO billDTO)
        {
            Bill bill = new Bill()
            {
                TotalPrice = billDTO.TotalPrice,
                CreationDate = DateTime.Now,
                CustomerID = billDTO.CustomerID,
                IsDelete = false,
                IsCheckOut = false
            };
            try
            {
                db.Bills.Add(bill);
                await db.SaveChangesAsync();
                return bill.ID;
            }
            catch (Exception e)
            {
                return -1;
                throw e;
            }
        }

        public async Task<List<BillDTO>> GetAllBillByCustomerID(int ID)
        {
            var resultList = (await db.Bills
                .ToListAsync())
                .Select(b => new BillDTO(b))
                .ToList();
            resultList = resultList.FindAll(b => b.CustomerID == ID);
            return resultList;
        }
    }
}