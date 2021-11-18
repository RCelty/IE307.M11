using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneStoreApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhoneStoreApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoriteProductPage : ContentPage
    {
        public FavoriteProductPage()
        {
            InitializeComponent();            
            BindingContext = new FavoriteProductViewModel();           
        }       
    }
}