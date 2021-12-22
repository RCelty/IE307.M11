using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using API.Assets.Contain;
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

            Customer customer = db.Customers.SingleOrDefault(c => c.ID == bill.CustomerID);
            bill.Customer = customer;

            db.Entry(bill).State = EntityState.Added;

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

        public async Task<bool> DeleteBill(int ID)
        {
            var result = db.Bills.SingleOrDefault(b => b.ID == ID);
            try
            {
                result.IsDelete = true;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

        public async Task<bool> RestoreAllBill()
        {
            var List = (await db.Bills
                        .ToListAsync())
                        .Select(b => new BillDTO(b))
                        .ToList();

            try
            {
                var DeletedList = db.Bills.Where(b => b.IsDelete == true).ToList();
                DeletedList.ForEach(b => b.IsDelete = false);
                db.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

        public async Task<List<BillDTO>> GetAllBillByCustomerID(int ID)
        {
            var resultList = (await db.Bills
                .ToListAsync())
                .Select(b => new BillDTO(b))
                .ToList();
            resultList = resultList.FindAll(b => b.CustomerID == ID && b.IsDelete == false);
            return resultList;
        }

        public async Task<bool> ChangeBillStatus(int ID)
        {
            var result = db.Bills.SingleOrDefault(b => b.ID == ID);
            try
            {
                result.IsCheckOut = !result.IsCheckOut;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

        public async Task<bool> SendOrderConfirmMail(int ID)
        {
            try
            {
                var bill = db.Bills.SingleOrDefault(b => b.ID == ID);
                var billDTO = new BillDTO(bill);
                var billDetailList = (await db.BillDetails
            .ToListAsync())
            .Select(b => new BillDetailDTO(b))
            .ToList();
                billDetailList = billDetailList.FindAll(b => b.BillID == ID);
                var customer = db.Customers.SingleOrDefault(c => c.ID == billDTO.CustomerID);
                string billStatus = "";
                if (bill.IsCheckOut == true)
                {
                    billStatus += "Đã thanh toán";
                }
                else
                {
                    billStatus += "Chưa thanh toán";
                }


                var body = "";

                body += "<hr/>";
                body += "Xin chào <b>" + billDTO.CustomerName + "</b>,<br/><br/>";

                body += "Cảm ơn bạn đã chọn mua hàng của ChoTech,<br/><br/>";
                body += "Chi tiết đơn hàng của bạn:<br/><br/>";

                body += @"
                        <table style='width: 100%; border:1px solid rgb(2, 172, 234); border-collapse: separate; border-spacing: 0;'>
                            <tr style='background-color: rgb(2, 172, 234); color: white;'>
                                <th style='text-align: left;'>SẢN PHẨM</th> 
                                <th style='text-align: left;'>ĐƠN GIÁ</th> 
                                <th style='text-align: left;'>SỐ LƯỢNG</th>                                  
                            </tr>";

                foreach (var item in billDetailList)
                {
                    body += $@"
                            <tr style='margin-top:10px; border-bottom: 1px solid black;'>
                                <td style='text-align: left; color: black;'>{item.ProductName}</td>
                                <td style='text-align: left; color: black;'>{Convert.ToDecimal(item.Price).ToString("#,##")} đ</td>
                                <td style='text-align: left; color: black;'>{item.TotalCount}</td>                                 
                            </tr>";
                }
                body += $@"
                    <tr style='border-top: 1px solid rgb(2, 172, 234);'>
                        <td colspan='2'><h4>Tổng giá trị đơn hàng</h4></td>                    
                        <td>{Convert.ToDecimal(bill.TotalPrice).ToString("#,##")}đ</td>                    
                    </tr>";
                body += $@"
                    <tr style='border-top: 1px solid rgb(2, 172, 234);'>
                        <td colspan='2'><h4>Tình trạng thanh toán</h4></td>                    
                        <td style='text-align: left; color: black;'>{billStatus}</td>                  
                    </tr>";
                body += "</table> <br/><br/>";

                body += "-----------------------------<br/>";
                body += "<b>D.A Nguyen - Leader</b><br/>";
                body += "<b>Phone:</b> (84) 00 7143 619<br/>";
                body += "<b>Facebook: </b><a href=" + "https://www.facebook.com/duyanh.nguyenngoc.14/" + ">https://www.facebook.com/duyanh.nguyenngoc.14/<a/>";
                body += "<hr/>";

                var result = Const.SendMail(customer.Email, "Xác nhận đặt hàng - ChoTech", body);
                if (result)
                    return true;
                else
                    return false;

            }
            catch (Exception e)
            {
                return false;
                throw e;
            }

        }
    }
}