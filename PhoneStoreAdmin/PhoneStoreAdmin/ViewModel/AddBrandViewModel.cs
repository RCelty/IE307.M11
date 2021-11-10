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
        public string DisplayName { get; set; }

        private string image;
        public string Image { get => image; set { image = value; OnPropertyChanged(); } }

        public string ImageName { get; set; }

        public byte[] ImageData { get; set; }

        public Window w { get; set; }

        public ICommand ImageChooseCommand { get; set; }

        public ICommand SubmitCommand { get; set; }

        public AddBrandViewModel(Brand brand, Window window)
        {
            ImageChooseCommand = new RelayCommand<object>((p) => { return true; }, (p) => { ImageChooseCommandExecute(); });
            SubmitCommand = new RelayCommand<object>((p) => { return true; }, (p) => { SubmitCommandExecute(); });
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

        async void SubmitCommandExecute()
        {
            await BrandService.Instance.UploadImage(ImageData, ImageName);
        }
    }
}
