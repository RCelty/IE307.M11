using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PhoneStoreApp.Models;
using PhoneStoreApp.ViewModels;

namespace PhoneStoreApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCustomerProfilePage : ContentPage
    {
        public EditCustomerProfilePage()
        {
            InitializeComponent();
        }
        public EditCustomerProfilePage(Customer customer)
        {
            InitializeComponent();
            BindingContext = new EditCustomerProfileViewModel(customer);
        }
    }
}