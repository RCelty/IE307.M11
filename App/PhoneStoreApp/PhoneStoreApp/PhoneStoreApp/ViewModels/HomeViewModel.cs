using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

using PhoneStoreApp.Models;
using PhoneStoreApp.Services;
using PhoneStoreApp.Views;
using Xamarin.Forms;
using System.IO;
using Xamarin.Essentials;
using PhoneStoreApp.Assets.Contains;

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

        private ObservableCollection<Product> topDiscountProducts;

        public ObservableCollection<Product> TopDiscountProducts
        {
            get => topDiscountProducts;
            set
            {
                topDiscountProducts = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Product> top5DiscountProducts;

        public ObservableCollection<Product> Top5DiscountProducts
        {
            get => top5DiscountProducts;
            set
            {
                top5DiscountProducts = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Product> topSellProducts;

        public ObservableCollection<Product> TopSellProducts
        {
            get => topSellProducts;
            set
            {
                topSellProducts = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Product> top5SellProducts;

        public ObservableCollection<Product> Top5SellProducts
        {
            get => top5SellProducts;
            set
            {
                top5SellProducts = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Product> topViewProducts;

        public ObservableCollection<Product> TopViewProducts
        {
            get => topViewProducts;
            set
            {
                topViewProducts = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Product> top5ViewProducts;

        public ObservableCollection<Product> Top5ViewProducts
        {
            get => top5ViewProducts;
            set
            {
                top5ViewProducts = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Product> topRateProducts;

        public ObservableCollection<Product> TopRateProducts
        {
            get => topRateProducts;
            set
            {
                topRateProducts = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Product> top5RateProducts;

        public ObservableCollection<Product> Top5RateProducts
        {
            get => top5RateProducts;
            set
            {
                top5RateProducts = value;
                OnPropertyChanged();
            }
        }


        #region Command        
        public Command ProductDetailOnClick { get; set; }
        public Command SearchCommand { get; set; }
        public Command CategoryPressCommand { get; set; }
        public Command OpenFilterCommand { get; set; }
        public Command SeeAllCommand { get; set; }
        public Command ImagePickCommand { get; set; }
        #endregion        
        public HomeViewModel()
        {
            LoadData();

            ProductDetailOnClick = new Command<Product>(ProductDetailOnClickExcute, product => product != null);
            SearchCommand = new Command<string>(SearchCommandExecute, (s) => true);
            CategoryPressCommand = new Command<Category>(CategoryPressCommandExecute, category => category != null);
            OpenFilterCommand = new Command(OpenFilterCommandExecute, () => true);
            SeeAllCommand = new Command<ObservableCollection<Product>>(SeeAllCommandExecute, products => products != null);
            ImagePickCommand = new Command(ImagePickCommandExecute, () => true);
        }

        private async void LoadData()
        {
            if (await HomeService.Instance.GetAllAdvertisement() != null)
                Advertisements = new ObservableCollection<Advertisement>(await HomeService.Instance.GetAllAdvertisement());

            if (await HomeService.Instance.GetAllCategoryAsync() != null)
                Categories = new ObservableCollection<Category>(await HomeService.Instance.GetAllCategoryAsync());

            //if (await HomeService.Instance.GetAllProduct() != null)
            //    Products = new ObservableCollection<Product>(await HomeService.Instance.GetAllProduct());

            if (await HomeService.Instance.GetTopViewProduct() != null)
            {
                TopViewProducts = new ObservableCollection<Product>(await HomeService.Instance.GetTopViewProduct());
                Top5ViewProducts = TopViewProducts;
                if (TopViewProducts.Count >= 5)
                {
                    Top5ViewProducts = new ObservableCollection<Product>(Top5ViewProducts.Take(5).ToList());
                }
            }

            if (await HomeService.Instance.GetTopDiscountProduct() != null)
            {
                TopDiscountProducts = new ObservableCollection<Product>(await HomeService.Instance.GetTopDiscountProduct());
                Top5DiscountProducts = TopDiscountProducts;
                if (TopDiscountProducts.Count >= 5)
                {
                    Top5DiscountProducts = new ObservableCollection<Product>(TopDiscountProducts.Take(5).ToList());
                }
            }

            if (await HomeService.Instance.GetTopSellProduct() != null)
            {
                TopSellProducts = new ObservableCollection<Product>(await HomeService.Instance.GetTopSellProduct());
                Top5SellProducts = TopSellProducts;
                if (TopSellProducts.Count >= 5)
                {
                    Top5SellProducts = new ObservableCollection<Product>(TopSellProducts.Take(5).ToList());
                }
            }

            if (await HomeService.Instance.GetTopRateProduct() != null)
            {
                TopRateProducts = new ObservableCollection<Product>(await HomeService.Instance.GetTopRateProduct());
                Top5RateProducts = TopRateProducts;
                if (TopRateProducts.Count >= 5)
                {
                    Top5RateProducts = new ObservableCollection<Product>(TopRateProducts.Take(5).ToList());
                }
            }
        }


        public async void ProductDetailOnClickExcute(Product product)
        {
            await App.Current.MainPage.Navigation.PushAsync(new ProductDetailPage(product.ID), true);
        }

        public async void SearchCommandExecute(string searchText)
        {
            var s = searchText;
            var resultList = await ProductService.Instance.GetProductBySearchText(s);
            await App.Current.MainPage.Navigation.PushAsync(new ProductPage(new ObservableCollection<Product>(resultList)));
        }

        public async void CategoryPressCommandExecute(Category category)
        {
            var productList = await ProductService.Instance.GetProductByCategoryID(category.ID);
            await App.Current.MainPage.Navigation.PushAsync(new ProductPage(new ObservableCollection<Product>(productList)));
        }

        public async void OpenFilterCommandExecute()
        {
            await App.Current.MainPage.Navigation.PushAsync(new FilterPage());
        }

        public async void SeeAllCommandExecute(ObservableCollection<Product> products)
        {
            await App.Current.MainPage.Navigation.PushAsync(new ProductPage(products));
        }

        public async void ImagePickCommandExecute()
        {
            //var file = await MediaPicker.PickPhotoAsync();
            //if (file == null)
            //    return;

            //byte[] buffer = File.ReadAllBytes(file.FullPath);
            //byte[] ImageData = buffer;

            //string ImageName = file.FileName;

            //await LoginServices.Instance.UploadImage(ImageData, ImageName);
            //var user = await LoginServices.Instance.GetCustomerByID(Const.CurrentCustomerID);
            //var result = await LoginServices.Instance.SendOTP(user);
            //var list = await CartService.Instance.GetBillByCustomerID(1);
            //var e = "";
            var result = LoginServices.Instance.ConfirmOTPEmail("DA", "18520459@gm.uit.edu.vn");
            var e = 0;
        }
    }
}
