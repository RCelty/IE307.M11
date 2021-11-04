using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Assets.Contain
{
    public class Const
    {
        public static readonly string Domain = $"http://10.0.2.2:88/";
        public static readonly string AdvertisementImagePath = Domain + @"Assets/Images/Advertisement/";
        public static readonly string BrandImagePath = Domain + @"Assets/Images/Brand/";
        public static readonly string ProductImagePath = Domain + @"Assets/Images/Product/";
    }
}