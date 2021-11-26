using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PhoneStoreApp.Assets.Contains;
using PhoneStoreApp.Models;

namespace PhoneStoreApp.Services
{
    public class CommentService
    {
        private static CommentService instance;

        public static CommentService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CommentService();
                }
                return instance;
            }
            private set => instance = value;
        }

        public async Task<List<Comment>> GetCommentByProductID(int ID)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dataString = await client.GetStringAsync(Const.ConverToPathWithParameter(Const.GetCommentByProductIDPath, new object[] { ID }));

                    var resultList = JsonConvert.DeserializeObject<List<Comment>>(dataString);

                    return resultList;
                }
                catch (Exception e)
                {
                    return null;
                    throw e;
                }
            }
        }

        public async Task<int> AddComment(Comment comment)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.AddCommentPath);

                    var myContent = JsonConvert.SerializeObject(comment);
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

        public async Task<bool> UpdateComment(Comment comment)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.UpdateCommentPath);

                    var myContent = JsonConvert.SerializeObject(comment);
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

        public async Task<bool> DeleteComment(int ID)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var convertString = Const.ConverToPathWithParameter(Const.DeleteCommentPath, new object[] { ID });

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
