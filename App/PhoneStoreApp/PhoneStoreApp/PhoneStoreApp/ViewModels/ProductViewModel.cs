using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using PhoneStoreApp.Models;

namespace PhoneStoreApp.ViewModels
{
    class ProductViewModel : BaseViewModel
    {
        private ObservableCollection<Product> product;
        public ObservableCollection<Product> Product { get => product; set { product = value; OnPropertyChanged(); } }
         public ProductViewModel()
        {
            
        }
        
    }
}
