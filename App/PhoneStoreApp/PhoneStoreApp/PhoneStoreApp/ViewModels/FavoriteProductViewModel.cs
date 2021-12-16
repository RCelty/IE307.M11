using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using PhoneStoreApp.Assets.Contains;
using PhoneStoreApp.Models;
using PhoneStoreApp.Models.SubModels;
using PhoneStoreApp.Services;
using Xamarin.Forms;

namespace PhoneStoreApp.ViewModels
{
    class FavoriteProductViewModel : BaseViewModel
    {
        private ObservableCollection<FavoriteProductItem> favoriteProducts;

        public ObservableCollection<FavoriteProductItem> FavoriteProducts
        {
            get => favoriteProducts;
            set
            {
                favoriteProducts = value;
                OnPropertyChanged();                
            }
        }

        private ObservableCollection<FavoriteProduct> favoriteList;

        public ObservableCollection<FavoriteProduct> FavoriteList
        {
            get => favoriteList;
            set
            {
                favoriteList = value;
                OnPropertyChanged();                
            }
        }

        #region Command
        public Command DeleteCommand { get; set; }
        public Command DeleteAllCommand { get; set; }
        public Command DeleteSelectedCommand { get; set; }
        public Command ProductDetailOnClick { get; set; }
        //public Command SelectAllCmd { get; set; }        
        #endregion

        public FavoriteProductViewModel()
        {
            LoadData();

            DeleteCommand = new Command<FavoriteProductItem>(DeleteCommandExecute, f => f != null);
            DeleteAllCommand = new Command(DeleteAllCommandExecute, () => ButtonCanExecute());
            DeleteSelectedCommand = new Command(DeleteSelectedCommandExecute, () => ButtonCanExecute());
            ProductDetailOnClick = new Command<FavoriteProductItem>(ProductDetailOnClickExcute, product => product != null);
            //SelectAllCmd = new Command(SelectAllCmdExe, () => true);
        }

        public async void LoadData()
        {
            IsBusy = true;
            var productList = await HomeService.Instance.GetAllProduct();

            if (await ProductService.Instance.GetFavoriteProductByCustomerID(Const.CurrentCustomerID) != null)
                FavoriteList = new ObservableCollection<FavoriteProduct>(await ProductService.Instance.GetFavoriteProductByCustomerID(Const.CurrentCustomerID));

            FavoriteProducts = new ObservableCollection<FavoriteProductItem>();
            if (productList != null && productList.Count > 0 && FavoriteList != null && FavoriteList.Count > 0)
            {
                foreach (var f in FavoriteList)
                {
                    foreach (var p in productList)
                    {
                        if (f.ProductID == p.ID)
                        {
                            FavoriteProducts.Add(new FavoriteProductItem
                            {
                                ID = p.ID,
                                DisplayName = p.DisplayName,
                                Price = p.Price,
                                DiscountPercent = p.DiscountPercent,
                                DiscountPrice = p.DiscountPrice,
                                Image1 = p.Image1,
                                Rating = p.Rating,
                                CommentCount = p.CommentCount,
                                IsSelected = false
                            });
                            break;
                        }
                    }
                }
            }
            DeleteAllCommand.ChangeCanExecute();
            DeleteSelectedCommand.ChangeCanExecute();
            IsBusy = false;
        }

        public async void DeleteCommandExecute(FavoriteProductItem favoriteProductItem)
        {
            var result = await ProductService.Instance.DeleteFavoriteProduct(Const.CurrentCustomerID, favoriteProductItem.ID);
            if (result)
            {
                FavoriteProducts.Remove(favoriteProductItem);
                await App.Current.MainPage.DisplayAlert("Thông báo", "Đã xóa khỏi yêu thích", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Có lỗi xảy ra, vui lòng thử lại", "OK");
            }
            DeleteAllCommand.ChangeCanExecute();
            DeleteSelectedCommand.ChangeCanExecute();
        }

        public async void DeleteAllCommandExecute()
        {
            if (FavoriteProducts != null && FavoriteProducts.Count > 0)
            {
                foreach (var f in FavoriteProducts)
                {
                    await ProductService.Instance.DeleteFavoriteProduct(Const.CurrentCustomerID, f.ID);
                }
                FavoriteProducts.Clear();
            }
            DeleteAllCommand.ChangeCanExecute();
            DeleteSelectedCommand.ChangeCanExecute();
        }

        public async void DeleteSelectedCommandExecute()
        {
            var selectedList = FavoriteProducts.ToList();
            selectedList = selectedList.FindAll(f => f.IsSelected == true);

            if (selectedList != null && selectedList.Count > 0)
            {
                foreach (var f in selectedList)
                {
                    await ProductService.Instance.DeleteFavoriteProduct(Const.CurrentCustomerID, f.ID);
                    FavoriteProducts.Remove(f);
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Bạn chưa chọn sản phẩm để xóa", "Ok");
            }
            DeleteAllCommand.ChangeCanExecute();
            DeleteSelectedCommand.ChangeCanExecute();

            //LoadData();
        }

        public async void ProductDetailOnClickExcute(FavoriteProductItem product)
        {
            //await App.Current.MainPage.DisplayAlert("t", product.ID.ToString(), "OK");
            await App.Current.MainPage.Navigation.PushAsync(new ProductDetailPage(product.ID), true);
        }

        bool ButtonCanExecute()
        {
            if (FavoriteProducts == null || FavoriteProducts.Count <= 0)
            {
                return false;
            }
            return true;
        }

        
        //void SelectAllCmdExe()
        //{
        //    foreach(var f in FavoriteProducts)
        //    {
        //        f.IsSelected = true;
        //    }
        //}
    }
}
