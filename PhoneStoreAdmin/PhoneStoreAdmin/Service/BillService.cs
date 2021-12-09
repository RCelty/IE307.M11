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
    public class BillService
    {
        private static BillService instance;

        public static BillService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BillService();
                }
                return instance;
            }
            private set => instance = value;
        }

        public async Task<List<Bill>> GetAllBillAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.GetAllBillPath);
                    var dataString = await client.GetStringAsync(convertString);

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

        public async Task<List<BillDetail>> GetAllBillDetailByBillID(int ID)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.GetBillDetailByBillIDPath, new object[] { ID });
                    var dataString = await client.GetStringAsync(convertString);

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
    }
}
