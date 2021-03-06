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
    public partial class AddCommentPage : ContentPage
    {
        public AddCommentPage(Comment comment, Product product)
        {
            BindingContext = new AddCommentViewModel(comment, product);
            InitializeComponent();
        }
    }
}