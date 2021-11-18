using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneStoreApp.Models.SubModels
{
    public class FavoriteProductItem
    {
        public int ID { get; set; }

        public string DisplayName { get; set; }

        public string Image1 { get; set; }

        public int? Price { get; set; }

        public int? DiscountPercent { get; set; }

        public int? DiscountPrice { get; set; }

        public double? Rating { get; set; }

        public int? CommentCount { get; set; }

        public bool IsSelected { get; set; }
    }
}
