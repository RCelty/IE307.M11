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
using System.Windows.Shapes;
using PhoneStoreAdmin.Model;
using PhoneStoreAdmin.ViewModel;

namespace PhoneStoreAdmin
{
    /// <summary>
    /// Interaction logic for CommentDetailWindow.xaml
    /// </summary>
    public partial class CommentDetailWindow : Window
    {
        public CommentDetailWindow()
        {
            InitializeComponent();           
        }

        public CommentDetailWindow(Comment comment)
        {
            InitializeComponent();
            DataContext = new CommentDetailViewModel(comment);
        }
    }
}
