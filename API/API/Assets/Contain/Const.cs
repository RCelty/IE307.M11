using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Web.Administration;

namespace API.Assets.Contain
{
    public class Const
    {
        public static readonly string Domain = $"http://10.0.2.2:88/";
        //public static readonly string Domain = $"http://chotechAPI.somee.com/";
        
        public static readonly string AdvertisementImagePath = Domain + @"Assets/Images/Advertisement/";
        public static readonly string BrandImagePath = Domain + @"Assets/Images/Brand/";
        public static readonly string ProductImagePath = Domain + @"Assets/Images/Product/";
        public static readonly string CustomerImagePath = Domain + @"Assets/Images/Customer/";

        public static readonly string Email = "ndo227587@gmail.com";
        public static readonly string Password = "766583Ha";


        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static async Task<bool> VerifyEmail(string email)
        {
            Task<bool> task = new Task<bool>(new Func<bool>(() =>
            {
                TcpClient tClient = new TcpClient("gmail-smtp-in.l.google.com", 25);
                string CRLF = "\r\n";
                byte[] dataBuffer;
                string ResponseString;
                NetworkStream netStream = tClient.GetStream();
                StreamReader reader = new StreamReader(netStream);
                ResponseString = reader.ReadLine();
                /* Perform HELO to SMTP Server and get Response */
                dataBuffer = BytesFromString("HELO KirtanHere" + CRLF);
                netStream.Write(dataBuffer, 0, dataBuffer.Length);
                ResponseString = reader.ReadLine();
                dataBuffer = BytesFromString("MAIL FROM:<YourGmailIDHere@gmail.com>" + CRLF);
                netStream.Write(dataBuffer, 0, dataBuffer.Length);
                ResponseString = reader.ReadLine();
                /* Read Response of the RCPT TO Message to know from google if it exist or not */
                dataBuffer = BytesFromString("RCPT TO:<" + email + ">" + CRLF);
                netStream.Write(dataBuffer, 0, dataBuffer.Length);
                ResponseString = reader.ReadLine();
                /* QUITE CONNECTION */
                dataBuffer = BytesFromString("QUITE" + CRLF);
                netStream.Write(dataBuffer, 0, dataBuffer.Length);
                tClient.Close();

                return ResponseString.Contains("OK");
            }));

            task.Start();

            return await task;
        }

        public static byte[] BytesFromString(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        public static int GetResponseCode(string ResponseString)
        {
            return int.Parse(ResponseString.Substring(0, 3));
        }

        public static bool SendMail(string to, string subject, string body)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                message.From = new MailAddress(Const.Email);
                message.To.Add(new MailAddress(to));
                message.Subject = subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = body;

                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(Const.Email, Const.Password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                smtp.Send(message);
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

        public static void RecycleAppPools()
        {
            ServerManager serverManager = new ServerManager();
            ApplicationPoolCollection appPools = serverManager.ApplicationPools;
            foreach (ApplicationPool ap in appPools)
            {
                ap.Recycle();
            }
        }

        public static void RecycleAppPool()
        {
            ServerManager serverManager = new ServerManager();
            string appPoolName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            ApplicationPool appPool = serverManager.ApplicationPools[appPoolName];
            if (appPool != null)
            {
                if (appPool.State == ObjectState.Stopped)
                {
                    appPool.Start();
                }
                else
                {
                    appPool.Recycle();
                }
            }
        }
    }
}