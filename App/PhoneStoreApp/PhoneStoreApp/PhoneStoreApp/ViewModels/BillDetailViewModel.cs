using System;
using System.Collections.Generic;
using System.Text;
using PhoneStoreApp.Models;
using Xamarin.Forms;
using PhoneStoreApp.Services;
using PhoneStoreApp.Assets.Contains;
using System.Collections.ObjectModel;

namespace PhoneStoreApp.ViewModels
{
    class BillDetailViewModel : BaseViewModel
    {
        private string billID;
        public string BillID
        {
            get => billID;
            set
            {
                billID = value;
                OnPropertyChanged();
            }
        }

        private string billCreationDate;
        public string BillCreationDate
        {
            get => billCreationDate;
            set
            {
                billCreationDate = value;
                OnPropertyChanged();
            }
        }

        private string billCustomerName;
        public string BillCustomerName
        {
            get => billCustomerName;
            set
            {
                billCustomerName = value;
                OnPropertyChanged();
            }
        }

        private string billCustomerPhone;
        public string BillCustomerPhone
        {
            get => billCustomerPhone;
            set
            {
                billCustomerPhone = value;
                OnPropertyChanged();
            }
        }

        private string billCustomerAddress;
        public string BillCustomerAddress
        {
            get => billCustomerAddress;
            set
            {
                billCustomerAddress = value;
                OnPropertyChanged();
            }
        }

        private long billTotalPrice;
        public long BillTotalPrice
        {
            get => billTotalPrice;
            set
            {
                billTotalPrice = value;
                OnPropertyChanged();
            }
        }

        private bool billIsCheckout;

        public bool BillIsCheckout
        {
            get => billIsCheckout;
            set
            {
                billIsCheckout = value;
                OnPropertyChanged();
            }
        }

        private Bill currentbill;
        public Bill CurrentBill
        {
            get => currentbill;
            set
            {
                currentbill = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<BillDetail> listBillDetail;
        public ObservableCollection<BillDetail> ListBillDetail
        {
            get => listBillDetail;
            set
            {
                listBillDetail = value;
                OnPropertyChanged();
            }
        }

        #region Command
        public new Command GoBackOnClick { get; set; }
        public new Command GoProductDetailOnClick { get; set; }
        #endregion
        public BillDetailViewModel(Bill bill)
        {
            CurrentBill = bill;
            LoadData();

            GoBackOnClick = new Command(GoBackOnClickExecute, () => true);
            GoProductDetailOnClick = new Command<BillDetail>(GoProductDetailOnClickExecute, billDetail => billDetail != null); ;
        }

        private async void LoadData()
        {
            BillID = CurrentBill.ID.ToString();
            BillCreationDate = CurrentBill.CreationDate.ToString();
            BillCustomerName = CurrentBill.CustomerName;
            BillCustomerAddress = CurrentBill.CustomerAddress;
            Customer customer = await LoginServices.Instance.GetCustomerByID((int)CurrentBill.CustomerID);
            BillCustomerPhone = customer.PhoneNumber;
            BillTotalPrice = (long)CurrentBill.TotalPrice;
            BillIsCheckout = (bool)CurrentBill.IsCheckOut;

            if (await CartService.Instance.GetBillDetailByBillID((int)CurrentBill.ID) != null)
            {
                ListBillDetail = new ObservableCollection<BillDetail>(await CartService.Instance.GetBillDetailByBillID((int)CurrentBill.ID));
            }

        }

        private async void GoBackOnClickExecute()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private async void GoProductDetailOnClickExecute(BillDetail billDetail)
        {
            await App.Current.MainPage.Navigation.PushAsync(new ProductDetailPage((int)billDetail.ProductID), true);

        }
    }
}
