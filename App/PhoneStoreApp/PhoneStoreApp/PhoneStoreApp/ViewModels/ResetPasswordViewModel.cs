﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using PhoneStoreApp.Models;
using PhoneStoreApp.Services;
using PhoneStoreApp.Views;

namespace PhoneStoreApp.ViewModels
{
    class ResetPasswordViewModel : BaseViewModel
    {

        private string customeroldpassword;
        public string CustomerOldPassword
        {
            get => customeroldpassword;
            set
            {
                customeroldpassword = value;
                OnPropertyChanged();
                SaveNewPassword.ChangeCanExecute();
            }
        }

        private string customernewpassword;
        public string CustomerNewPassword
        {
            get => customernewpassword;
            set
            {
                customernewpassword = value;
                OnPropertyChanged();
                SaveNewPassword.ChangeCanExecute();
            }
        }

        private string customernewpasswordconfirm;
        public string CustomerNewPasswordConfirm
        {
            get => customernewpasswordconfirm;
            set
            {
                customernewpasswordconfirm = value;
                OnPropertyChanged();
                SaveNewPassword.ChangeCanExecute();
            }
        }


        public Customer customerTemp;
        public bool isLostPassword;

        #region Command
        public Command SaveNewPassword { get; set; }
        public Command GoBackOnClick { get; set; }
        #endregion

        public ResetPasswordViewModel (Customer customer, bool isLostPw) // isLostPassword == false "edit password, create new password"
        {
            customerTemp = customer;
            isLostPassword = isLostPw;
            SaveNewPassword = new Command(SaveNewPasswordExecute, () => SaveNewPasswordCanExecute());
            GoBackOnClick = new Command(GoBackOnClickExecute, () => true);
        }

        public async void SaveNewPasswordExecute()
        {
            
            Customer customer = await Services.LoginServices.Instance.GetCustomerUserNameID(customerTemp.UserName);

            // case lost password
            if (isLostPassword)
            {
                checkNewPassword(customer);
            }
            else //case change new password
            {
                if (customer.PassWord.Equals(CustomerOldPassword))
                {
                    checkNewPassword(customer);
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Thông báo", "Mật khẩu cũ không đúng", "OK");
                }
            }
        }

        public async void checkNewPassword(Customer customer)
        {
            if (CustomerNewPassword.Equals(CustomerNewPasswordConfirm))
            {
                customer.PassWord = CustomerNewPassword;
                await Services.LoginServices.Instance.UpdateCustomer(customer);
                await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Mật khẩu xác nhận không chính xác", "OK");
            }
        }

        public async void GoBackOnClickExecute()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        public bool SaveNewPasswordCanExecute()
        {
            if (isLostPassword)
            {
                if (string.IsNullOrWhiteSpace(CustomerNewPassword) ||
                   string.IsNullOrWhiteSpace(CustomerNewPasswordConfirm))
                {
                    return false;
                }
                return true;
            } else
            {
                if (string.IsNullOrWhiteSpace(CustomerOldPassword) ||
                string.IsNullOrWhiteSpace(CustomerNewPassword) ||
                string.IsNullOrWhiteSpace(CustomerNewPasswordConfirm))
                {
                    return false;
                }
                return true;
            }
            
        }
    }
}