using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhoneStoreApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LostPasswordPage : ContentPage
    {
        public LostPasswordPage()
        {
            InitializeComponent();
        }

        private async void btnSendVerifyCode_Clicked(object sender, EventArgs e)
        {
            const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

            string txtEmail = txtLoginUserEmail.Text;

            if (txtEmail == null)
            {
                await DisplayAlert("", "Vui lòng nhập email", "Ok");

            }
            else
            {
                if (!Regex.IsMatch(txtEmail, emailRegex))
                {
                    lbUserMailError.Text = "Nhập sai dịnh dạng mail";
                }
                else
                {
                    //add code - send verify code for user

                    Navigation.PushAsync(new VerifyCodePage());
                    await DisplayAlert("Thông báo", "Đã gửi mã xác thực qua email", "Ok");
                }
            }
        }
    }
}