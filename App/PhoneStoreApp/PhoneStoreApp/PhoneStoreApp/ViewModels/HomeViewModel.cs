using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using PhoneStoreApp.Models;
using PhoneStoreApp.Services;
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
        #endregion
        public HomeViewModel()
        {            
            LoadData();                       
            clickCommand = new Command<Product>(clickCommandExcute, product => product != null);
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
    }
}
