using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PhoneStoreApp.Assets.Contains;
using PhoneStoreApp.Models;
using PhoneStoreApp.Services;
using PhoneStoreApp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PhoneStoreApp.ViewModels
{
    class EditCustomerProfileViewModel : BaseViewModel
    {

        private Customer customerUser;
        public Customer CustomerUser
        {
            get => customerUser;
            set
            {
                customerUser = value;
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

        private string customerDisplayName;
        public string CustomerDisplayName
        {
            get => customerDisplayName;
            set
            {
                customerDisplayName = value;
                OnPropertyChanged();
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
            }
        }

        private  string imagename;
        public string ImageName
        {
            get => imagename;
            set
            {
                imagename = value;
                OnPropertyChanged();
            }
        }

        public byte[] ImageData;

        #region Command
        public Command GoBackOnClick { get; set; }
        public Command UploadAvatarCommand { get; set; }
        public Command SaveInfoCustomerEdited { get; set; }
        #endregion

        public static readonly string SourceImagePath = Const.Domain + @"Assets/Images/Customer/";

        public EditCustomerProfileViewModel(Customer customer)
        {
            CustomerUser = customer;
            LoadData();

            GoBackOnClick = new Command(GoBackOnClickExecute, () => true);
            UploadAvatarCommand = new Command(UploadAvatarCommandExecute, () => true);
            SaveInfoCustomerEdited = new Command(SaveInfoCustomerEditedExecute, () => true);
        }

        public void LoadData()
        {
            CustomerDisplayName = CustomerUser.DisplayName;
            CustomerPhone = CustomerUser.PhoneNumber;
            CustomerAddress = CustomerUser.Address;
            CustomerAvatar = CustomerUser.Avatar;

            ImageName = CustomerUser.Avatar.Replace(SourceImagePath, "");
        }

        public async void GoBackOnClickExecute()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
        public async void UploadAvatarCommandExecute()
        {

            var file = await MediaPicker.PickPhotoAsync();
            if (file == null)
            {
                await App.Current.MainPage.DisplayAlert("Lỗi", "Không thể chọn ảnh, vui lòng thử lại!!!", "Ok");
                return;
            }

            byte[] buffer = File.ReadAllBytes(file.FullPath);
            ImageData = buffer;

            string dateTime = DateTime.Now.ToString("dd-mm-yyyy-hh-mm-ss");
            ImageName = dateTime + file.FileName;

            CustomerAvatar = file.FullPath;

        }
        public async void SaveInfoCustomerEditedExecute()
        {
            CustomerUser.DisplayName = CustomerDisplayName;
            CustomerUser.Address = CustomerAddress;
            CustomerUser.PhoneNumber = CustomerPhone;
            CustomerUser.Avatar = ImageName;
            if (ImageData != null)
            {
                await LoginServices.Instance.UploadImage(ImageData, ImageName);
            }

            if (SaveInfoCustomerEditedCanExecute())
            {
                if (CustomerUser != null)
                {
                    bool updateSuccess = await LoginServices.Instance.UpdateCustomer(CustomerUser);
                    if (updateSuccess)
                    {
                        await App.Current.MainPage.Navigation.PopAsync();
                        await App.Current.MainPage.DisplayAlert("", "Cập nhật tài khoảng thành công", "Ok");

                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Thông báo", "Cập nhật tài khoảng không thành công", "Ok");
                    }
                }
            } else
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Không bỏ trống thông tin", "Ok");
            }
        }

        public bool SaveInfoCustomerEditedCanExecute()
        {

            if (string.IsNullOrWhiteSpace(CustomerDisplayName) ||
                string.IsNullOrWhiteSpace(CustomerAddress) ||
                string.IsNullOrWhiteSpace(CustomerPhone) ||
                    string.IsNullOrWhiteSpace(ImageName))
            {
                return false;
            }
            return true;
        }

    }
}
