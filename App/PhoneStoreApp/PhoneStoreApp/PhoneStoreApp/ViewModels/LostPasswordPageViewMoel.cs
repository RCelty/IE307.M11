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
            }
        }

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

        #region Command
        public Command SendOTPCodeCommand { get; set; }
        public Command GoBackOnClick { get; set; }
        #endregion

        public LostPasswordPageViewMoel()
        {
            SendOTPCodeCommand = new Command(SendOTPCodeExecute, () => true);
            GoBackOnClick = new Command(GoBackOnClickExecute, () => true);
        }

        private async void SendOTPCodeExecute()
        {
            Customer customer = new Customer { UserName = CustomerName, Email = CustomerEmail };
            var isRegisterAble = await Services.LoginServices.Instance.IsRegisterAlbe(customer);

            if (isRegisterAble == -1) // check exist account
            {
                await App.Current.MainPage.Navigation.PushAsync(new VerifyCodePage(customer));
            }else
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Tài khoảng không tồn tại", "Ok");
            }
        }

        private async void GoBackOnClickExecute()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
