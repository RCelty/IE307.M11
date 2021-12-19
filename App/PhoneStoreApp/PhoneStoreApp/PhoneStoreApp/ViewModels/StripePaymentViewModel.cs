using PhoneStoreApp.Assets.Contains;
using PhoneStoreApp.Services;
using Stripe;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using PhoneStoreApp.Models;
using PhoneStoreApp.Views;
using System.Collections.ObjectModel;
using PhoneStoreApp.Models.SubModels;

namespace PhoneStoreApp.ViewModels
{
    class StripePaymentViewModel : BaseViewModel
    {
        private string nameCard;
        public string NameCard { get => nameCard; set { nameCard = value; OnPropertyChanged(); } }
        private string cardNumber;
        public string CardNumber { get => cardNumber; set { cardNumber = value; OnPropertyChanged(); } }
        private string cardDate;
        public string CardDate { get => cardDate; set { cardDate = value; OnPropertyChanged(); } }
        private string cvv;
        public string CVV { get => cvv; set { cvv = value; OnPropertyChanged(); } }

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
        private Models.Customer customer;
        public Models.Customer Customer { get => customer; set { customer = value; OnPropertyChanged(); } }
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
        public Command GoBackOnClick { get; set; }
        public Command PayCommand { get; set; }
        public StripePaymentViewModel(int price)
        {
            GoBackOnClick = new Command(GoBackOnClickExcute, () => true);
            PayCommand = new Command(PayCommandExcute, () => !IsBusy);
            TotalPrice = price;
            LoadData();
        }
        async public void LoadData()
        {
            Customer = await LoginServices.Instance.GetCustomerByID(Const.CurrentCustomerID);
            CartProducts = new ObservableCollection<CartItem>(await CartService.Instance.GetAllCart());
        }
        private async void GoBackOnClickExcute()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
        async public void PayCommandExcute()
        {
            if (CardNumber.Length < 19)
                await App.Current.MainPage.DisplayAlert("Thong bao", "Số thẻ không hợp lệ", "Ok");
            if (Convert.ToInt16(CardDate.Substring(3, 2)) <= 21)
            {
                await App.Current.MainPage.DisplayAlert("Thong bao", "Hạn của thẻ không hợp lệ", "Ok");
            }
            if (CardNumber.Length == 19 && Convert.ToInt16(CardDate.Substring(3, 2)) > 21)
            {
                IsBusy = true;
                Pay();
            }


        }
        async public void CreateBill()
        {
            Bill bill = new Bill() { TotalPrice = TotalPrice, CustomerID = Const.CurrentCustomerID };

            int ID = await CartService.Instance.CreateBill(bill);
            if (ID != -1)
            {
                List<BillDetail> billDetails = new List<BillDetail>();
                foreach (var c in CartProducts)
                {
                    BillDetail billdetail = new BillDetail() { ProductID = c.ID, TotalCount = c.Count, BillID = ID, ProductName = c.DisplayName, Price = c.Price };
                    await CartService.Instance.AddBillDetail(billdetail);
                    billDetails.Add(billdetail);
                }
                await CartService.Instance.ChangeBillStatus(ID);
                await CartService.Instance.SendOrderConfirm(bill, true, billDetails, Customer);
                await App.Current.MainPage.DisplayAlert("Thông báo", "Thanh toán thành công, đơn hàng sẽ được chuyển đến bạn sớm", "Ok");
                await App.Current.MainPage.Navigation.PopAsync();
                await App.Current.MainPage.Navigation.PushAsync(new MainViewPage());

            }
        }
        public void Pay()
        {
            StripeConfiguration.SetApiKey("sk_test_51JzRjkFcp2PGaujs1pHdS6N52AEilubL7Uwt3hFU3fRiLG21sYxVmdWyq1gNhjUpg8LiZENFqwGDr6T440vF5WGb00KZVfORI9");

            Stripe.CreditCardOptions stripcard = new Stripe.CreditCardOptions();
            stripcard.Number = CardNumber;
            stripcard.ExpYear = Convert.ToInt16(CardDate.Substring(3, 2));
            stripcard.ExpMonth = Convert.ToInt16(CardDate.Substring(0, 2));
            stripcard.Cvc = CVV;


            //Step 1 : Assign Card to Token Object and create Token

            Stripe.TokenCreateOptions token = new Stripe.TokenCreateOptions();
            token.Card = stripcard;
            Stripe.TokenService serviceToken = new Stripe.TokenService();
            Stripe.Token newToken = serviceToken.Create(token);

            // Step 2 : Assign Token to the Source

            var options = new SourceCreateOptions
            {
                Type = SourceType.Card,
                Currency = "usd",
                Token = newToken.Id
            };

            var service = new SourceService();
            Source source = service.Create(options);

            //Step 3 : Now generate the customer who is doing the payment

            Stripe.CustomerCreateOptions myCustomer = new Stripe.CustomerCreateOptions()
            {
                Name = Customer.DisplayName,
                Email = Customer.Email,
                Description = "Khách hàng của chợ Tech",
            };

            var customerService = new Stripe.CustomerService();
            Stripe.Customer stripeCustomer = customerService.Create(myCustomer);

            //Step 4 : Now Create Charge Options for the customer. 

            var chargeoptions = new Stripe.ChargeCreateOptions
            {
                Amount = TotalPrice,
                Currency = "VND",
                ReceiptEmail = Customer.Email,
                Customer = stripeCustomer.Id,
                Source = source.Id

            };

            //Step 5 : Perform the payment by  Charging the customer with the payment. 
            var service1 = new Stripe.ChargeService();
            Stripe.Charge charge = service1.Create(chargeoptions); // This will do the Payment

            if (charge.Status == "succeeded")
            {
                CreateBill();
            }
        }

    }
}
