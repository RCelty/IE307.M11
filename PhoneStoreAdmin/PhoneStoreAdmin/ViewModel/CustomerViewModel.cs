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
    public class CustomerViewModel : BaseViewModel
    {
        private ObservableCollection<Customer> customers;

        public ObservableCollection<Customer> Customers
        {
            get => customers;
            set
            {
                customers = value;
                OnPropertyChanged();
            }
        }

        #region Command
        public ICommand ChangeRoleCommand { get; set; }
        public ICommand DetailCommand { get; set; }
        #endregion
        public CustomerViewModel()
        {
            LoadData();

            ChangeRoleCommand = new RelayCommand<Customer>((p) => { return true; }, (p) => { ChangeRoleCommandExecute(p); });
            DetailCommand = new RelayCommand<Customer>((p) => { return true; }, (p) => { DetailCommandExecute(p); });
        }

        public async void LoadData()
        {
            if(await CustomerService.Instance.GetAllCustomerAsync() != null)
            {
                Customers = new ObservableCollection<Customer>(await CustomerService.Instance.GetAllCustomerAsync());
            }
        }

        public async void ChangeRoleCommandExecute(Customer customer)
        {
            var result = await CustomerService.Instance.ChangeCustomerRole((int)customer.ID);
            if (result)
            {
                customer.IsAdmin = !customer.IsAdmin;
            }
        }

        void DetailCommandExecute(Customer customer)
        {
            CustomerDetailWindow customerDetailWindow = new CustomerDetailWindow(customer);
            customerDetailWindow.ShowDialog();
        }
    }
}
