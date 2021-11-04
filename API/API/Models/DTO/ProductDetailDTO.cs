using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Models.EF;

namespace API.Models.DTO
{
    public class ProductDetailDTO
    {
        public int? ProductID { get; set; }

        public string DetailName { get; set; }

        public string DetailDescription { get; set; }

        public ProductDetailDTO()
        {

        }

        public ProductDetailDTO(ProductDetail productDetail)
        {
            ProductID = productDetail.ProductID;
            DetailName = productDetail.DetailName;
            DetailDescription = productDetail.DetailDescription;
        }
    }
}