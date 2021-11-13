using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PhoneStoreApp.Assets.Contains;
using PhoneStoreApp.Models;

namespace PhoneStoreApp.Services
{
    public class ProductService
    {
        private static ProductService instance;

        public static ProductService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductService();
                }
                return instance;
            }
            private set => instance = value;
        }

        public async Task<List<Product>> GetProductByCategoryID(int categoryID)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.GetAllProductPath);
                    var dataString = await client.GetStringAsync(convertString);

                    var ProductList = JsonConvert.DeserializeObject<List<Product>>(dataString);

                    ProductList = ProductList.FindAll(p => p.CategoryID == categoryID);

                    return ProductList;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public async Task<List<Product>> GetProductBySearchText(string searchText)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.GetAllProductPath);
                    var dataString = await client.GetStringAsync(convertString);

                    var ProductList = JsonConvert.DeserializeObject<List<Product>>(dataString);

                    ProductList = ProductList.FindAll(p => p.DisplayName.IndexOf(searchText, 0, StringComparison.CurrentCultureIgnoreCase) != -1);

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
