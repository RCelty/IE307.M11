using System;
using System.Collections.Generic;
using System.Text;

using PhoneStoreApp.Models;
using PhoneStoreApp.Services;
using PhoneStoreApp.Views;
using Xamarin.Forms;

namespace PhoneStoreApp.ViewModels
{
    class SignUpViewModel : BaseViewModel
    {
        private string customeremail;
        public string CustomerEmail
        {
            get => customeremail;
            set
            {
                customeremail = value;
                OnPropertyChanged();
            }
        }

        private string customerdisplayname;
        public string CustomerDisplayName
        {
            get => customerdisplayname;
            set
            {
                customerdisplayname = value;
                OnPropertyChanged();
            }
        }

        private string customername;
        public string CustomerName
        {
            get => customername;
            set
            {
                customername = value;
                OnPropertyChanged();
            }
        }

        private string customerpassword;
        public string CustomerPassword
        {
            get => customerpassword;
            set
            {
                customerpassword = value;
                OnPropertyChanged();
            }
        }

        private string customerconfirmpassword;
        public string CustomerConfirmPassword
        {
            get => customerconfirmpassword;
            set
            {
                customerconfirmpassword = value;
                OnPropertyChanged();
            }
        }

        #region Command
        public Command GoBackOnClick { get; set; }
        public Command SendFormSignUpCommand { set; get; }
        #endregion

        public SignUpViewModel()
        {
            

            GoBackOnClick = new Command(GoBackOnClickExecute, () => true);
            SendFormSignUpCommand = new Command(SendFormSignUpCommandExecute, () =>  true);
        }

        private async void GoBackOnClickExecute()
        {
            App.Current.MainPage.Navigation.PopAsync();
        }

        private async void SendFormSignUpCommandExecute()
        {
            Customer customer = new Customer() { Email = CustomerEmail, DisplayName = CustomerDisplayName, UserName = CustomerName, PassWord = CustomerPassword };

            if (customer != null)
            {
                var isRegisterAlbe = await Services.LoginServices.Instance.IsRegisterAlbe(customer);
                if (isRegisterAlbe == 1)
                {
                    App.Current.MainPage.Navigation.PushAsync(new VerifyCodePage(customer));
                }
                else if (isRegisterAlbe == -1)
                {
                        App.Current.MainPage.DisplayAlert("Thông báo", "Tài khoản đã tồn tại", "Ok");
                }
                else if (isRegisterAlbe == -2)
                {
                    App.Current.MainPage.DisplayAlert("Thông báo", "Email đăng ký chưa đúng", "Ok");
                }

            }
            
        }

        
    }
}
