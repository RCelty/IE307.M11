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
    public class FavoriteProductDAO
    {
        private static FavoriteProductDAO instance;

        public static FavoriteProductDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FavoriteProductDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        PhoneStoreEntities1 db = new PhoneStoreEntities1();

        public async Task<List<FavoriteProductDTO>> GetFavoriteProductByCustomerID(int ID)
        {
            var FavoriteProductList = (await db.FavoriteProducts
                .ToListAsync())
                .Select(favoriteproduct => new FavoriteProductDTO(favoriteproduct))
                .ToList();
            FavoriteProductList = FavoriteProductList.FindAll(f => f.CustomerID == ID);
            return FavoriteProductList;
        }

        public async Task<int> AddFavoriteProduct(FavoriteProductDTO favoriteProductDTO)
        {
            var favproduct = new FavoriteProduct()
            {
                ProductID = favoriteProductDTO.ProductID,
                CustomerID = favoriteProductDTO.CustomerID
            };
            try
            {
                var FavoriteProduct = await db.FavoriteProducts.SingleOrDefaultAsync(f => f.CustomerID == favproduct.CustomerID && f.ProductID == favproduct.ProductID);

                if (FavoriteProduct == null)
                {
                    db.FavoriteProducts.Add(favproduct);
                    await db.SaveChangesAsync();
                    return favproduct.ID;
                }
                else
                {
                    return -1;
                }                    
            }
            catch (Exception e)
            {
                return -1;
                throw e;
            }
        }

        public async Task<bool> DeleteFavoriteProduct(FavoriteProductDTO favoriteProductDTO)
        {
            try
            {
                var FavoriteProduct = await db.FavoriteProducts.SingleOrDefaultAsync(f => f.CustomerID == favoriteProductDTO.CustomerID && f.ProductID == favoriteProductDTO.ProductID);
                if (FavoriteProduct != null)
                {
                    db.FavoriteProducts.Remove(FavoriteProduct);
                    await db.SaveChangesAsync();
                    return true;
                }
                else return false;
            }
            catch(Exception e)
            {
                return false;
                throw e;
            }
        }

        public async Task<bool> DeleteFavoriteProductByID(int ID)
        {
            var result = db.FavoriteProducts.SingleOrDefault(f => f.ID == ID);

            try
            {
                db.FavoriteProducts.Remove(result);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }
    }
}