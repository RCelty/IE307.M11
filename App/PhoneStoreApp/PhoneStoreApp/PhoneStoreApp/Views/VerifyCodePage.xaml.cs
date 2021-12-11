using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PhoneStoreApp.ViewModels;
using PhoneStoreApp.Models;

namespace PhoneStoreApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerifyCodePage : ContentPage
    {
        public VerifyCodePage()
        {
            InitializeComponent();
        }
        public VerifyCodePage(Customer customer, bool isSignUp)
        {
            InitializeComponent();
            BindingContext = new VerifyCodeViewModel(customer, isSignUp);
        }
        
    }
}