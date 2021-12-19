using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PhoneStoreApp.Assets.Contains;
using PhoneStoreApp.Models;
using PhoneStoreApp.Models.SubModels;

namespace PhoneStoreApp.Services
{
    public class CartService
    {
        private static CartService instance;

        public static CartService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CartService();
                }
                return instance;
            }
            private set => instance = value;
        }

        public async Task<List<CartItem>> GetAllCart()
        {
            try
            {
                List<CartItem> cartItems = new List<CartItem>();
                var cartList = Const.cartProducts;
                foreach (var p in cartList)
                {
                    cartItems.Add(new CartItem()
                    {
                        ID = p.ID,
                        DisplayName = p.DisplayName,
                        Image1 = p.Image1,
                        Price = p.Price,
                        DiscountPercent =
                        p.DiscountPercent,
                        DiscountPrice = p.DiscountPrice,
                        CommentCount = p.CommentCount,
                        Rating = p.Rating,
                        Count = 1,
                        IsSelected = false
                    });
                }
                return cartItems;
            }
            catch(Exception e)
            {
                return null;
                throw e;
            }
        } 
        
        public async Task<int> CreateBill(Bill bill)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.CreateBillPath);

                    var myContent = JsonConvert.SerializeObject(bill);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var result = client.PostAsync(convertString, byteContent).Result.Content.ReadAsStringAsync().Result;

                    var resultID = JsonConvert.DeserializeObject<int>(result);

                    return resultID;
                }
                catch (Exception e)
                {
                    return -1;
                    throw e;
                }
            }
        }

        public async Task<List<Bill>> GetBillByCustomerID(int ID)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dataString = await client.GetStringAsync(Const.ConverToPathWithParameter(Const.GetAllBillByCustomerIDPath, new object[] { ID }));

                    var resultList = JsonConvert.DeserializeObject<List<Bill>>(dataString);

                    return resultList;
                }
                catch (Exception e)
                {
                    return null;
                    throw e;
                }
            }
        }

        public async Task<List<BillDetail>> GetBillDetailByBillID(int ID)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dataString = await client.GetStringAsync(Const.ConverToPathWithParameter(Const.GetAllBillDetailByBillIDPath, new object[] { ID }));

                    var resultList = JsonConvert.DeserializeObject<List<BillDetail>>(dataString);

                    return resultList;
                }
                catch (Exception e)
                {
                    return null;
                    throw e;
                }
            }
        }

        public async Task<int> AddBillDetail(BillDetail billDetail)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.AddBillDetailPath);

                    var myContent = JsonConvert.SerializeObject(billDetail);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var result = client.PostAsync(convertString, byteContent).Result.Content.ReadAsStringAsync().Result;

                    var resultID = JsonConvert.DeserializeObject<int>(result);

                    return resultID;
                }
                catch (Exception e)
                {
                    return -1;
                    throw e;
                }
            }
        }

        public async Task<bool> DeleteBillDetail(int ID)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.DeleteBillDetailByIDPath, new object[] { ID });

                    var dataString = await client.GetStringAsync(convertString);

                    var result = JsonConvert.DeserializeObject<bool>(dataString);

                    return result;
                }
                catch (Exception e)
                {
                    return false;
                    throw e;
                }
            }
        }

        public async Task<bool> ChangeBillStatus(int ID)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.ChangeBillStatusPath, new object[] { ID });

                    var dataString = await client.GetStringAsync(convertString);

                    var result = JsonConvert.DeserializeObject<bool>(dataString);

                    return result;
                }
                catch (Exception e)
                {
                    return false;
                    throw e;
                }
            }
        }

        public async Task<bool> SendOrderConfirm(Bill bill, bool IsCheckOut, List<BillDetail> billDetailList, Customer customer)
        {
            //using (HttpClient client = new HttpClient())
            //{
            //    try
            //    {
            //        var convertString = Const.ConverToPathWithParameter(Const.SendOrderConfirmMailPath, new object[] { ID });

            //        var dataString = await client.GetStringAsync(convertString);

            //        var result = JsonConvert.DeserializeObject<bool>(dataString);

            //        return result;
            //    }
            //    catch (Exception e)
            //    {
            //        return false;
            //        throw e;
            //    }
            //}

            try
            {
            //    var bill = db.Bills.SingleOrDefault(b => b.ID == ID);
            //    var billDTO = new BillDTO(bill);
            //    var billDetailList = (await db.BillDetails
            //.ToListAsync())
            //.Select(b => new BillDetailDTO(b))
            //.ToList();
            //    billDetailList = billDetailList.FindAll(b => b.BillID == ID);
            //    var customer = db.Customers.SingleOrDefault(c => c.ID == billDTO.CustomerID);
                string billStatus = "";
                if (IsCheckOut)
                {
                    billStatus += "Đã thanh toán";
                }
                else
                {
                    billStatus += "Chưa thanh toán";
                }


                var body = "";

                body += "<hr/>";
                body += "Xin chào <b>" + customer.DisplayName + "</b>,<br/><br/>";

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
