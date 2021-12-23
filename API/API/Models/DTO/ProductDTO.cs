using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Assets.Contain;
using API.Models.EF;

namespace API.Models.DTO
{
    public class ProductDTO
    {
        public int? ID { get; set; }

        public string DisplayName { get; set; }

        public int? Price { get; set; }

        public int? DiscountPercent { get; set; }

        public int? DiscountPrice { get; set; }

        public double? Rating { get; set; }

        public int? ViewCount { get; set; }

        public int? CommentCount { get; set; }

        public int? SellCount { get; set; }

        public string Description { get; set; }

        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }

        public int? CategoryID { get; set; }

        public string CategoryName { get; set; }

        public int? BrandID { get; set; }

        public string BrandName { get; set; }

        public bool? IsDeleted { get; set; }

        public List<ProductDetailDTO> ProductDetails { get; set; }

        public List<CommentDTO> Comments { get; set; }

        public List<BillDetailDTO> BillDetails { get; set; }

        public ProductDTO()
        {

        }

        public ProductDTO(Product product)
        {
            ID = product.ID;
            DisplayName = product.DisplayName;
            Price = product.Price;
            DiscountPercent = product.DiscountPercent;
            //Rating = product.Rating;
            ViewCount = product.ViewCount;
            //CommentCount = product.CommentCount;
            CommentCount = product.Comments.Count();
            //SellCount = product.SellCount;
            Description = product.Description;
            Image1 = Const.ProductImagePath + product.Image1;
            Image2 = Const.ProductImagePath + product.Image2;
            Image3 = Const.ProductImagePath + product.Image3;
            Image4 = Const.ProductImagePath + product.Image4;
            CategoryID = product.CategoryID;
            CategoryName = product.Category.DisplayName;
            BrandID = product.BrandID;
            BrandName = product.Brand.DisplayName;
            DiscountPrice = product.Price - (int)Math.Ceiling((float)product.Price * (float)product.DiscountPercent / 100);
            ProductDetails = product.ProductDetails
                                        .Select(productDetail => new ProductDetailDTO(productDetail))
                                        .ToList();
            Comments = product.Comments.Select(c => new CommentDTO(c)).ToList();
            IsDeleted = product.IsDeleted;
            BillDetails = product.BillDetails.Select(b => new BillDetailDTO(b)).ToList();
            Rating = Comments.Count > 0 ? Math.Round((double)product.Comments.Average(c => c.Rating), 1) : 0;
            SellCount = BillDetails.Count > 0 ? product.BillDetails.Sum(b => b.TotalCount) : 0;
        }
    }
}