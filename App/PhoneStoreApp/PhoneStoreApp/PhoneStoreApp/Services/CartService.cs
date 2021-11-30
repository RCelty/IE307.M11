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
                    var convertString = Const.ConverToPathWithParameter(Const.CreateBillPath);

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
    }
}
