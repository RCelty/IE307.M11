using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PhoneStoreApp.Assets.Contains;
using PhoneStoreApp.Models;
using PhoneStoreApp.Models.SubModels;

namespace PhoneStoreApp.Services
{
    public class CartService
    {
        private static CartService instance;

        public static CartService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CartService();
                }
                return instance;
            }
            private set => instance = value;
        }

        public async Task<List<CartItem>> GetAllCart()
        {
            try
            {
                List<CartItem> cartItems = new List<CartItem>();
                var cartList = Const.cartProducts;
                foreach (var p in cartList)
                {
                    cartItems.Add(new CartItem()
                    {
                        ID = p.ID,
                        DisplayName = p.DisplayName,
                        Image1 = p.Image1,
                        Price = p.Price,
                        DiscountPercent =
                        p.DiscountPercent,
                        DiscountPrice = p.DiscountPrice,
                        CommentCount = p.CommentCount,
                        Rating = p.Rating,
                        Count = 1,
                        IsSelected = false
                    });
                }
                return cartItems;
            }
            catch(Exception e)
            {
                return null;
                throw e;
            }
        }       
    }
}
