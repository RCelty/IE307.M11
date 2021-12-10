using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PhoneStoreAdmin.Model;
using PhoneStoreAdmin.Service;

namespace PhoneStoreAdmin.ViewModel
{
    public class BillViewModel : BaseViewModel
    {
        private ObservableCollection<Bill> bills;

        public ObservableCollection<Bill> Bills
        {
            get => bills;
            set
            {
                bills = value;
                OnPropertyChanged();
            }
        }

        #region Command
        public ICommand CheckoutCommand { get; set; }
        public ICommand DetailCommand { get; set; }
        #endregion

        public BillViewModel()
        {
            LoadData();

            CheckoutCommand = new RelayCommand<Bill>((p) => { return true; }, (p) => { CheckoutCommandExecute(p); });
            DetailCommand = new RelayCommand<Bill>((p) => { return true; }, (p) => { DetailCommandExecute(p); });
        }

        public async void LoadData()
        {
            if (await BillService.Instance.GetAllBillAsync() != null)
                Bills = new ObservableCollection<Bill>(await BillService.Instance.GetAllBillAsync());
        }

        public async void CheckoutCommandExecute(Bill bill)
        {
            var result = await BillService.Instance.ChangeBillStatus((int)bill.ID);
            if (result)
            {
                bill.IsCheckOut = !bill.IsCheckOut;
            }
        }

        void DetailCommandExecute(Bill bill)
        {
            BillDetailWindow billDetailWindow = new BillDetailWindow(bill);
            billDetailWindow.ShowDialog();
        }
    }
}
