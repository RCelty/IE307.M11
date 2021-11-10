using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using PhoneStoreApp.Models;
using PhoneStoreApp.Services;
using Xamarin.Forms;

namespace PhoneStoreApp.ViewModels
{
    class ProductViewModel : BaseViewModel
    {
        public int ID { get; set; }

        public ObservableCollection<Product> temp_Product = new ObservableCollection<Product>();
        private Product product;
        public Product Product { get => product; set { product = value; OnPropertyChanged(); } }
        public Command GoBackOnClick { get; set; }
        public ProductViewModel(int ID)
        {
            this.ID = ID;
            LoadData();
            GoBackOnClick = new Command(GoBackOnClickExcute , ()=>true);
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
        private async void GoBackOnClickExcute()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
