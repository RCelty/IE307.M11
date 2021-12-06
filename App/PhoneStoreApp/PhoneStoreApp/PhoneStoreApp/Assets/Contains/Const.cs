using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using PhoneStoreApp.Models;

namespace PhoneStoreApp.Assets.Contains
{
    public class Const
    {
        public static int CurrentCustomerID = 1;
        public static List<Product> cartProducts = new List<Product>();

        public static readonly string SQLiteDBContextPath = "SQLite.db";
        public static readonly string Domain = $"http://10.0.2.2:88/";

        public static readonly string GetAllCategoryPath = Domain + @"Api/CategoryController/GetAllCategory";
        public static readonly string GetAllAdvertisementPath = Domain + @"Api/AdvertisementController/GetAllAdvertisement";
        public static readonly string GetAllBrandPath = Domain + @"Api/BrandController/GetAllBrand";
        public static readonly string GetAllProductPath = Domain + @"Api/ProductController/GetAllProduct";

        public static readonly string LoginPath = Domain + @"Api/CustomerController/Login";
        public static readonly string GetCustomerByIDPath = Domain + @"Api/CustomerController/GetCustomerByID/{ID}";
        public static readonly string CustomerUploadImagePath = Domain + @"Api/CustomerController/UploadImage";

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
    }
}
