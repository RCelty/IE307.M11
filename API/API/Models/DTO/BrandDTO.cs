using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Assets.Contain;
using API.Models.EF;

namespace API.Models.DTO
{
    public class BrandDTO
    {
        public int? ID { get; set; }

        public string DisplayName { get; set; }

        public string Image { get; set; }

        public BrandDTO()
        {

        }

        public BrandDTO(Brand brand)
        {
            DisplayName = brand.DisplayName;
            ID = brand.ID;
            Image = Const.BrandImagePath + brand.Image;
        }
    }
}