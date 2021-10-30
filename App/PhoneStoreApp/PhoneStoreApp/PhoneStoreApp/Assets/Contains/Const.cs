using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace PhoneStoreApp.Assets.Contains
{
    public class Const
    {        
        public static readonly string SQLiteDBContextPath = "SQLite.db";
        public static readonly string Domain = $"http://10.0.2.2:88/";

        public static readonly string GetAllCategoryPath = Domain + @"Api/CategoryController/GetAllCategory";
        public static readonly string GetAllAdvertisementPath = Domain + @"Api/AdvertisementController/GetAllAdvertisement";

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
    }
}
