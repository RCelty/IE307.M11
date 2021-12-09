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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void btnLostPassword_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LostPasswordPage());
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
            const string passwordRegex = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$";

            string txtEmail = txtLoginUserEmail.Text;
            string txtPassword = txtLoginUserPassword.Text;


            if (txtEmail == null || txtPassword == null)
            {
                await DisplayAlert("", "Tài khoản hoặc mật khẩu không đúng", "Ok");

            }
            else
            {
                // case: input error
                if (!Regex.IsMatch(txtEmail, emailRegex) || !Regex.IsMatch(txtPassword, passwordRegex))
                {

                    if (!Regex.IsMatch(txtEmail, emailRegex))
                    {
                        lbUserMailError.Text = "Nhập sai dịnh dạng mail";
                    }

                    if (!Regex.IsMatch(txtPassword, passwordRegex))
                    {
                        lbUserPasswordError.Text = "Mật khảu gồm 8 ký tự ít nhất một số, và một chữ";
                    }

                }
                else // case: input success, check exist account
                {
                    Navigation.PushAsync(new HomePage());
                }
            }
        }

        private void btnSignUpNewAccount_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
        }
    }
}