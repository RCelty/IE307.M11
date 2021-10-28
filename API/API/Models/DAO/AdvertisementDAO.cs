using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using API.Models.DTO;
using API.Models.EF;

namespace API.Models.DAO
{
    public class AdvertisementDAO
    {
        private static AdvertisementDAO instance;

        public static AdvertisementDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AdvertisementDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        PhoneStoreEntities db = new PhoneStoreEntities();

        public async Task<List<AdvertisementDTO>> GetAllAdvertisement()
        {
            return (await db.Advertisements
                        .ToListAsync())
                        .Select(advertisement => new AdvertisementDTO(advertisement))
                        .ToList();
        }
    }
}
