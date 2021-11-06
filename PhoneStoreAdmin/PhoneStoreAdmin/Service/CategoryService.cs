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
    public class CategoryService
    {
        private static CategoryService instance;

        public static CategoryService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CategoryService();
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
        public async Task<bool> AddCategory(Category category)
        {
            using(HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.AddCategoryPath);

                    var myContent = JsonConvert.SerializeObject(category);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var result = client.PostAsync(convertString, byteContent).Result.Content.ReadAsStringAsync().Result;

                    return true;
                }
                catch(Exception e)
                {
                    return false;
                }
            }
        }

        public async Task<bool> UpdateCategory(Category category)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.UpdateCategoryPath);

                    var myContent = JsonConvert.SerializeObject(category);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var result = client.PostAsync(convertString, byteContent).Result.Content.ReadAsStringAsync().Result;

                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public async Task<bool> DeleteCategory(Category category)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.DeleteCategoryPath);

                    var myContent = JsonConvert.SerializeObject(category);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var result = client.PostAsync(convertString, byteContent).Result.Content.ReadAsStringAsync().Result;

                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
    }
}
