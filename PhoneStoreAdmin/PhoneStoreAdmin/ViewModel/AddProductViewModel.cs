using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PhoneStoreAdmin.Model;
using PhoneStoreAdmin.Service;

namespace PhoneStoreAdmin.ViewModel
{
    public class AddProductViewModel : BaseViewModel
    {
        private Product product;

        public Product Product
        {
            get => product;
            set
            {
                product = value;
                OnPropertyChanged();
            }
        }

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

        private int price;

        public int Price
        {
            get => price;
            set
            {
                price = value;
                OnPropertyChanged();
            }
        }

        private int discountPercent;

        public int DiscountPercent
        {
            get => discountPercent;
            set
            {
                discountPercent = value;
                OnPropertyChanged();
            }
        }

        private string description;

        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }

        private string image1;
        public string Image1 { get => image1; set { image1 = value; OnPropertyChanged(); } }

        private string image2;
        public string Image2 { get => image2; set { image2 = value; OnPropertyChanged(); } }

        private string image3;
        public string Image3 { get => image3; set { image3 = value; OnPropertyChanged(); } }

        private string image4;
        public string Image4 { get => image4; set { image4 = value; OnPropertyChanged(); } }


        private ObservableCollection<Category> categories;

        public ObservableCollection<Category> Categories
        {
            get => categories;
            set
            {
                categories = value;
                OnPropertyChanged();
            }
        }

        private Category selectedCategory;

        public Category SelectedCategory
        {
            get => selectedCategory;
            set
            {
                selectedCategory = value;
                OnPropertyChanged();
            }
        }

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

        private Brand selectedBrand;

        public Brand SelectedBrand
        {
            get => selectedBrand;
            set
            {
                selectedBrand = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ProductDetail> productDetails;

        public ObservableCollection<ProductDetail> ProductDetails
        {
            get => productDetails;
            set
            {
                productDetails = value;
                OnPropertyChanged();
            }
        }

        private string selectedDetailName;

        public string SelectedDetailName
        {
            get => selectedDetailName;
            set
            {
                selectedDetailName = value;
                OnPropertyChanged();
            }
        }

        private string selectedDetailDes;

        public string SelectedDetailDes
        {
            get => selectedDetailDes;
            set
            {
                selectedDetailDes = value;
                OnPropertyChanged();
            }
        }

        private ProductDetail selectedDetail;

        public ProductDetail SelectedDetail
        {
            get => selectedDetail;
            set
            {
                selectedDetail = value;
                OnPropertyChanged();
                if (SelectedDetail != null)
                {
                    SelectedDetailName = SelectedDetail.DetailName;
                    SelectedDetailDes = SelectedDetail.DetailDescription;
                }
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public AddProductViewModel(Product product, Window window)
        {
            this.Product = product;
            if (Product.ID != null)
            {
                DisplayName = Product.DisplayName;
                Price = (int)Product.Price;
                DiscountPercent = (int)Product.DiscountPercent;
                Description = Product.Description;
                ProductDetails = new ObservableCollection<ProductDetail>(Product.ProductDetails);
                Image1 = Product.Image1;
                Image2 = Product.Image2;
                Image3 = Product.Image3;
                Image4 = Product.Image4;
            }
            LoadData();

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(SelectedDetailName) || string.IsNullOrEmpty(SelectedDetailDes))
                    return false;
                return true;
            }, (p) => { });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedDetail == null || string.IsNullOrEmpty(SelectedDetailName) || string.IsNullOrEmpty(SelectedDetailDes))
                    return false;
                return true;
            }, (p) => { });
        }

        public async void LoadData()
        {
            Categories = new ObservableCollection<Category>(await CategoryService.Instance.GetAllCategoryAsync());
            Brands = new ObservableCollection<Brand>(await BrandService.Instance.GetAllBrandAsync());

            SelectedCategory = Categories.SingleOrDefault(c => c.ID == Product.CategoryID);
            SelectedBrand = Brands.SingleOrDefault(b => b.ID == Product.BrandID);
        }
    }
}
