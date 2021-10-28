using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Assets.Contain;
using API.Models.EF;

namespace API.Models.DTO
{
    public class AdvertisementDTO
    {
        public string Image { get; set; }

        public AdvertisementDTO()
        {

        }

        public AdvertisementDTO(Advertisement advertisement)
        {
            Image = Const.AdvertisementImagePath + advertisement.Image;
        }
    }
}