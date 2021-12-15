using PhoneStoreApp.Assets.Contains;
using PhoneStoreApp.Models;
using PhoneStoreApp.Services;
using PhoneStoreApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace PhoneStoreApp.ViewModels
{
    class HistoryViewModel : BaseViewModel
    {

        private ObservableCollection<Bill> allBill;
        public ObservableCollection<Bill> AllBill
        {
            get => allBill;
            set
            {
                allBill = value;
                OnPropertyChanged();
            }
        }
        private Customer currentCustomer;
        public Customer CurrentCustomer
        {
            get => currentCustomer;
            set
            {
                currentCustomer = value;
                OnPropertyChanged();
            }
        }

        #region Command
        public Command GoBackOnClick { get; set; }
        public Command BillDetailOnClick { get; set; }
        #endregion
        public HistoryViewModel()
        {
            LoadData();

            GoBackOnClick = new Command(GoBackOnClickExecute, () => true);
            BillDetailOnClick = new Command<Bill>(BillDetailOnClickExecute, bill => bill != null);
        }

        public async void LoadData()
        {
            CurrentCustomer = await LoginServices.Instance.GetCustomerByID(Const.CurrentCustomerID);

            if (await CartService.Instance.GetBillByCustomerID(Const.CurrentCustomerID) != null)
            {
                AllBill = new ObservableCollection<Bill>(await CartService.Instance.GetBillByCustomerID(Const.CurrentCustomerID));

            }

        }

        public async void GoBackOnClickExecute()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        public async void BillDetailOnClickExecute(Bill bill)
        {
            await App.Current.MainPage.Navigation.PushAsync(new BillDetailPage(bill), true);
        }
    }
}
