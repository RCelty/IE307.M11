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
    class ProductViewModel : BaseViewModel
    {
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
        public Command ProductDetailOnClick { get; set; }
        public Command SearchCommand { get; set; }
        public Command OpenFilterCommand { get; set; }
        #endregion

        public ProductViewModel(ObservableCollection<Product> products)
        {
            if (products != null)
                Products = products;

            ProductDetailOnClick = new Command<Product>(ProductDetailOnClickExcute, product => product != null);
            SearchCommand = new Command<string>(SearchCommandExecute, (s) => true);
            OpenFilterCommand = new Command(OpenFilterCommandExecute, () => true);
        }

        public async void ProductDetailOnClickExcute(Product product)
        {
            await App.Current.MainPage.Navigation.PushAsync(new ProductDetailPage(product.ID), true);
        }

        public async void SearchCommandExecute(string searchText)
        {
            var s = searchText;
            var resultList = await ProductService.Instance.GetProductBySearchText(s);
            await App.Current.MainPage.Navigation.PopAsync();
            await App.Current.MainPage.Navigation.PushAsync(new ProductPage(new ObservableCollection<Product>(resultList)));
        }

        public async void OpenFilterCommandExecute()
        {
            await App.Current.MainPage.Navigation.PopAsync();
            await App.Current.MainPage.Navigation.PushAsync(new FilterPage());
        }
    }
}
