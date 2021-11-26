using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using PhoneStoreAdmin.Model;
using PhoneStoreAdmin.Service;

namespace PhoneStoreAdmin.ViewModel
{
    public class AddBrandViewModel : BaseViewModel
    {
        public Brand brandGlobal { get; set; }

        private string displayName;

        public string DisplayName
        {
            get => displayName;
            set
            {
                displayName = value;
                OnPropertyChanged();
            }
        }

        private string image;
        public string Image { get => image; set { image = value; OnPropertyChanged(); } }

        public string ImageName { get; set; }

        public byte[] ImageData { get; set; }

        public Window w { get; set; }

        public ICommand ImageChooseCommand { get; set; }

        public ICommand SubmitCommand { get; set; }

        public AddBrandViewModel(Brand brand, Window window)
        {
            w = window;

            brandGlobal = brand;
            if (brandGlobal.ID != null)
            {
                string DomainString = "http://localhost:88/Assets/Images/Brand/";
                DisplayName = brand.DisplayName;
                Image = brand.Image;
                ImageName = brand.Image.Replace(DomainString, "");
            }

            ImageChooseCommand = new RelayCommand<object>((p) => { return true; }, (p) => { ImageChooseCommandExecute(); });
            SubmitCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName) || string.IsNullOrEmpty(ImageName)) 
                    return false; 
                return true;
            }, (p) => { SubmitCommandExecute(new Brand { ID = brandGlobal.ID, DisplayName = DisplayName, Image = ImageName }); });
        }

        void ImageChooseCommandExecute()
        {
            var dialog = new OpenFileDialog();
            //var result = dialog.ShowDialog();
            if (dialog.ShowDialog() == true)
            {
                Image = dialog.FileName;
                ImageName = dialog.SafeFileName;
                byte[] buffer = File.ReadAllBytes(dialog.FileName);
                ImageData = buffer;
            }
        }

        async void SubmitCommandExecute(Brand brand)
        {
            if (brand.ID == null)
            {
                await BrandService.Instance.UploadImage(ImageData, ImageName);
                await BrandService.Instance.AddBrand(brand);
                w.Close();
            }
            else if (ImageData != null)
            {
                await BrandService.Instance.UploadImage(ImageData, ImageName);
                await BrandService.Instance.UpdateBrand(brand);
                w.Close();
            }
            else
            {
                await BrandService.Instance.UpdateBrand(brand);
                w.Close();
            }
        }
    }
}
