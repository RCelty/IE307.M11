using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhoneStoreApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpSuccessPage : ContentPage
    {
        public SignUpSuccessPage()
        {
            InitializeComponent();
        }

        private void btnSignUpSuccess_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }
    }
}