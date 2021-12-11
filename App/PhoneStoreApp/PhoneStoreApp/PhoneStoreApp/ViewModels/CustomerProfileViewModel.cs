using System;
using System.Collections.Generic;
using System.Text;

using PhoneStoreApp.Models;
using PhoneStoreApp.Assets.Contains;
using PhoneStoreApp.Views;
using Xamarin.Forms;
using PhoneStoreApp.Services;

namespace PhoneStoreApp.ViewModels
{
    class CustomerProfileViewModel : BaseViewModel
    {
        private Customer customeruser;
        public Customer CustomerUser
        {
            get => customeruser;
            set
            {
                customeruser = value;
                OnPropertyChanged();
            }
        }

        private string customeravatar;
        public string CustomerAvatar
        {
            get => customeravatar;
            set
            {
                customeravatar = value;
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
        public Command EditProfileCommand { get; set; }
        public Command NotificationCommand { get; set; }
        public Command UsagePolicyCommand { get; set; }
        public Command SecretPolicyCommand { get; set; }
        public Command ReportCommand { get; set; }
        public Command LogoutCommand { get; set; }
        #endregion

        public CustomerProfileViewModel()
        {
            LoadData();

            EditProfileCommand = new Command(EditProfileCommandExecute, () => true);
            NotificationCommand = new Command(NotificationCommandExecute, () => true);
            UsagePolicyCommand = new Command(UsagePolicyCommandExecute, () => true);
            SecretPolicyCommand = new Command(SecretPolicyCommandExecute, () => true);
            ReportCommand = new Command(ReportCommandExecute, () => true);
            LogoutCommand = new Command(LogoutCommandExecute, () => true);

        }

        private async void LoadData()
        {
            if (await LoginServices.Instance.GetCustomerByID(Const.CurrentCustomerID) != null)
                CustomerUser = await LoginServices.Instance.GetCustomerByID(Const.CurrentCustomerID);

            if (CustomerUser != null)
            {
                CustomerAvatar = CustomerUser.Avatar;
                CustomerName = CustomerUser.DisplayName;
                CustomerEmail = CustomerUser.Email;
            }


        }

        private void EditProfileCommandExecute()
        {

        }

        private void NotificationCommandExecute()
        {

        }
        private void UsagePolicyCommandExecute()
        {

        }
        private void SecretPolicyCommandExecute()
        {

        }
        private void ReportCommandExecute()
        {

        }
        private async void LogoutCommandExecute()
        {
            await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
            Const.CurrentCustomerID = -1;

        }

    }
}
