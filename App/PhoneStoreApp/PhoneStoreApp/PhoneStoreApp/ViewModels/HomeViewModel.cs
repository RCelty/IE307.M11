using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using PhoneStoreApp.Models;
using PhoneStoreApp.Services;
using PhoneStoreApp.Views;
using Xamarin.Forms;

namespace PhoneStoreApp.ViewModels
{
    class HomeViewModel : BaseViewModel
    {
        private ObservableCollection<Advertisement> advertisements;

        public ObservableCollection<Advertisement> Advertisements
        {
            get => advertisements;
            set
            {
                advertisements = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Category> categories;

        public ObservableCollection<Category> Categories
        {
            get => categories;
            set
            {
                categories = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Product> products;

        public ObservableCollection<Product> Products
        {
            get => products;
            set
            {
                products = value;
                OnPropertyChanged();
            }
        }


        #region Command
        public Command clickCommand { get; set; }
        public Command ProductDetailOnClick { get; set; }
        public Command SearchCommand { get; set; }
        public Command CategoryPressCommand { get; set; }
        #endregion
        public HomeViewModel()
        {
            LoadData();
            clickCommand = new Command<Product>(clickCommandExcute, product => product != null);
            ProductDetailOnClick = new Command<Product>(ProductDetailOnClickExcute, product => product != null);
            SearchCommand = new Command(SearchCommandExecute, () => true);
            CategoryPressCommand = new Command<Category>(CategoryPressCommandExecute, category => category != null);
        }

        private async void LoadData()
        {
            Advertisements = new ObservableCollection<Advertisement>(await HomeService.Instance.GetAllAdvertisement());
            Categories = new ObservableCollection<Category>(await HomeService.Instance.GetAllCategoryAsync());
            Products = new ObservableCollection<Product>(await HomeService.Instance.GetAllProduct());
        }

        //public void createProduct()
        //{
        //    Products = new ObservableCollection<Product>();

        //    Products.Add(new Product { ID = 1, DisplayName = "Điện thoại 1", Img = "img1.jpg", Price = 2000000, Rating = 4.6, CommentCount = 86 });
        //    Products.Add(new Product { ID = 2, DisplayName = "Điện thoại 2", Img = "img1.jpg", Price = 2000000, Rating = 4.6, CommentCount = 86 });
        //    Products.Add(new Product { ID = 3, DisplayName = "Điện thoại 3", Img = "img1.jpg", Price = 2000000, Rating = 4.6, CommentCount = 86 });
        //}       

        public void clickCommandExcute(Product product)
        {
            var message = product.DisplayName;
            App.Current.MainPage.DisplayAlert("Test", message, "Ok");
        }
        public async void ProductDetailOnClickExcute(Product product)
        {
            await App.Current.MainPage.Navigation.PushAsync(new ProductDetailPage(product.ID), true);
        }

        public async void SearchCommandExecute()
        {
            await App.Current.MainPage.Navigation.PushAsync(new ProductPage(Products));
        }

        public async void CategoryPressCommandExecute(Category category)
        {
            var productList = await ProductService.Instance.GetProductByCategoryID(category.ID);
            await App.Current.MainPage.Navigation.PushAsync(new ProductPage(new ObservableCollection<Product>(productList)));
        }
    }
}
