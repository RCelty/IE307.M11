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
    public class BillDetailDAO
    {
        private static BillDetailDAO instance;

        public static BillDetailDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BillDetailDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        PhoneStoreEntities1 db = new PhoneStoreEntities1();

        public async Task<List<BillDetailDTO>> GetAllBillDetail()
        {
            var resulList = (await db.BillDetails
                        .ToListAsync())
                        .Select(b => new BillDetailDTO(b))
                        .ToList();
            
            return resulList;
        }

        public async Task<List<BillDetailDTO>> GetAllBillDetailByBillID(int ID)
        {
            var resultList = (await db.BillDetails
                .ToListAsync())
                .Select(b => new BillDetailDTO(b))
                .ToList();
            resultList = resultList.FindAll(b => b.BillID == ID);
            return resultList;
        }

        public async Task<List<BillDetailDTO>> GetAllBillDetailByProductID(int ID)
        {
            var resultList = (await db.BillDetails
                .ToListAsync())
                .Select(b => new BillDetailDTO(b))
                .ToList();
            resultList = resultList.FindAll(b => b.ProductID == ID);
            return resultList;
        }

        public async Task<int> AddBillDetail(BillDetailDTO billDetailDTO)
        {
            BillDetail billDetail = new BillDetail()
            {
                TotalCount = billDetailDTO.TotalCount,
                ProductID = billDetailDTO.ProductID,
                BillID = billDetailDTO.BillID
            };
            try
            {
                db.BillDetails.Add(billDetail);
                await db.SaveChangesAsync();
                return billDetail.ID;
            }
            catch (Exception e)
            {
                return -1;
                throw e;
            }
        }

        public async Task<bool> DeleteBillDetail(BillDetailDTO billDetailDTO)
        {
            try
            {
                var billDetail = await db.BillDetails.SingleOrDefaultAsync(b => b.ProductID == billDetailDTO.ProductID && b.BillID == billDetailDTO.BillID);
                if (billDetail != null)
                {
                    db.BillDetails.Remove(billDetail);
                    await db.SaveChangesAsync();
                    return true;
                }
                else return false;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

        public async Task<bool> DeleteBillDetailByID(int ID)
        {
            var result = db.BillDetails.SingleOrDefault(b => b.ID == ID);

            try
            {
                db.BillDetails.Remove(result);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }
    }
}