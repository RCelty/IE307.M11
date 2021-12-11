using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using PhoneStoreApp.Models;

namespace PhoneStoreApp.Assets.Contains
{
    public class Const
    {
        public static int CurrentCustomerID = -1;
        public static List<Product> cartProducts = new List<Product>();

        public static readonly string SQLiteDBContextPath = "SQLite.db";
        public static readonly string Domain = $"http://10.0.2.2:88/";

        public static readonly string GetAllCategoryPath = Domain + @"Api/CategoryController/GetAllCategory";
        public static readonly string GetAllAdvertisementPath = Domain + @"Api/AdvertisementController/GetAllAdvertisement";
        public static readonly string GetAllBrandPath = Domain + @"Api/BrandController/GetAllBrand";
        public static readonly string GetAllProductPath = Domain + @"Api/ProductController/GetAllProduct";
        public static readonly string IncreaseViewCountPath = Domain + @"Api/ProductController/IncreaseViewCount/{ID}";

        public static readonly string LoginPath = Domain + @"Api/CustomerController/Login";
        public static readonly string GetCustomerByIDPath = Domain + @"Api/CustomerController/GetCustomerByID/{ID}";
        public static readonly string GetCustomerByUserNamePath = Domain + @"Api/CustomerController/GetCustomerByUserName/{UserName}";
        public static readonly string CustomerUploadImagePath = Domain + @"Api/CustomerController/UploadImage";

        public static readonly string IsRegisterAblePath = Domain + @"Api/CustomerController/IsRegisterAble";
        public static readonly string RegisterPath = Domain + @"Api/CustomerController/Register";
        public static readonly string SendOTPPath = Domain + @"Api/CustomerController/SendOTP";
        public static readonly string UpdateCustomerPath = Domain + @"Api/CustomerController/UpdateCustomer";

        public static readonly string GetFavoriteProductByCustomerIDPath = Domain + @"Api/CustomerController/GetFavoriteProductByCustomerID/{ID}";
        public static readonly string AddFavoriteProductPath = Domain + @"Api/CustomerController/AddFavoriteProduct";
        public static readonly string DeleteFavoriteProductPath = Domain + @"Api/CustomerController/DeleteFavoriteProduct";

        public static readonly string GetCommentByProductIDPath = Domain + @"Api/CommentController/GetCommentByProductID/{ID}";
        public static readonly string AddCommentPath = Domain + @"Api/CommentController/AddComment";
        public static readonly string UpdateCommentPath = Domain + @"Api/CommentController/UpdateComment";
        public static readonly string DeleteCommentPath = Domain + @"Api/CommentController/DeleteComment/{ID}";

        public static readonly string CreateBillPath = Domain + @"Api/BillController/CreateBill";
        public static readonly string GetAllBillByCustomerIDPath = Domain + @"Api/BillController/GetAllBillByCustomerID/{ID}";

        public static readonly string GetAllBillDetailByBillIDPath = Domain + @"Api/BillController/GetAllBillDetailByBillID/{ID}";
        public static readonly string AddBillDetailPath = Domain + @"Api/BillController/AddBillDetail";
        public static readonly string DeleteBillDetailByIDPath = Domain + @"Api/BillController/DeleteBillDetailByID/{ID}";
        public static readonly string ChangeBillStatusPath = Domain + @"Api/BillController/ChangeBillStatus/{ID}";

        public static readonly string Email = "ndo227587@gmail.com";
        public static readonly string Password = "766583Ha";


        public static string ConverToPathWithParameter(string path, object[] param = null)
        {
            if (param == null)
                return path;

            foreach (var item in param)
            {
                var startIndex = path.IndexOf("{");
                var endIndex = path.IndexOf("}");
                var oldString = path.Substring(startIndex, endIndex - startIndex + 1);
                path = path.Replace(oldString, item.ToString());
            }
            return path;
        }

        public static string ConvertToUnsign(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

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

        //public static bool SendMail(string to, string subject, string body)
        //{
        //    try
        //    {
        //        MailMessage message = new MailMessage();
        //        SmtpClient smtp = new SmtpClient();

        //        message.From = new MailAddress(Const.Email);
        //        message.To.Add(new MailAddress(to));
        //        message.Subject = subject;
        //        message.IsBodyHtml = true; //to make message body as html  
        //        message.Body = body;

        //        smtp.Port = 587;
        //        smtp.Host = "smtp.gmail.com"; //for gmail host  
        //        smtp.EnableSsl = true;
        //        smtp.UseDefaultCredentials = false;
        //        smtp.Credentials = new NetworkCredential(Const.Email, Const.Password);
        //        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
               
        //        smtp.Send(message);                
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //        throw e;
        //    }
        //}        
    }
}
