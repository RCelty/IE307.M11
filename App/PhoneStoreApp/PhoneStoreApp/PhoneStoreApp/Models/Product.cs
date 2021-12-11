using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneStoreApp.Models
{
    public class Product
    {
        public int ID { get; set; }

        public string DisplayName { get; set; }

        public int? Price { get; set; }

        public int DiscountPercent { get; set; }

        public int DiscountPrice { get; set; }

        public double Rating { get; set; }

        public int ViewCount { get; set; }

        public int? CommentCount { get; set; }

        public int SellCount { get; set; }

        public string Description { get; set; }

        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }

        public int? CategoryID { get; set; }

        public string CategoryName { get; set; }

        public int? BrandID { get; set; }

        public string BrandName { get; set; }
        public List<ProductDetail> ProductDetails { get; set; }
    }
}
