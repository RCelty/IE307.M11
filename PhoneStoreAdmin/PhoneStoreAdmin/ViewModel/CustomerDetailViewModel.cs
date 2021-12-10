using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneStoreAdmin.Model;

namespace PhoneStoreAdmin.ViewModel
{
    class CustomerDetailViewModel : BaseViewModel
    {
        private Customer customer;

        public Customer Customer
        {
            get => customer;
            set
            {
                customer = value;
                OnPropertyChanged();
            }
        }
        public CustomerDetailViewModel(Customer customer)
        {
            Customer = customer;
        }
    }
}
