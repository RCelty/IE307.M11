using Newtonsoft.Json;
using PhoneStoreApp.Models.SubModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreApp.Services
{
    public class MomoService
    {
        private static MomoService instance;

        public static MomoService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MomoService();
                }
                return instance;
            }
            private set => instance = value;
        }
        public async Task<string> sendPaymentRequest(string endpoint, string postJsonString)
        {

            try
            {
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(endpoint);

                var postData = postJsonString;

                var data = Encoding.UTF8.GetBytes(postData);

                httpWReq.ProtocolVersion = HttpVersion.Version11;
                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/json";

                httpWReq.ContentLength = data.Length;
                httpWReq.ReadWriteTimeout = 30000;
                httpWReq.Timeout = 15000;
                Stream stream = httpWReq.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();

                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();

                string jsonresponse = "";

                using (var reader = new StreamReader(response.GetResponseStream()))
                {

                    string temp = null;
                    while ((temp = reader.ReadLine()) != null)
                    {
                        jsonresponse += temp;
                    }
                }


                //todo parse it
                return jsonresponse;

                //todo parse it
                //return new MomoResponse(mtid, jsonresponse);

            }
            catch (WebException e)
            {
                return null;
                throw e;
            }
        }
    }

}
