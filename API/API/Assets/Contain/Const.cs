using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace API.Assets.Contain
{
    public class Const
    {
        public static readonly string Domain = $"http://10.0.2.2:88/";
        
        public static readonly string AdvertisementImagePath = Domain + @"Assets/Images/Advertisement/";       
        public static readonly string BrandImagePath = Domain + @"Assets/Images/Brand/";       
        public static readonly string ProductImagePath = Domain + @"Assets/Images/Product/";
        public static readonly string CustomerImagePath = Domain + @"Assets/Images/Customer/";

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
    }
}