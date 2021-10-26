using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneStoreApp.Assets.Contains
{
    public class Const
    {
        public static readonly string SQLiteDBContextPath = "SQLite.db";
        public static readonly string Domain = $"http://192.168.1.5:88/";

        public static readonly string GetAllCategoryPath = Domain + @"Api/CategoryController/GetAllCategory";

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
