using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using PhoneStoreApp.Assets.Contains;
using PhoneStoreApp.Models;
using PhoneStoreApp.Services;
using Xamarin.Forms;

namespace PhoneStoreApp.ViewModels
{
    class ProductDetailViewModel : BaseViewModel
    {
        private bool isFavorite;

        public bool IsFavorite
        {
            get => isFavorite;
            set
            {
                isFavorite = value;
                OnPropertyChanged();
            }
        }

        private bool isInCart;

        public bool IsInCart
        {
            get => isInCart;
            set
            {
                isInCart = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<string> listImage;
        public ObservableCollection<string> ListImage { get=>listImage; set { listImage = value; OnPropertyChanged(); } }

        public int ID { get; set; }

        public ObservableCollection<Product> temp_Product = new ObservableCollection<Product>();
        private Product product;
        public Product Product { get => product; set { product = value; OnPropertyChanged(); } }
        private List<Comment> listComment;
        public List<Comment> ListComment { get =>listComment; set { listComment = value; OnPropertyChanged(); } }

        #region Command
        public Command GoBackOnClick { get; set; }
        public Command FavoriteCommand { get; set; }
        public Command AddToCartCommand { get; set; }
        #endregion
        public ProductDetailViewModel(int ID)
        {
            this.ID = ID;
            LoadData();
            SetIsFavorite();
            SetIsInCart();

            GoBackOnClick = new Command(GoBackOnClickExcute, () => true);
            FavoriteCommand = new Command(FavoriteCommandExecute, () => true);
            AddToCartCommand = new Command(AddToCartCommandExecute, () => true);
        }
        async void LoadData()
        {
            temp_Product = new ObservableCollection<Product>(await HomeService.Instance.GetAllProduct());
            foreach (Product product in temp_Product)
            {
                if (product.ID == ID)
                {
                    Product = product;
                    break;
                }
            }
            ListImage = new ObservableCollection<string>() {Product.Image1, Product.Image2 , Product.Image3 , Product.Image4 };
            ListComment = await CommentService.Instance.GetCommentByProductID(Product.ID);
        }

        async void SetIsFavorite()
        {
            IsFavorite = false;
            var favProductList = await ProductService.Instance.GetFavoriteProductByCustomerID(Const.CurrentCustomerID);
            if (favProductList != null && favProductList.Count > 0)
            {
                foreach (var f in favProductList)
                {
                    if (f.ProductID == ID)
                    {
                        IsFavorite = true;
                        return;
                    }
                }
            }
        }

        void SetIsInCart()
        {
            IsInCart = false;
            var cartList = Const.cartProducts;

            if (cartList != null && cartList.Count > 0)
            {
                foreach (var c in cartList)
                {
                    if (c.ID == ID)
                    {
                        IsInCart = true;
                        return;
                    }
                }
            }
        }
        private async void GoBackOnClickExcute()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        public async void FavoriteCommandExecute()
        {
            if (IsFavorite)
            {
                var result = await ProductService.Instance.DeleteFavoriteProduct(Const.CurrentCustomerID, ID);
                if (result)
                {
                    IsFavorite = !IsFavorite;
                    await App.Current.MainPage.DisplayAlert("Thông báo", "Đã xóa khỏi yêu thích", "OK");
                }
            }
            else
            {
                var result = await ProductService.Instance.AddFavoriteProduct(Const.CurrentCustomerID, ID);
                if (result != -1)
                {
                    IsFavorite = !IsFavorite;
                    await App.Current.MainPage.DisplayAlert("Thông báo", "Đã thêm vào yêu thích", "OK");
                }
            }
        }

        public async void AddToCartCommandExecute()
        {
            if (IsInCart)
            {
                var delete = Const.cartProducts.SingleOrDefault(c => c.ID == ID);
                Const.cartProducts.Remove(delete);
                IsInCart = !IsInCart;
                await App.Current.MainPage.DisplayAlert("Thông báo", "Đã xóa khỏi giỏ hàng", "OK");
            }
            else
            {
                Const.cartProducts.Add(Product);
                IsInCart = !IsInCart;
                await App.Current.MainPage.DisplayAlert("Thông báo", "Đã thêm vào giỏ hàng", "OK");
            }
        }
    }
}
