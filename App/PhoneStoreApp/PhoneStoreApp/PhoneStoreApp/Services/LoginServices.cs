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
    }
}
