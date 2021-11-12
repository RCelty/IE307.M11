using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneStoreApp.Models;
using PhoneStoreApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhoneStoreApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductPage : ContentPage
    {
        public ProductPage(ObservableCollection<Product> products)
        {
            InitializeComponent();
            BindingContext = new ProductViewModel(products);
        }
    }
}