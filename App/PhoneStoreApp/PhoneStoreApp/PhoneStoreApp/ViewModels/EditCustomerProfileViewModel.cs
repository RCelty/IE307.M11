using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using PhoneStoreApp.Models;
using PhoneStoreApp.Services;
using PhoneStoreApp.Views;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
        #region Command
        public Command GoBackOnClick { get; set; }
        public Command UploadAvatarCommand {get; set; }
        public Command SaveInfoCustomerEdited { get; set; }
        #endregion

        public EditCustomerProfileViewModel(Customer customer)
        {
            LoadData(customer);
            CustomerUser = customer;

            GoBackOnClick = new Command(GoBackOnClickExecute, () => true);
            UploadAvatarCommand = new Command(UploadAvatarCommandExecute, () => true);
            SaveInfoCustomerEdited = new Command(SaveInfoCustomerEditedExecute, () => SaveInfoCustomerEditedCanExecute());
        }

        public void LoadData(Customer customer)
        {
            CustomerAvatar = customer.Avatar;
            CustomerDisplayName = customer.DisplayName;
            CustomerPhone = customer.PhoneNumber;
            CustomerAddress = customer.Address;
        }

        public async void GoBackOnClickExecute() {
            await App.Current.MainPage.Navigation.PopAsync();
        }
        public async void UploadAvatarCommandExecute() {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Thiết bị của bạn không hỗ trợ tác vụ này", "Ok");
                return;
            }

            var mediaOptions = new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Medium
            };

            var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);
            
            if (selectedImageFile == null)
            {
                await App.Current.MainPage.DisplayAlert("Lỗi", "Không thể chọn ảnh, vui lòng thử lại!!!", "Ok");
                return;
            }

            byte[] imageByte = ReadToEnd(selectedImageFile.GetStream());

            Random random = new Random();
            int randomNumber = random.Next(100000,9999999);
            string dateTime = DateTime.Now.ToString("dd-mm-yyyy-hh-mm-ss");
            string ImageName = string.Concat(dateTime, randomNumber.ToString());

            string convertByteToString = Encoding.UTF8.GetString(imageByte);

            CustomerAvatar = convertByteToString;
            var isUploadImageSuccess = await LoginServices.Instance.UploadImage(imageByte, ImageName);
            if (isUploadImageSuccess == null)
            {
                await App.Current.MainPage.DisplayAlert("Lỗi", "Không thể chọn ảnh, vui lòng thử lại!!!", "Ok");
                return;
            }

        }
        public async void SaveInfoCustomerEditedExecute() {
            CustomerUser.DisplayName = CustomerDisplayName;
            CustomerUser.Address = CustomerAddress;
            CustomerUser.PhoneNumber = CustomerPhone;

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
        }

        public bool SaveInfoCustomerEditedCanExecute()
        {
            if (string.IsNullOrWhiteSpace(CustomerDisplayName) ||
                string.IsNullOrWhiteSpace(CustomerAddress) ||
                string.IsNullOrWhiteSpace(CustomerPhone))
            {
                return false;
            }
            return true;
        }

        public static byte[] ReadToEnd(Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }
    }
}
