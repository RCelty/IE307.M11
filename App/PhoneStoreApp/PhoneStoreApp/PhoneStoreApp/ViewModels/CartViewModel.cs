using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using PhoneStoreApp.Assets.Contains;
using PhoneStoreApp.Models.SubModels;
using PhoneStoreApp.Services;
using Xamarin.Forms;

namespace PhoneStoreApp.ViewModels
{
    class CartViewModel : BaseViewModel
    {
        private ObservableCollection<CartItem> cartProducts;

        public ObservableCollection<CartItem> CartProducts
        {
            get => cartProducts;
            set
            {
                cartProducts = value;
                OnPropertyChanged();
            }
        }

        private int totalPrice;

        public int TotalPrice
        {
            get => totalPrice;
            set
            {
                totalPrice = value;
                OnPropertyChanged();
            }
        }

        #region Command
        public Command DecreaseCommand { get; set; }
        public Command IncreaseCommand { get; set; }
        public Command DeleteCommand { get; set; }
        public Command SelectAllCommand { get; set; }
        public Command DeleteSelectedCommand { get; set; }
        public Command ProductDetailOnClick { get; set; }

        #endregion

        public CartViewModel()
        {
            LoadData();

            DecreaseCommand = new Command<CartItem>(DecreaseCommandExecute, c => c != null);
            IncreaseCommand = new Command<CartItem>(IncreaseCommandExecute, c => c != null);
            DeleteCommand = new Command<CartItem>(DeleteCommandExecute, c => c != null);
            SelectAllCommand = new Command(SelectAllCommandExecute, () => true);
            DeleteSelectedCommand = new Command(DeleteSelectedCommandExecute, () => true);
            ProductDetailOnClick = new Command<CartItem>(ProductDetailOnClickExcute, product => product != null);
        }

        public async void LoadData()
        {
            TotalPrice = 0;
            CartProducts = new ObservableCollection<CartItem>(await CartService.Instance.GetAllCart());
            if (CartProducts != null && cartProducts.Count > 0)
            {
                foreach (var c in CartProducts)
                {
                    TotalPrice += (int)c.Price * c.Count;
                }
            }
        }

        void DecreaseCommandExecute(CartItem cartItem)
        {
            if (cartItem.Count > 1)
            {
                cartItem.Count--;
                TotalPrice -= (int)cartItem.Price;
            }
        }

        void IncreaseCommandExecute(CartItem cartItem)
        {
            cartItem.Count++;
            TotalPrice += (int)cartItem.Price;
        }

        void DeleteCommandExecute(CartItem cartItem)
        {            
            var delete = Const.cartProducts.FirstOrDefault(p => p.ID == cartItem.ID);
            Const.cartProducts.Remove(delete);
            CartProducts.Remove(cartItem);
            TotalPrice -= (int)cartItem.Price;
        }

        void SelectAllCommandExecute()
        {
            foreach(var c in CartProducts)
            {
                c.IsSelected = true;
            }
        }

        public async void DeleteSelectedCommandExecute()
        {
            var selectedList = CartProducts.ToList();
            selectedList = selectedList.FindAll(s => s.IsSelected == true);

            if (selectedList != null && selectedList.Count > 0)
            {
                foreach(var s in selectedList)
                {
                    var delete = Const.cartProducts.SingleOrDefault(c => c.ID == s.ID);
                    Const.cartProducts.Remove(delete);
                    CartProducts.Remove(s);
                    TotalPrice -= (int)s.Price;
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Bạn chưa chọn sản phẩm để xóa", "Ok");
            }
        }

        public async void ProductDetailOnClickExcute(CartItem product)
        {
            //await App.Current.MainPage.DisplayAlert("t", product.ID.ToString(), "OK");
            await App.Current.MainPage.Navigation.PushAsync(new ProductDetailPage(product.ID), true);
        }
    }
}
