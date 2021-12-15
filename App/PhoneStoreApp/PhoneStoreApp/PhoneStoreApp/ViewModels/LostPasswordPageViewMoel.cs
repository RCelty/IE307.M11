using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

using PhoneStoreApp.Models;
using PhoneStoreApp.Services;
using PhoneStoreApp.Views;

namespace PhoneStoreApp.ViewModels
{
    class LostPasswordPageViewMoel : BaseViewModel
    {
        private string customername;
        public string CustomerName
        {
            get => customername;
            set
            {
                customername = value;
                OnPropertyChanged();
                SendOTPCodeCommand.ChangeCanExecute();

            }
        }

        #region Command
        public Command SendOTPCodeCommand { get; set; }
        public Command GoBackOnClick { get; set; }
        #endregion

        public LostPasswordPageViewMoel()
        {
            SendOTPCodeCommand = new Command(SendOTPCodeExecute, () => SendOTPCodeCanExecute());
            GoBackOnClick = new Command(GoBackOnClickExecute, () => true);
        }

        public async void SendOTPCodeExecute()
        {
            Customer customer = await LoginServices.Instance.GetCustomerUserNameID(CustomerName);
            var isRegisterAble = await Services.LoginServices.Instance.IsRegisterAlbe(customer);

            if (isRegisterAble == -1) // check exist account
            {
                await App.Current.MainPage.Navigation.PushAsync(new VerifyCodePage(customer, false));
            }else
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Tài khoảng không tồn tại", "Ok");
            }
        }

        public async void GoBackOnClickExecute()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        public bool SendOTPCodeCanExecute()
        {
            if (string.IsNullOrWhiteSpace(CustomerName))
            {
                return false;
            }
            return true;
        }
    }
}
