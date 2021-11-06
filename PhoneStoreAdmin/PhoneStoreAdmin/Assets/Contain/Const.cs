using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreAdmin.Assets.Contain
{
    public class Const
    {
        public static readonly string SQLiteDBContextPath = "SQLite.db";
        public static readonly string Domain = $"http://localhost:88/";

        public static readonly string GetAllCategoryPath = Domain + @"Api/CategoryController/GetAllCategory";
        public static readonly string GetAllAdvertisementPath = Domain + @"Api/AdvertisementController/GetAllAdvertisement";
        public static readonly string GetAllBrandPath = Domain + @"Api/BrandController/GetAllBrand";
        public static readonly string GetAllProductPath = Domain + @"Api/ProductController/GetAllProduct";
        public static readonly string AddCategoryPath = Domain + @"Api/CategoryController/AddCategory";
        public static readonly string UpdateCategoryPath = Domain + @"Api/CategoryController/UpdateCategory";
        public static readonly string DeleteCategoryPath = Domain + @"Api/CategoryController/DeleteCategory";

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
