using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneStoreAdmin.Model;
using PhoneStoreAdmin.Service;

namespace PhoneStoreAdmin.ViewModel
{
    public class BillDetailViewModel : BaseViewModel
    {
        private Bill bill;

        public Bill Bill
        {
            get => bill;
            set
            {
                bill = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<BillDetail> billDetails;

        public ObservableCollection<BillDetail> BillDetails
        {
            get => billDetails;
            set
            {
                billDetails = value;
                OnPropertyChanged();
            }
        }

        public BillDetailViewModel(Bill bill)
        {
            if (bill != null)
            {
                Bill = bill;
            }
            LoadData();
        }

        public async void LoadData()
        {
            BillDetails = new ObservableCollection<BillDetail>(await BillService.Instance.GetAllBillDetailByBillID((int)bill.ID));
        }
    }
}
