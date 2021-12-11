using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PhoneStoreApp.ViewModels;

namespace PhoneStoreApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LostPasswordPage : ContentPage
    {
        public LostPasswordPage()
        {
            InitializeComponent();
            BindingContext = new LostPasswordPageViewMoel();
        }

    }
}