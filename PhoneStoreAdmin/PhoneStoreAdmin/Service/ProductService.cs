using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PhoneStoreAdmin.Assets.Contain;
using PhoneStoreAdmin.Model;

namespace PhoneStoreAdmin.Service
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

        public async Task<List<Product>> GetAllProductAsync()
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
                    throw e;
                }
            }
        }

        public async Task<int> AddProduct(Product product)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.AddProductPath);

                    var myContent = JsonConvert.SerializeObject(product);
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

        public async Task<HttpResponseMessage> UploadImage(byte[] ImageData, string ImageName)
        {
            if (ImageData != null && !string.IsNullOrEmpty(ImageName))
            {
                using (HttpClient client = new HttpClient())
                {
                    var convertString = Const.ConverToPathWithParameter(Const.UploadProductImagePath);
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

        public async Task<bool> UpdateProduct(Product product)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.UpdateProductPath);

                    var myContent = JsonConvert.SerializeObject(product);
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

        public async Task<bool> DeleteProduct(int ID)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.DeleteProductPath, new object[] { ID });

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

        public async Task<int> AddProductDetail(ProductDetail productDetail)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.AddProductDetailPath);

                    var myContent = JsonConvert.SerializeObject(productDetail);
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

        public async Task<bool> DeleteProductDetailByProductID(int ID)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.DeleteProductDetailPath, new object[] { ID });

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

        public async Task<List<ProductDetail>> GetAllProductDetailAsync(int ID)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.GetProductDetailByProductIDPath, new object[] { ID });
                    var dataString = await client.GetStringAsync(convertString);

                    var DetailList = JsonConvert.DeserializeObject<List<ProductDetail>>(dataString);

                    return DetailList;
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
