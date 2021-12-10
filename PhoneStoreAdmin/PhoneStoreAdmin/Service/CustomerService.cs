using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PhoneStoreAdmin.Assets.Contain;
using PhoneStoreAdmin.Model;

namespace PhoneStoreAdmin.Service
{
    public class CustomerService
    {
        private static CustomerService instance;

        public static CustomerService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CustomerService();
                }
                return instance;
            }
            private set => instance = value;
        }

        public async Task<List<Customer>> GetAllCustomerAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.GetAllCustomerPath);
                    var dataString = await client.GetStringAsync(convertString);

                    var resultList = JsonConvert.DeserializeObject<List<Customer>>(dataString);

                    return resultList;
                }
                catch (Exception e)
                {
                    return null;
                    throw e;
                }
            }
        }

        public async Task<bool> ChangeCustomerRole(int ID)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.ChangeCustomerRolePath, new object[] { ID });

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
