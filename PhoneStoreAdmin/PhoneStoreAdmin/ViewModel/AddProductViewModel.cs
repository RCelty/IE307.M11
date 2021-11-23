using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public string ImageName1 { get; set; }

        public byte[] ImageData1 { get; set; }

        private string image2;
        public string Image2 { get => image2; set { image2 = value; OnPropertyChanged(); } }

        public string ImageName2 { get; set; }

        public byte[] ImageData2 { get; set; }

        private string image3;
        public string Image3 { get => image3; set { image3 = value; OnPropertyChanged(); } }

        public string ImageName3 { get; set; }

        public byte[] ImageData3 { get; set; }

        private string image4;
        public string Image4 { get => image4; set { image4 = value; OnPropertyChanged(); } }

        public string ImageName4 { get; set; }

        public byte[] ImageData4 { get; set; }

        public Window w { get; set; }


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
        public ICommand DeleteCommand { get; set; }
        public ICommand Image1ChooseCommand { get; set; }
        public ICommand Image2ChooseCommand { get; set; }
        public ICommand Image3ChooseCommand { get; set; }
        public ICommand Image4ChooseCommand { get; set; }
        public ICommand SubmitCommand { get; set; }

        public AddProductViewModel(Product product, Window window)
        {
            w = window;
            this.Product = product;
            if (Product.ID != null)
            {
                LoadProductData();
            }
            LoadComboboxData();

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(SelectedDetailName) || string.IsNullOrEmpty(SelectedDetailDes))
                    return false;
                return true;
            }, (p) => { AddCommandExecute(); });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedDetail == null || string.IsNullOrEmpty(SelectedDetailName) || string.IsNullOrEmpty(SelectedDetailDes))
                    return false;
                return true;
            }, (p) => { EditCommandExecute(); });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedDetail == null)
                    return false;
                return true;
            }, (p) => { DeleteCommandExecute(); });

            Image1ChooseCommand = new RelayCommand<object>((p) => { return true; }, (p) => { Image1ChooseCommandExecute(); });
            Image2ChooseCommand = new RelayCommand<object>((p) => { return true; }, (p) => { Image2ChooseCommandExecute(); });
            Image3ChooseCommand = new RelayCommand<object>((p) => { return true; }, (p) => { Image3ChooseCommandExecute(); });
            Image4ChooseCommand = new RelayCommand<object>((p) => { return true; }, (p) => { Image4ChooseCommandExecute(); });

            SubmitCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName) || string.IsNullOrEmpty(Price.ToString()) || string.IsNullOrEmpty(DiscountPercent.ToString()) || string.IsNullOrEmpty(Description) || SelectedCategory == null || SelectedBrand == null ||
                string.IsNullOrEmpty(Image1) || string.IsNullOrEmpty(Image2) || string.IsNullOrEmpty(Image3) || string.IsNullOrEmpty(Image4))
                    return false;
                return true;
            }, (p) => { SubmitCommandExecute(); });
        }

        public async void LoadProductData()
        {
            string DomainString = "http://localhost:88/Assets/Images/Product/";
            DisplayName = Product.DisplayName;
            Price = (int)Product.Price;
            DiscountPercent = (int)Product.DiscountPercent;
            Description = Product.Description;
            ProductDetails = new ObservableCollection<ProductDetail>(await ProductService.Instance.GetAllProductDetailAsync((int)Product.ID));
            Image1 = Product.Image1;
            ImageName1 = Product.Image1.Replace(DomainString, "");
            Image2 = Product.Image2;
            ImageName2 = Product.Image2.Replace(DomainString, "");
            Image3 = Product.Image3;
            ImageName3 = Product.Image3.Replace(DomainString, "");
            Image4 = Product.Image4;
            ImageName4 = Product.Image4.Replace(DomainString, "");
        }
        public async void LoadComboboxData()
        {
            Categories = new ObservableCollection<Category>(await CategoryService.Instance.GetAllCategoryAsync());
            Brands = new ObservableCollection<Brand>(await BrandService.Instance.GetAllBrandAsync());

            SelectedCategory = Categories.SingleOrDefault(c => c.ID == Product.CategoryID);
            SelectedBrand = Brands.SingleOrDefault(b => b.ID == Product.BrandID);
        }

        void EditCommandExecute()
        {
            SelectedDetail.DetailName = SelectedDetailName;
            SelectedDetail.DetailDescription = selectedDetailDes;
        }

        void AddCommandExecute()
        {
            ProductDetail newDetail = new ProductDetail()
            {
                DetailName = SelectedDetailName,
                DetailDescription = SelectedDetailDes
            };
            if (ProductDetails == null)
                ProductDetails = new ObservableCollection<ProductDetail>();
            ProductDetails.Add(newDetail);
        }

        void DeleteCommandExecute()
        {
            ProductDetails.Remove(SelectedDetail);
        }

        void Image1ChooseCommandExecute()
        {
            var dialog = new OpenFileDialog();
            //var result = dialog.ShowDialog();
            if (dialog.ShowDialog() == true)
            {
                Image1 = dialog.FileName;
                ImageName1 = dialog.SafeFileName;
                byte[] buffer = File.ReadAllBytes(dialog.FileName);
                ImageData1 = buffer;
            }
        }

        void Image2ChooseCommandExecute()
        {
            var dialog = new OpenFileDialog();
            //var result = dialog.ShowDialog();
            if (dialog.ShowDialog() == true)
            {
                Image2 = dialog.FileName;
                ImageName2 = dialog.SafeFileName;
                byte[] buffer = File.ReadAllBytes(dialog.FileName);
                ImageData2 = buffer;
            }
        }

        void Image3ChooseCommandExecute()
        {
            var dialog = new OpenFileDialog();
            //var result = dialog.ShowDialog();
            if (dialog.ShowDialog() == true)
            {
                Image3 = dialog.FileName;
                ImageName3 = dialog.SafeFileName;
                byte[] buffer = File.ReadAllBytes(dialog.FileName);
                ImageData3 = buffer;
            }
        }

        void Image4ChooseCommandExecute()
        {
            var dialog = new OpenFileDialog();
            //var result = dialog.ShowDialog();
            if (dialog.ShowDialog() == true)
            {
                Image4 = dialog.FileName;
                ImageName4 = dialog.SafeFileName;
                byte[] buffer = File.ReadAllBytes(dialog.FileName);
                ImageData4 = buffer;
            }
        }

        public async void SubmitCommandExecute()
        {
            if (Product.ID == null)
            {
                Product newProduct = new Product()
                {
                    DisplayName = this.DisplayName,
                    Price = this.Price,
                    DiscountPercent = this.DiscountPercent,
                    Description = this.Description,
                    Image1 = this.ImageName1,
                    Image2 = this.ImageName2,
                    Image3 = this.ImageName3,
                    Image4 = this.ImageName4,
                    CategoryID = this.SelectedCategory.ID,
                    BrandID = this.SelectedBrand.ID
                };
                var productID = await ProductService.Instance.AddProduct(newProduct);
                await ProductService.Instance.UploadImage(ImageData1, ImageName1);
                await ProductService.Instance.UploadImage(ImageData2, ImageName2);
                await ProductService.Instance.UploadImage(ImageData3, ImageName3);
                await ProductService.Instance.UploadImage(ImageData4, ImageName4);

                if (productID != -1 && ProductDetails != null && ProductDetails.Count > 0)
                {
                    foreach (var d in ProductDetails)
                    {
                        ProductDetail productDetail = new ProductDetail()
                        {
                            ProductID = productID,
                            DetailName = d.DetailName,
                            DetailDescription = d.DetailDescription
                        };
                        await ProductService.Instance.AddProductDetail(productDetail);
                    }
                }
            }
            else
            {
                Product.DisplayName = this.DisplayName;
                Product.Price = this.Price;
                Product.DiscountPercent = this.DiscountPercent;
                Product.Description = this.Description;
                Product.Image1 = this.ImageName1;
                Product.Image2 = this.ImageName2;
                Product.Image3 = this.ImageName3;
                Product.Image4 = this.ImageName4;
                Product.CategoryID = this.SelectedCategory.ID;
                Product.BrandID = this.SelectedBrand.ID;

                await ProductService.Instance.UpdateProduct(Product);
                await ProductService.Instance.UploadImage(ImageData1, ImageName1);
                await ProductService.Instance.UploadImage(ImageData2, ImageName2);
                await ProductService.Instance.UploadImage(ImageData3, ImageName3);
                await ProductService.Instance.UploadImage(ImageData4, ImageName4);

                await ProductService.Instance.DeleteProductDetailByProductID((int)Product.ID);

                if (ProductDetails != null && ProductDetails.Count > 0)
                {
                    foreach (var d in ProductDetails)
                    {
                        ProductDetail productDetail = new ProductDetail()
                        {
                            ProductID = Product.ID,
                            DetailName = d.DetailName,
                            DetailDescription = d.DetailDescription
                        };
                        await ProductService.Instance.AddProductDetail(productDetail);
                    }
                }
            }
            w.Close();
        }
    }
}
