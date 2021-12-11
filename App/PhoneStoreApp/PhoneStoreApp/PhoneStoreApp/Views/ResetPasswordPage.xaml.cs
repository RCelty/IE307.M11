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
    public partial class ResetPasswordPage : ContentPage
    {
        public ResetPasswordPage()
        {
            InitializeComponent();
        }

        public ResetPasswordPage(Customer customer, bool isLostPassword) // isLostPassword == false "edit password, create new password"
        {
            InitializeComponent();
            if (isLostPassword)
            {
                lbOldPassword.IsVisible = true;
                txtOldPassword.IsVisible = true;
            }else
            {
                lbOldPassword.IsVisible = false;
                txtOldPassword.IsVisible = false;
            }

            BindingContext = new ResetPasswordViewModel(customer, isLostPassword);
        }
    }
}