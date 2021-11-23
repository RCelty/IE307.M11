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
                    throw e;
                }
            }
        }

        public async Task<HttpResponseMessage> UploadImage(byte[] ImageData, string ImageName)
        {
            using (HttpClient client = new HttpClient())
            {
                var convertString = Const.ConverToPathWithParameter(Const.UploadBrandImagePath);
                var requestContent = new MultipartFormDataContent();
                //    here you can specify boundary if you need---^
                var imageContent = new ByteArrayContent(ImageData);
                imageContent.Headers.ContentType =
                    MediaTypeHeaderValue.Parse("image/jpeg");

                requestContent.Add(imageContent, "image", ImageName);

                return await client.PostAsync(convertString, requestContent);
            }
        }

        public async Task<int> AddBrand(Brand brand)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.AddBrandPath);

                    var myContent = JsonConvert.SerializeObject(brand);
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

        public async Task<bool> UpdateBrand(Brand brand)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.UpdateBrandPath);

                    var myContent = JsonConvert.SerializeObject(brand);
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

        public async Task<bool> DeleteBrand(int ID)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.DeleteBrandPath, new object[] { ID});

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
