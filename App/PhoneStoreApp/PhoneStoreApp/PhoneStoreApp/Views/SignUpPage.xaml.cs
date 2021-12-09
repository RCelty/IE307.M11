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
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private async void btnSendFormSignUp_Clicked(object sender, EventArgs e)
        {
            if (isCheckValidate())
            {
                Navigation.PushAsync(new SignUpSuccessPage());
            }
            else
            {
                await DisplayAlert("", "Thông tin đăng ký chưa đúng", "Ok");
            }
        }

        public Boolean isCheckValidate()
        {
            const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
            const string passwordRegex = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$";

            string txtEmail = txtLoginUserEmail.Text;
            string txtPassword = txtUserPassword.Text;
            string txtConfirmPassword = txtUserConfirmPassword.Text;
            string txtUsername = txtUserName.Text;

            if (txtEmail == null || txtPassword == null || txtConfirmPassword == null || txtUsername == null)
            {
               
                return false;
            }
            else
            {

                if (!Regex.IsMatch(txtEmail, emailRegex) || !Regex.IsMatch(txtPassword, passwordRegex) || !txtPassword.Equals(txtConfirmPassword))
                {

                    if (!Regex.IsMatch(txtEmail, emailRegex))
                    {
                        lbUserMailError.IsVisible = true;
                        lbUserMailError.Text = "Nhập sai dịnh dạng mail";
                    }

                    if (!Regex.IsMatch(txtPassword, passwordRegex))
                    {
                        lbUserPasswordError.IsVisible = true;
                        lbUserPasswordError.Text = "Mật khảu gồm 8 ký tự ít nhất một số, và một chữ";
                    }

                    if (!txtPassword.Equals(txtConfirmPassword))
                    {
                        lbConfirmPasswordError.IsVisible = true;
                        lbConfirmPasswordError.Text = "Xác thực mật khẩu không chính xác";
                    }
                    return false;
                }
                else // case: input success, check exist account
                {
                    return true;
                }
            }
        }
    }
}