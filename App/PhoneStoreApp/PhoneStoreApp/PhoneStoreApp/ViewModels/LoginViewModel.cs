using System;
using System.Collections.Generic;
using System.Text;
using PhoneStoreApp.Assets.Contains;
using PhoneStoreApp.Models;
using PhoneStoreApp.Services;
using PhoneStoreApp.Views;
using Xamarin.Forms;


namespace PhoneStoreApp.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        private string username;
        public string UserName
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged();
                LoginCommand.ChangeCanExecute();
            }
        }

        private string userpassword;
        public string UserPassword
        {
            get => userpassword;
            set
            {
                userpassword = value;
                OnPropertyChanged();
                LoginCommand.ChangeCanExecute();
            }
        }

        #region Command
        public Command LostPasswordCommand { get; set; }
        public Command LoginCommand { get; set; }
        public Command SignUpCommand { get; set; }
        #endregion

        public LoginViewModel()
        {
            //LoadData();
            LoginCommand = new Command(LoginCommandExecute, () => LoginCommandCanExecute());
            LostPasswordCommand = new Command(LostPasswordCommandExecute, () => true); ;
            SignUpCommand = new Command(SignUpCommandExecute, () => true);
        }

        public async void LostPasswordCommandExecute()
        {
            await App.Current.MainPage.Navigation.PushAsync(new LostPasswordPage());
        }

        public async void LoginCommandExecute()
        {
            var customer = await LoginServices.Instance.LoginAsync(UserName, UserPassword);
            if (customer != null)
            {
                await App.Current.MainPage.Navigation.PushAsync(new MainViewPage());
                Const.CurrentCustomerID = (int)customer.ID;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("","Tên đang nhập hoặc mật khẩu không đúng","OK");
            }
        }
        public async void SignUpCommandExecute()
        {
            await App.Current.MainPage.Navigation.PushAsync(new SignUpPage());
        }

        bool LoginCommandCanExecute()
        {
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(UserPassword))
                return false;
            return true;
        }
    }
}
