using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PhoneStoreAdmin.Model;
using PhoneStoreAdmin.Service;

namespace PhoneStoreAdmin.ViewModel
{
    public class BrandViewModel : BaseViewModel
    {
        private ObservableCollection<Brand> brands;

        public ObservableCollection<Brand> Brands
        {
            get => brands;
            set
            {
                brands = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenAddBrandCommand { get; set; }
        public ICommand OpenEditBrandCommand { get; set; }
        public ICommand DeleteBrandCommand { get; set; }

        public BrandViewModel()
        {
            LoadData();
            OpenAddBrandCommand = new RelayCommand<object>((p) => { return true; }, (p) => { OpenAddBrandCommandExecute(); });
            OpenEditBrandCommand = new RelayCommand<Brand>((p) => { return true; }, (p) => { OpenEditBrandCommandExecute(p); });
            DeleteBrandCommand = new RelayCommand<Brand>((p) => { return true; }, (p) => { DeleteBrandCommandExecute(p); });
        }

        private async void LoadData()
        {
            Brands = new ObservableCollection<Brand>(await BrandService.Instance.GetAllBrandAsync());
            string oldDomain = "http://10.0.2.2:88/";
            string newDomain = "http://localhost:88/";
            foreach (var b in Brands)
            {
                b.Image = b.Image.Replace(oldDomain, newDomain);
            }
        }

        void OpenAddBrandCommandExecute()
        {
            Brand brand = new Brand();
            AddBrandWindow addBrandWindow = new AddBrandWindow(brand);
            addBrandWindow.ShowDialog();
            LoadData();
        }

        void OpenEditBrandCommandExecute(Brand brand)
        {
            AddBrandWindow addCategoryWindow = new AddBrandWindow(brand);
            addCategoryWindow.ShowDialog();
            LoadData();
        }

        async void DeleteBrandCommandExecute(Brand brand)
        {
            var result = await BrandService.Instance.DeleteBrand((int)brand.ID);
            if (result)
            {
                Brands.Remove(brand);
            }
        }
    }
}
