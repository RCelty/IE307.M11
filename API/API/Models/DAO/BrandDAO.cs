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
    public class BrandDAO
    {
        private static BrandDAO instance;

        public static BrandDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BrandDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        PhoneStoreEntities1 db = new PhoneStoreEntities1();

        public async Task<List<BrandDTO>> GetAllBrand()
        {
            return (await db.Brands
                        .ToListAsync())
                        .Select(brand => new BrandDTO(brand))
                        .ToList();
        }
    }
}