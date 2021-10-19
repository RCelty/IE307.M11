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
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel();

            Device.StartTimer(TimeSpan.FromSeconds(2), (Func<bool>)(() =>
            {
                CarouselViewer.Position = (CarouselViewer.Position + 1) % 3;
                return true;
            }));
        }
    }
}