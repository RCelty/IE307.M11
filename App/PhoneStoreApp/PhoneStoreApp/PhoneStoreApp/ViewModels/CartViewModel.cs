using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using PhoneStoreApp.Assets.Contains;
using PhoneStoreApp.Models.SubModels;
using PhoneStoreApp.Services;
using Xamarin.Forms;
using Xamarin.Essentials;
using PhoneStoreApp.Models;
using PhoneStoreApp.Views;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
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
        public Command CartPageOnClick { get; set; }

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
            CartPageOnClick = new Command(CartPageOnClickExcute, () => !IsBusy);
        }

        public async void LoadData()
        {
            TotalPrice = 0;
            CartProducts = new ObservableCollection<CartItem>(await CartService.Instance.GetAllCart());
            if (CartProducts != null && cartProducts.Count > 0)
            {
                foreach (var c in CartProducts)
                {
                    TotalPrice += (int)c.DiscountPrice * c.Count;
                }
            }
        }

        void DecreaseCommandExecute(CartItem cartItem)
        {
            if (cartItem.Count > 1)
            {
                cartItem.Count--;
                TotalPrice -= (int)cartItem.DiscountPrice;
            }
        }

        void IncreaseCommandExecute(CartItem cartItem)
        {
            cartItem.Count++;
            TotalPrice += (int)cartItem.DiscountPrice;
        }

        void DeleteCommandExecute(CartItem cartItem)
        {            
            var delete = Const.cartProducts.FirstOrDefault(p => p.ID == cartItem.ID);
            Const.cartProducts.Remove(delete);
            CartProducts.Remove(cartItem);
            TotalPrice -= (int)cartItem.DiscountPrice;
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
                    TotalPrice -= (int)s.DiscountPrice;
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Bạn chưa chọn sản phẩm để xóa", "Ok");
            }
        }
        async void CartPageOnClickExcute()
        {
            string action = await App.Current.MainPage.DisplayActionSheet("Bạn muốn thanh toán bằng?", "Hủy", null, "Thanh toán online", "Tiền mặt");

            if (action == "Thanh toán online")
                await App.Current.MainPage.Navigation.PushAsync(new StripePaymentPage(TotalPrice));
            if (action == "Tiền mặt")
            {
                IsBusy = true;
                Bill bill = new Bill() { TotalPrice = TotalPrice, CustomerID = Const.CurrentCustomerID };
                int ID = await CartService.Instance.CreateBill(bill);
                if (ID != -1)
                {
                    foreach (var c in CartProducts)
                    {
                        BillDetail billdetail = new BillDetail() { ProductID = c.ID, TotalCount = c.Count, BillID = ID };
                        await CartService.Instance.AddBillDetail(billdetail);
                    }
                    await CartService.Instance.SendOrderConfirm(ID);
                    IsBusy = false;
                    await App.Current.MainPage.DisplayAlert("Thông báo", "Đặt hàng thành công", "Ok");
                    //await App.Current.MainPage.Navigation.PopAsync();
                    await App.Current.MainPage.Navigation.PushAsync(new MainViewPage());
                }
                IsBusy = false;
            }
        }
        public async void CartPageOnClickMoMoExcute()
        {
            //request params need to request to MoMo system
            string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = "MOMOOJOI20210710";
            string accessKey = "iPXneGmrJH0G8FOP";
            string serectkey = "sFcbSGRSJjwGxwhhcEktCHWYUuTuPNDB";
            string orderInfo = "Thanh toán chợ TECH";
            string returnUrl = "http://phonestore.app.link";
            string notifyurl = "http://ba1adf48beba.ngrok.io/Home/SavePayment"; //lưu ý: notifyurl không được sử dụng localhost, có thể sử dụng ngrok để public localhost trong quá trình test

            string amount = totalPrice.ToString();
            string orderid = DateTime.Now.Ticks.ToString();
            string requestId = DateTime.Now.Ticks.ToString();
            string extraData = "";
            //Before sign HMAC SHA256 signature
            string rawHash = "partnerCode=" +
                partnerCode + "&accessKey=" +
                accessKey + "&requestId=" +
                requestId + "&amount=" +
                amount + "&orderId=" +
                orderid + "&orderInfo=" +
                orderInfo + "&returnUrl=" +
                returnUrl + "&notifyUrl=" +
                notifyurl + "&extraData=" +
                extraData;

            //sign signature SHA256
            string signature = MomoSercurity.signSHA256(rawHash, serectkey);

            //build body json request
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderid },
                { "orderInfo", orderInfo },
                { "returnUrl", returnUrl },
                { "notifyUrl", notifyurl },
                { "extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }

            };
            string responseMomoModel = await MomoService.Instance.sendPaymentRequest(endpoint, message.ToString());

            string responseFromMomo = responseMomoModel;
            JObject jmessage = JObject.Parse(responseFromMomo);
            await Browser.OpenAsync(jmessage.GetValue("payUrl").ToString(), BrowserLaunchMode.SystemPreferred);


        }
        public async void ProductDetailOnClickExcute(CartItem product)
        {
            //await App.Current.MainPage.DisplayAlert("t", product.ID.ToString(), "OK");
            await App.Current.MainPage.Navigation.PushAsync(new ProductDetailPage(product.ID), true);
        }
    }
}
