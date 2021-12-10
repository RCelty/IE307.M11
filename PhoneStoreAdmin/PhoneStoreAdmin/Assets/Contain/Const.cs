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
        
        public static readonly string GetAllAdvertisementPath = Domain + @"Api/AdvertisementController/GetAllAdvertisement";
        
        public static readonly string GetAllProductPath = Domain + @"Api/ProductController/GetAllProduct";
        public static readonly string AddProductPath = Domain + @"Api/ProductController/AddProduct";
        public static readonly string UploadProductImagePath = Domain + @"Api/ProductController/UploadProductImage";
        public static readonly string UpdateProductPath = Domain + @"Api/ProductController/UpdateProduct";
        public static readonly string DeleteProductPath = Domain + @"Api/ProductController/DeleteProduct/{ID}";

        public static readonly string GetProductDetailByProductIDPath = Domain + @"Api/ProductController/GetAllProductDetailByProductID/{ID}";
        public static readonly string AddProductDetailPath = Domain + @"Api/ProductController/AddProductDetail";
        public static readonly string DeleteProductDetailPath = Domain + @"Api/ProductController/DeleteProductDetailByProductID/{ID}";

        public static readonly string GetAllCategoryPath = Domain + @"Api/CategoryController/GetAllCategory";
        public static readonly string AddCategoryPath = Domain + @"Api/CategoryController/AddCategory";
        public static readonly string UpdateCategoryPath = Domain + @"Api/CategoryController/UpdateCategory";
        public static readonly string DeleteCategoryPath = Domain + @"Api/CategoryController/DeleteCategory/{ID}";

        public static readonly string GetAllBrandPath = Domain + @"Api/BrandController/GetAllBrand";
        public static readonly string UploadBrandImagePath = Domain + @"Api/BrandController/UploadBrandImage";
        public static readonly string AddBrandPath = Domain + @"Api/BrandController/AddBrand";
        public static readonly string UpdateBrandPath = Domain + @"Api/BrandController/UpdateBrand";
        public static readonly string DeleteBrandPath = Domain + @"Api/BrandController/DeleteBrand/{ID}";

        public static readonly string GetAllCommentPath = Domain + @"Api/CommentController/GetAllComment";
        public static readonly string DeleteCommentPath = Domain + @"Api/CommentController/DeleteComment/{ID}";

        public static readonly string GetAllBillPath = Domain + @"Api/BillController/GetAllBill";
        public static readonly string GetBillDetailByBillIDPath = Domain + @"Api/BillController/GetAllBillDetailByBillID/{ID}";
        public static readonly string ChangeBillStatusPath = Domain + @"Api/BillController/ChangeBillStatus/{ID}";

        public static readonly string GetAllCustomerPath = Domain + @"Api/CustomerController/GetAllCustomer";
        public static readonly string ChangeCustomerRolePath = Domain + @"Api/CustomerController/ChangeCustomerRole/{ID}";

        public static readonly string LoginPath = Domain + @"Api/CustomerController/Login";

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
