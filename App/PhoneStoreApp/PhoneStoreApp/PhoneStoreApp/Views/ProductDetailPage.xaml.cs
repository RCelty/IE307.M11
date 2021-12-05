using PhoneStoreApp.Models;
using PhoneStoreApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhoneStoreApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailPage : ContentPage
    {
        public ProductDetailPage(int ID)
        {
            InitializeComponent();
            BindingContext = new ProductDetailViewModel(ID);
            Device.StartTimer(TimeSpan.FromSeconds(2), (Func<bool>)(() =>
            {
                CarouselViewer.Position = (CarouselViewer.Position + 1) % 4;
                return true;
            }));
        }
    }
}