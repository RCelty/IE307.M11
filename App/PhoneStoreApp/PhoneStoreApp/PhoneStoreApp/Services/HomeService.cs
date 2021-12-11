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
                    throw e;
                }
            }
        }

        public async Task<List<Brand>> GetAllBrandyAsync()
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
                    throw e;
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
                    throw e;
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

                    //ProductList.Sort((p1, p2) => p2.DiscountPrice.CompareTo(p1.DiscountPrice));

                    return ProductList;
                }
                catch (Exception e)
                {
                    return null;
                    throw e;
                }
            }
        }

        public async Task<List<Product>> GetTopDiscountProduct()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.GetAllProductPath);
                    var dataString = await client.GetStringAsync(convertString);

                    var ProductList = JsonConvert.DeserializeObject<List<Product>>(dataString);

                    ProductList.Sort((p1, p2) => p2.DiscountPercent.CompareTo(p1.DiscountPercent));

                    ProductList = ProductList.FindAll(p => p.DiscountPercent > 0);

                    return ProductList;
                }
                catch (Exception e)
                {
                    return null;
                    throw e;
                }
            }
        }

        public async Task<List<Product>> GetTopSellProduct()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.GetAllProductPath);
                    var dataString = await client.GetStringAsync(convertString);

                    var ProductList = JsonConvert.DeserializeObject<List<Product>>(dataString);

                    ProductList.Sort((p1, p2) => p2.SellCount.CompareTo(p1.SellCount));                    

                    return ProductList;
                }
                catch (Exception e)
                {
                    return null;
                    throw e;
                }
            }
        }

        public async Task<List<Product>> GetTopRateProduct()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.GetAllProductPath);
                    var dataString = await client.GetStringAsync(convertString);

                    var ProductList = JsonConvert.DeserializeObject<List<Product>>(dataString);

                    ProductList.Sort((p1, p2) => p2.Rating.CompareTo(p1.Rating));

                    return ProductList;
                }
                catch (Exception e)
                {
                    return null;
                    throw e;
                }
            }
        }

        public async Task<List<Product>> GetTopViewProduct()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.GetAllProductPath);
                    var dataString = await client.GetStringAsync(convertString);

                    var ProductList = JsonConvert.DeserializeObject<List<Product>>(dataString);

                    ProductList.Sort((p1, p2) => p2.ViewCount.CompareTo((p1.ViewCount)));

                    return ProductList;
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
