using System;
using System.Collections.Generic;
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
