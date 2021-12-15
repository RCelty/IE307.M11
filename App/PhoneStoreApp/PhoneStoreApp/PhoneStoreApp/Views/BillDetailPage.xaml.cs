using PhoneStoreApp.Models;
using PhoneStoreApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhoneStoreApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BillDetailPage : ContentPage
    {
        public BillDetailPage()
        {
            InitializeComponent();
        }

        public BillDetailPage(Bill bill)
        {
            InitializeComponent();
            BindingContext = new BillDetailViewModel(bill);
        }
    }
}