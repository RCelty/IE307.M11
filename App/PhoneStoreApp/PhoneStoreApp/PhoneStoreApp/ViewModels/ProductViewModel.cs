using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using PhoneStoreApp.Models;
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
        #endregion

        public ProductViewModel(ObservableCollection<Product> products)
        {
            Products = products;

            ProductDetailOnClick = new Command<Product>(ProductDetailOnClickExcute, product => product != null);
        }

        public async void ProductDetailOnClickExcute(Product product)
        {
            await App.Current.MainPage.Navigation.PushAsync(new ProductDetailPage(product.ID), true);
        }
    }
}
