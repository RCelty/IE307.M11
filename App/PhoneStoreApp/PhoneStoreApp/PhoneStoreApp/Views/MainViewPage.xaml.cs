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
    public partial class MainViewPage : TabbedPage
    {
        public MainViewPage()
        {
            InitializeComponent();
        }

        private void FavoriteProductPage_Appearing(object sender, EventArgs e)
        {
            //(BindingContext as ViewModels.FavoriteProductViewModel).LoadData();
            CurrentPage.BindingContext = new FavoriteProductViewModel();
        }

        private void CartPage_Appearing(object sender, EventArgs e)
        {
            CurrentPage.BindingContext = new CartViewModel();
        }

        private void CustomerPage_Appearing(object sender, EventArgs e)
        {
            CurrentPage.BindingContext = new CustomerProfileViewModel();
        }
    }
}