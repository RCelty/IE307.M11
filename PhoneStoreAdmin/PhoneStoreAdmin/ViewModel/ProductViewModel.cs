using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PhoneStoreAdmin.Assets.Contain;
using PhoneStoreAdmin.Model;
using PhoneStoreAdmin.Service;

namespace PhoneStoreAdmin.ViewModel
{
    public class ProductViewModel : BaseViewModel
    {
        private ObservableCollection<Product> products;

        public ObservableCollection<Product> Products
        {
            get => products;
            set
            {
                products = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenEditCommand { get; set; }
        public ICommand OpenAddProductWindowCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public ProductViewModel()
        {
            LoadData();

            OpenEditCommand = new RelayCommand<Product>((p) => { return true; }, (p) => { OpenEditCommandExecute(p); });
            OpenAddProductWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) => { OpenAddProductWindowCommandExecute(); });
            DeleteCommand = new RelayCommand<Product>((p) => { return true; }, (p) => { DeleteCommandExecute(p); });
        }

        public async void LoadData()
        {
            Products = new ObservableCollection<Product>(await ProductService.Instance.GetAllProductAsync());
            string oldDomain = "http://10.0.2.2:88/";
            //string newDomain = "http://localhost:88/";
            foreach (var p in Products)
            {
                p.Image1 = p.Image1.Replace(oldDomain, Const.Domain);
                p.Image2 = p.Image2.Replace(oldDomain, Const.Domain);
                p.Image3 = p.Image3.Replace(oldDomain, Const.Domain);
                p.Image4 = p.Image4.Replace(oldDomain, Const.Domain);
            }
        }

        void OpenEditCommandExecute(Product product)
        {
            AddProductWindow addProductWindow = new AddProductWindow(product);
            addProductWindow.ShowDialog();
            LoadData();
        }

        void OpenAddProductWindowCommandExecute()
        {
            Product product = new Product();
            AddProductWindow addProductWindow = new AddProductWindow(product);
            addProductWindow.ShowDialog();
            LoadData();
        }

        public async void DeleteCommandExecute(Product product)
        {
            var result = await ProductService.Instance.DeleteProduct((int)product.ID);
            if (result)
            {
                Products.Remove(product);
            }
        }
    }
}
