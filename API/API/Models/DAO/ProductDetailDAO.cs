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
    public class ProductDetailDAO
    {
        private static ProductDetailDAO instance;

        public static ProductDetailDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductDetailDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        PhoneStoreEntities1 db = new PhoneStoreEntities1();

        public async Task<bool> DeleteProductDetailByProductID(int productID)
        {
            try
            {
                var detailList = await db.ProductDetails.Where(p => p.ProductID == productID).ToListAsync();
                if (detailList != null && detailList.Count > 0)
                {
                    foreach (var d in detailList)
                    {
                        db.ProductDetails.Remove(d);
                        await db.SaveChangesAsync();
                    }
                    return true;
                }
                else return false;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

        public async Task<int> AddProductDetail(ProductDetailDTO productDetailDTO)
        {
            var productDetail = new ProductDetail()
            {
                ProductID = productDetailDTO.ProductID,
                DetailName = productDetailDTO.DetailName,
                DetailDescription = productDetailDTO.DetailDescription,
                IsDeleted = false,
            };

            try
            {
                db.ProductDetails.Add(productDetail);
                await db.SaveChangesAsync();
                return productDetail.ID;
            }
            catch (Exception e)
            {
                return -1;
                throw e;
            }
        }

        public async Task<List<ProductDetailDTO>> GetAllProductDetail()
        {
            var resultList = (await db.ProductDetails
                        .ToListAsync())
                        .Select(p => new ProductDetailDTO(p))
                        .ToList();
            resultList = resultList.FindAll(b => b.IsDeleted == false);
            return resultList;
        }

        public async Task<List<ProductDetailDTO>> GetAllProductDetailByProductID(int productID)
        {
            var resultList = (await db.ProductDetails
                        .ToListAsync())
                        .Select(p => new ProductDetailDTO(p))
                        .ToList();
            resultList = resultList.FindAll(b => b.IsDeleted == false && b.ProductID == productID);
            return resultList;
        }
    }
}