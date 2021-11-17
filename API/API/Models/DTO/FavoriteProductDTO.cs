using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Models.EF;

namespace API.Models.DTO
{
    public class FavoriteProductDTO
    {
        public int? ID { get; set; }

        public int? ProductID { get; set; }

        public int? CustomerID { get; set; }

        public FavoriteProductDTO()
        {

        }

        public FavoriteProductDTO(FavoriteProduct favoriteProduct)
        {
            ID = favoriteProduct.ID;
            ProductID = favoriteProduct.ProductID;
            CustomerID = favoriteProduct.CustomerID;
        }
    }
}