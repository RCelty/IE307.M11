using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PhoneStoreAdmin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            frame.Navigate(new Uri("ProductPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void SideMenuControl_SelectionChanged(object sender, EventArgs e)
        {
            switch (MenuList.SelectedIndex)
            {
                case 0:
                    frame.Navigate(new Uri("ProductPage.xaml", UriKind.RelativeOrAbsolute));
                    break;
                case 1:
                    frame.Navigate(new Uri("CategoryPage.xaml", UriKind.RelativeOrAbsolute));
                    break;
                case 2:
                    frame.Navigate(new Uri("BrandPage.xaml", UriKind.RelativeOrAbsolute));
                    break;
                case 3:
                    frame.Navigate(new Uri("CommentPage.xaml", UriKind.RelativeOrAbsolute));
                    break;
                case 4:
                    frame.Navigate(new Uri("BillPage.xaml", UriKind.RelativeOrAbsolute));
                    break;
                case 5:
                    frame.Navigate(new Uri("CustomerPage.xaml", UriKind.RelativeOrAbsolute));
                    break;
            }
        }
    }
}
