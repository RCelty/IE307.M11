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
                }
            }
        }

        public async Task<HttpResponseMessage> UploadImage(byte[] ImageData, string ImageName)
        {
            using (HttpClient client = new HttpClient())
            {
                var convertString = Const.ConverToPathWithParameter(Const.UploadImagePath);
                var requestContent = new MultipartFormDataContent();
                //    here you can specify boundary if you need---^
                var imageContent = new ByteArrayContent(ImageData);
                imageContent.Headers.ContentType =
                    MediaTypeHeaderValue.Parse("image/jpeg");

                requestContent.Add(imageContent, "image", ImageName);

                return await client.PostAsync(convertString, requestContent);
            }
        }
    }
}
