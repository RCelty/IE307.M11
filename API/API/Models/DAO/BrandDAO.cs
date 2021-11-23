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
            var BrandList = (await db.Brands                        
                        .ToListAsync())
                        .Select(brand => new BrandDTO(brand))
                        .ToList();
            BrandList = BrandList.FindAll(b => b.IsDeleted == false);
            return BrandList;
        }

        public async Task<int> AddBrand(BrandDTO brandDTO)
        {
            var brand = new Brand()
            {
                DisplayName = brandDTO.DisplayName,
                Image = brandDTO.Image,
                IsDeleted = false
            };

            try
            {
                db.Brands.Add(brand);
                await db.SaveChangesAsync();
                return brand.ID;
            }
            catch(Exception e)
            {
                return -1;
                throw e;
            }            
        }

        public async Task<bool> UpdateBrand(BrandDTO brand)
        {
            var result = db.Brands.SingleOrDefault(b => b.ID == brand.ID);
            try
            {
                result.DisplayName = brand.DisplayName;
                result.Image = brand.Image;
                await db.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                return false;
                throw e;
            }
        }

        public async Task<bool> DeleteBrand(int ID)
        {
            var result = db.Brands.SingleOrDefault(b => b.ID == ID);

            try
            {
                result.IsDeleted = true;
                await db.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {                
                return false;
                throw e;
            }
        }

        public async Task<bool> RestoreAllBrand()
        {
            var BrandList = (await db.Brands
                        .ToListAsync())
                        .Select(brand => new BrandDTO(brand))
                        .ToList();

            try
            {
                var DeletedList = db.Brands.Where(b => b.IsDeleted == true).ToList();
                DeletedList.ForEach(b => b.IsDeleted = false);
                db.SaveChanges();

                return true;
            }
            catch(Exception e)
            {
                return false;
                throw e;
            }
        }
    }
}