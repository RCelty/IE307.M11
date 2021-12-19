using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PhoneStoreApp.Assets.Contains;
using PhoneStoreApp.Models;

namespace PhoneStoreApp.Services
{
    public class LoginServices
    {
        private static LoginServices instance;

        public static LoginServices Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LoginServices();
                }
                return instance;
            }
            private set => instance = value;
        }

        public async Task<Customer> LoginAsync(string userName, string passWord)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.LoginPath);

                    Customer customer = new Customer() { UserName = userName, PassWord = passWord };

                    var myContent = JsonConvert.SerializeObject(customer);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var result = client.PostAsync(convertString, byteContent).Result.Content.ReadAsStringAsync().Result;

                    var resultCustomer = JsonConvert.DeserializeObject<Customer>(result);

                    return resultCustomer;
                }
                catch (Exception e)
                {
                    return null;
                    throw e;
                }
            }
        }

        public async Task<Customer> GetCustomerByID(int ID)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dataString = await client.GetStringAsync(Const.ConverToPathWithParameter(Const.GetCustomerByIDPath, new object[] { ID }));

                    var customer = JsonConvert.DeserializeObject<Customer>(dataString);

                    return customer;
                }
                catch (Exception e)
                {
                    return null;
                    throw e;
                }
            }
        }

        public async Task<Customer> GetCustomerUserNameID(string userName)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dataString = await client.GetStringAsync(Const.ConverToPathWithParameter(Const.GetCustomerByUserNamePath, new object[] { userName }));

                    var customer = JsonConvert.DeserializeObject<Customer>(dataString);

                    return customer;
                }
                catch (Exception e)
                {
                    return null;
                    throw e;
                }
            }
        }

        public async Task<HttpResponseMessage> UploadImage(byte[] ImageData, string ImageName)
        {
            if (ImageData != null && !string.IsNullOrEmpty(ImageName))
            {
                using (HttpClient client = new HttpClient())
                {
                    var convertString = Const.ConverToPathWithParameter(Const.CustomerUploadImagePath);
                    var requestContent = new MultipartFormDataContent();
                    //    here you can specify boundary if you need---^
                    var imageContent = new ByteArrayContent(ImageData);
                    imageContent.Headers.ContentType =
                        MediaTypeHeaderValue.Parse("image/jpeg");

                    requestContent.Add(imageContent, "image", ImageName);

                    return await client.PostAsync(convertString, requestContent);
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<int> IsRegisterAlbe(Customer customer)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.IsRegisterAblePath);

                    var myContent = JsonConvert.SerializeObject(customer);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var result = client.PostAsync(convertString, byteContent).Result.Content.ReadAsStringAsync().Result;

                    var resultInt = JsonConvert.DeserializeObject<int>(result);

                    return resultInt;
                }
                catch (Exception e)
                {
                    return -3;
                    throw e;
                }
            }
        }

        public async Task<int> Register(Customer customer)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.RegisterPath);

                    var myContent = JsonConvert.SerializeObject(customer);
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

        public async Task<string> SendOTP(Customer customer)
        {
            //using (HttpClient client = new HttpClient())
            //{
            //    try
            //    {
            //        var convertString = Const.ConverToPathWithParameter(Const.SendOTPPath);

            //        var myContent = JsonConvert.SerializeObject(customer);
            //        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            //        var byteContent = new ByteArrayContent(buffer);

            //        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //        var result = client.PostAsync(convertString, byteContent).Result.Content.ReadAsStringAsync().Result;

            //        var OTP = JsonConvert.DeserializeObject<string>(result);

            //        return OTP;
            //    }
            //    catch (Exception e)
            //    {
            //        return "";
            //        throw e;
            //    }
            //}
            Task<string> task = new Task<string>(new Func<string>(() =>
            {
                try
                {
                    string OTP = new Random().Next(1000, 10000).ToString();
                    var body = "";

                    body += "<hr/>";
                    body += "Xin chào <b>" + customer.DisplayName + "</b>,<br/><br/>";

                    body += "Mã xác thực OTP Chotech của bạn là: <b>" + OTP + "</b><br/><br/>";
                    body += "Vui lòng không cung cấp mã OTP cho bất kì ai khác.<br/><br/>";
                    body += "Cảm ơn đã đăng ký,<br/><br/>";
                    body += "-----------------------------<br/>";
                    body += "<b>D.A Nguyen - Leader</b><br/>";
                    body += "<b>Phone:</b> (84) 00 7143 619<br/>";
                    body += "<b>Facebook: </b><a href=" + "https://www.facebook.com/duyanh.nguyenngoc.14/" + ">https://www.facebook.com/duyanh.nguyenngoc.14/<a/>";
                    body += "<hr/>";

                    var result = Const.SendMail(customer.Email, "Xác thực Email - ChoTech", body);
                    if (result)
                        return OTP;
                    else
                        return "";

                }
                catch (Exception e)
                {
                    return "";
                    throw e;
                }
            }));

            task.Start();

            return await task;
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.UpdateCustomerPath);

                    var myContent = JsonConvert.SerializeObject(customer);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var result = client.PostAsync(convertString, byteContent).Result.Content.ReadAsStringAsync().Result;

                    var resultBool = JsonConvert.DeserializeObject<bool>(result);

                    return resultBool;
                }
                catch (Exception e)
                {
                    return false;
                    throw e;
                }
            }
        }


        public async Task<string> ConfirmOTPEmail(string fullName, string email)
        {
            Task<string> task = new Task<string>(new Func<string>(() =>
            {
                try
                {
                    string OTP = new Random().Next(1000, 10000).ToString();
                    var body = "";

                    body += "<hr/>";
                    body += "Xin chào <b>" + fullName + "</b>,<br/><br/>";

                    body += "Mã xác thực OTP Chotech của bạn là: <b>" + OTP + "</b><br/><br/>";

                    Const.SendMail(email, "Confirm Password - Electronic Shop", body);
                    return OTP;

                }
                catch (Exception e)
                {
                    return "";
                    throw e;
                }
            }));

            task.Start();

            return await task;
        }
    }
}
