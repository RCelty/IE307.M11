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
    public class BrandService
    {
        private static BrandService instance;

        public static BrandService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BrandService();
                }
                return instance;
            }
            private set => instance = value;
        }

        public async Task<List<Brand>> GetAllBrandAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.GetAllBrandPath);
                    var dataString = await client.GetStringAsync(convertString);

                    var BrandList = JsonConvert.DeserializeObject<List<Brand>>(dataString);

                    return BrandList;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }
    }
}
