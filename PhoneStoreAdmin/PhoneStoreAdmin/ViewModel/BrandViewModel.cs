using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public BrandViewModel()
        {
            LoadData();
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
    }
}
