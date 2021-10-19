using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using PhoneStoreApp.Models;
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
        public Command FavoriteIconOnClickCommand { get; set; }
        #endregion
        public HomeViewModel()
        {
            createAdvertisement();
            createCategory();
            createProduct();

            FavoriteIconOnClickCommand = new Command(FavoriteIconOnClick);
        }

        public void createAdvertisement()
        {
            Advertisements = new ObservableCollection<Advertisement>();

            Advertisements.Add(new Advertisement { Image = "backgroundLogin.jpg" });
            Advertisements.Add(new Advertisement { Image = "backgroundLogin.jpg" });
            Advertisements.Add(new Advertisement { Image = "backgroundLogin.jpg" });
        }

        public void createCategory()
        {
            Categories = new ObservableCollection<Category>();

            Categories.Add(new Category { ID = 1, DisplayName = "Điện thoại" });
            Categories.Add(new Category { ID = 2, DisplayName = "Laptop" });
        }

        public void createProduct()
        {
            Products = new ObservableCollection<Product>();

            Products.Add(new Product { ID = 1, DisplayName = "Điện thoại 1", Img = "img1.jpg", Price = 2000000, Rating = 4.6, CommentCount = 86 });
            Products.Add(new Product { ID = 2, DisplayName = "Điện thoại 2", Img = "img1.jpg", Price = 2000000, Rating = 4.6, CommentCount = 86 });
            Products.Add(new Product { ID = 3, DisplayName = "Điện thoại 3", Img = "img1.jpg", Price = 2000000, Rating = 4.6, CommentCount = 86 });
        }

        public void FavoriteIconOnClick(object sender)
        {
            App.Current.MainPage.DisplayAlert("Alert", "your message", "OK");
            //for (int i=0; i<Products.Count; i++)
            //{
            //    if (Products[i].ID == (int)sender)
            //    {
            //        Products[i].IsFavoriteProduct = !Products[i].IsFavoriteProduct;
            //        break;
            //    }
            //}   
        }
    }
}
