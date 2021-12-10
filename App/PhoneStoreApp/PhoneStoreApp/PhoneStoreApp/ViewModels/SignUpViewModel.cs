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
                SendFormSignUpCommand.ChangeCanExecute();
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
                SendFormSignUpCommand.ChangeCanExecute();
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
                SendFormSignUpCommand.ChangeCanExecute();
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
                SendFormSignUpCommand.ChangeCanExecute();
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
                SendFormSignUpCommand.ChangeCanExecute();
            }
        }

        private string customerPhone;
        public string CustomerPhone
        {
            get => customerPhone;
            set
            {
                customerPhone = value;
                OnPropertyChanged();
                SendFormSignUpCommand.ChangeCanExecute();
            }
        }

        private string customerAddress;
        public string CustomerAddress
        {
            get => customerAddress;
            set
            {
                customerAddress = value;
                OnPropertyChanged();
                SendFormSignUpCommand.ChangeCanExecute();
            }
        }

        #region Command
        public Command GoBackOnClick { get; set; }
        public Command SendFormSignUpCommand { set; get; }
        #endregion

        public SignUpViewModel()
        {
            GoBackOnClick = new Command(GoBackOnClickExecute, () => true);
            SendFormSignUpCommand = new Command(SendFormSignUpCommandExecute, () => SendFormSignUpCommandCanExecute());
        }

        private async void GoBackOnClickExecute()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private async void SendFormSignUpCommandExecute()
        {
            Customer customer = new Customer()
            {
                Email = CustomerEmail,
                DisplayName = CustomerDisplayName,
                UserName = CustomerName,
                PassWord = CustomerPassword,
                Address = CustomerAddress,
                PhoneNumber = CustomerPhone
            };

            if (customer != null)
            {
                var isRegisterAlbe = await Services.LoginServices.Instance.IsRegisterAlbe(customer);
                if (isRegisterAlbe == 1)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new VerifyCodePage(customer));
                }
                else if (isRegisterAlbe == -1)
                {
                    await App.Current.MainPage.DisplayAlert("Thông báo", "Tài khoản đã tồn tại", "Ok");
                }
                else if (isRegisterAlbe == -2)
                {
                    await App.Current.MainPage.DisplayAlert("Thông báo", "Email đăng ký chưa đúng", "Ok");
                }

            }

        }

        bool SendFormSignUpCommandCanExecute()
        {
            if (string.IsNullOrWhiteSpace(CustomerDisplayName) ||
                string.IsNullOrWhiteSpace(CustomerEmail) ||
                string.IsNullOrWhiteSpace(CustomerName) ||
                string.IsNullOrWhiteSpace(CustomerPassword) ||
                string.IsNullOrWhiteSpace(CustomerConfirmPassword) ||
                string.IsNullOrWhiteSpace(CustomerAddress) ||
                string.IsNullOrWhiteSpace(CustomerPhone))
            {
                return false;
            }
            return true;
        }


    }
}
