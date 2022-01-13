using PhoneStoreApp.Models.SubModels;
using PhoneStoreApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhoneStoreApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StripePaymentPage : ContentPage
    {
        public StripePaymentPage(int toTalPrice, ObservableCollection<CartItem> cartItems)
        {
            BindingContext = new StripePaymentViewModel(toTalPrice, cartItems);
            InitializeComponent();
        }
    }
}