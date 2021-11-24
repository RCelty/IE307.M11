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
    public class ProductDAO
    {
        private static ProductDAO instance;

        public static ProductDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        PhoneStoreEntities1 db = new PhoneStoreEntities1();

        public async Task<List<ProductDTO>> GetAllProduct()
        {
            var ProductList = (await db.Products
                        .ToListAsync())
                        .Select(product => new ProductDTO(product))
                        .ToList();
            ProductList = ProductList.FindAll(b => b.IsDeleted == false);
            return ProductList;
        }

        public async Task<int> AddProduct(ProductDTO productDTO)
        {
            var product = new Product()
            {
                DisplayName = productDTO.DisplayName,
                Price = productDTO.Price,
                DiscountPercent = productDTO.DiscountPercent,
                Rating = 0,
                ViewCount = 0,
                CommentCount = 0,
                SellCount = 0,
                Description = productDTO.Description,
                Image1 = productDTO.Image1,
                Image2 = productDTO.Image2,
                Image3 = productDTO.Image3,
                Image4 = productDTO.Image4,
                CategoryID = productDTO.CategoryID,
                BrandID = productDTO.BrandID,
                IsDeleted = false
            };

            try
            {
                db.Products.Add(product);
                await db.SaveChangesAsync();
                return product.ID;
            }
            catch (Exception e)
            {
                return -1;
                throw e;
            }
        }

        public async Task<bool> UpdateProduct(ProductDTO productDTO)
        {
            var result = db.Products.SingleOrDefault(p => p.ID == productDTO.ID);
            try
            {
                result.DisplayName = productDTO.DisplayName;
                result.Price = productDTO.Price;
                result.DiscountPercent = productDTO.DiscountPercent;
                result.Description = productDTO.Description;               
                result.Image1 = productDTO.Image1;
                result.Image2 = productDTO.Image2;
                result.Image3 = productDTO.Image3;
                result.Image4 = productDTO.Image4;
                result.CategoryID = productDTO.CategoryID;
                result.BrandID = productDTO.BrandID;

                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

        public async Task<bool> DeleteProduct(int ID)
        {
            var result = db.Products.SingleOrDefault(b => b.ID == ID);

            try
            {
                result.IsDeleted = true;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

        public async Task<bool> RestoreAllProduct()
        {
            var BrandList = (await db.Products
                        .ToListAsync())
                        .Select(product => new ProductDTO(product))
                        .ToList();

            try
            {
                var DeletedList = db.Products.Where(b => b.IsDeleted == true).ToList();
                DeletedList.ForEach(b => b.IsDeleted = false);
                db.SaveChanges();

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