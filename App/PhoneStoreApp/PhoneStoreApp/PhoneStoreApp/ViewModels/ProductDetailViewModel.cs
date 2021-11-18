using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public int ID { get; set; }

        public ObservableCollection<Product> temp_Product = new ObservableCollection<Product>();
        private Product product;
        public Product Product { get => product; set { product = value; OnPropertyChanged(); } }

        #region Command
        public Command GoBackOnClick { get; set; }
        public Command FavoriteCommand { get; set; }
        #endregion
        public ProductDetailViewModel(int ID)
        {
            this.ID = ID;
            LoadData();            
            SetIsFavorite();
            GoBackOnClick = new Command(GoBackOnClickExcute , ()=>true);
            FavoriteCommand = new Command(FavoriteCommandExecute, () => true);
        }
        async void LoadData()
        {    
           temp_Product = new ObservableCollection<Product>(await HomeService.Instance.GetAllProduct());
            foreach (Product product in temp_Product)
            {
                if (product.ID == ID)
                    Product = product;               
            }           
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
                    SetIsFavorite();
                    await App.Current.MainPage.DisplayAlert("Thông báo", "Đã xóa khỏi yêu thích", "OK");
                }
            }
            else
            {
                var result = await ProductService.Instance.AddFavoriteProduct(Const.CurrentCustomerID, ID);
                if (result != -1)
                {
                    SetIsFavorite();
                    await App.Current.MainPage.DisplayAlert("Thông báo", "Đã thêm vào yêu thích", "OK");
                }
            }
        }
    }
}
