using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using PhoneStoreApp.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using PhoneStoreApp.Assets.Contains;

namespace PhoneStoreApp.Services
{
    public class HomeService
    {
        private static HomeService instance;

        public static HomeService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HomeService();
                }
                return instance;
            }
            private set => instance = value;
        }

        public async Task<List<Category>> GetAllCategoryAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.GetAllCategoryPath);
                    var dataString = await client.GetStringAsync(convertString);

                    var CategoryList = JsonConvert.DeserializeObject<List<Category>>(dataString);

                    return CategoryList;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public async Task<List<Advertisement>> GetAllAdvertisement()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.GetAllAdvertisementPath);
                    var dataString = await client.GetStringAsync(convertString);

                    var AdvertisementList = JsonConvert.DeserializeObject<List<Advertisement>>(dataString);

                    return AdvertisementList;
                }
                catch(Exception e)
                {
                    return null;
                }
            }
        }

        public async Task<List<Product>> GetAllProduct()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.GetAllProductPath);
                    var dataString = await client.GetStringAsync(convertString);

                    var ProductList = JsonConvert.DeserializeObject<List<Product>>(dataString);

                    return ProductList;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }       
    }
}
