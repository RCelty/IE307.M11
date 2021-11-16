using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using PhoneStoreApp.Models;
using PhoneStoreApp.Models.SubModels;
using PhoneStoreApp.Services;
using PhoneStoreApp.Views;
using Xamarin.Forms;

namespace PhoneStoreApp.ViewModels
{
    class FilterViewModel : BaseViewModel
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

        private ObservableCollection<BrandChoice> brandChoices;

        public ObservableCollection<BrandChoice> BrandChoices
        {
            get => brandChoices;
            set
            {
                brandChoices = value;
                OnPropertyChanged();
            }
        }

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

        private ObservableCollection<CategoryChoice> categoryChoices;

        public ObservableCollection<CategoryChoice> CategoryChoices
        {
            get => categoryChoices;
            set
            {
                categoryChoices = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<RateChoice> rateChoices;

        public ObservableCollection<RateChoice> RateChoices
        {
            get => rateChoices;
            set
            {
                rateChoices = value;
                OnPropertyChanged();
            }
        }

        private int brandcustomheight;

        public int BrandCustomHeight
        {
            get => brandcustomheight;
            set
            {
                brandcustomheight = value;
                OnPropertyChanged();
            }
        }

        private int categorycustomheight;

        public int CategoryCustomHeight
        {
            get => categorycustomheight;
            set
            {
                categorycustomheight = value;
                OnPropertyChanged();
            }
        }

        private int minprice;

        public int MinPrice
        {
            get => minprice;
            set
            {
                minprice = value;
                OnPropertyChanged();
            }
        }

        private int maxprice;

        public int MaxPrice
        {
            get => maxprice;
            set
            {
                maxprice = value;
                OnPropertyChanged();
            }
        }

        private bool isSaleOff;

        public bool IsSaleOff
        {
            get => isSaleOff;
            set
            {
                isSaleOff = value;
                OnPropertyChanged();
            }
        }

        #region Command
        public Command ApplyCommand { get; set; }
        #endregion

        public FilterViewModel()
        {
            LoadData();
            ApplyCommand = new Command(ApplyCommandExecute, () => true);
        }

        public async void LoadData()
        {
            Brands = new ObservableCollection<Brand>(await HomeService.Instance.GetAllBrandyAsync());
            BrandChoices = new ObservableCollection<BrandChoice>(BrandChoicesInit(Brands));

            Categories = new ObservableCollection<Category>(await HomeService.Instance.GetAllCategoryAsync());
            CategoryChoices = new ObservableCollection<CategoryChoice>(CategoryChoicesInit(Categories));

            RateChoices = new ObservableCollection<RateChoice>(RateChoicesInit());

            BrandCustomHeight = (Brands.Count() / 3) > 0 ? 80 * (Brands.Count() / 3) : 80;
            CategoryCustomHeight = (Categories.Count() / 3) > 0 ? 80 * (Categories.Count() / 3) : 80;

            var productList = new ObservableCollection<Product>(await HomeService.Instance.GetAllProduct()).ToList();

            MinPrice = 0;
            MaxPrice = GetMaxPrice(productList);

            IsSaleOff = false;
        }

        List<BrandChoice> BrandChoicesInit(ObservableCollection<Brand> brands)
        {
            var result = new List<BrandChoice>();
            foreach (var b in brands)
            {
                var test = b.Image;
                result.Add(new BrandChoice { ID = b.ID, DisplayName = b.DisplayName, Image = b.Image, IsSelected = false });
            }
            return result;
        }

        List<CategoryChoice> CategoryChoicesInit(ObservableCollection<Category> categories)
        {
            var result = new List<CategoryChoice>();
            foreach(var c in categories)
            {
                result.Add(new CategoryChoice { ID = c.ID, DisplayName = c.DisplayName, IsSelected = false });
            }
            return result;
        }

        List<RateChoice> RateChoicesInit()
        {
            var result = new List<RateChoice>();
            for(int i =1; i<=5; i++)
            {
                result.Add(new RateChoice { DisplayName = i + " sao", Value = i, IsSelected = false });
            }
            return result;
        }

        int GetMaxPrice(List<Product> products)
        {
            var result = products[0].DiscountPrice;
            foreach(var p in products)
            {
                if (p.DiscountPrice > result)
                {
                    result = p.DiscountPrice;
                }
            }
            return (int)result;
        }

        public async void ApplyCommandExecute()
        {
            var selectedCategoryList = CategoryChoices.ToList();
            selectedCategoryList = selectedCategoryList.FindAll(c => c.IsSelected == true);

            var selectedBrandList = BrandChoices.ToList();
            selectedBrandList = selectedBrandList.FindAll(b => b.IsSelected == true);

            var selectedRateList = RateChoices.ToList();
            selectedRateList = selectedRateList.FindAll(r => r.IsSelected == true);

            var productList = await HomeService.Instance.GetAllProduct();

            if (selectedCategoryList != null && selectedCategoryList.Count > 0)
            {
                var itemList = new List<Product>();
                foreach(var c in selectedCategoryList)
                {
                    foreach(var p in productList)
                    {
                        if (p.CategoryID == c.ID)
                        {
                            itemList.Add(p);
                        }
                    }
                }
                productList = itemList;
            }

            if (selectedBrandList != null && selectedBrandList.Count > 0)
            {
                var itemList = new List<Product>();
                foreach(var p in productList)
                {
                    foreach(var b in selectedBrandList)
                    {
                        if (p.BrandID == b.ID)
                        {
                            itemList.Add(p);
                        }
                    }
                }
                productList = itemList;
            }

            if (selectedRateList != null && selectedRateList.Count > 0)
            {
                var itemList = new List<Product>();
                foreach(var p in productList)
                {
                    foreach(var r in selectedRateList)
                    {
                        if (Math.Round((decimal)p.Rating) == r.Value)
                        {
                            itemList.Add(p);
                        }
                    }
                }
                productList = itemList;
            }
            productList = productList.FindAll(p => p.DiscountPrice >= MinPrice && p.DiscountPrice <= MaxPrice);
            if (IsSaleOff == true)
            {
                productList = productList.FindAll(p => p.DiscountPercent > 0);
            }
            await App.Current.MainPage.Navigation.PopAsync();
            await App.Current.MainPage.Navigation.PushAsync(new ProductPage(new ObservableCollection<Product>(productList)));
        }
    }
}
