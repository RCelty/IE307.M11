using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PhoneStoreAdmin.Service;

namespace PhoneStoreAdmin.ViewModel
{
    public class LoginWindowViewModel : BaseViewModel
    {
        private string userName;

        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged();
            }
        }

        public Window window { get; set; }

        //public string PassWord { get; set; }

        public bool IsLogin { get; set; }

        #region Command
        public ICommand ExitCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        #endregion
        public LoginWindowViewModel(Window window)
        {
            this.window = window;
            LoginCommand = new RelayCommand<object>((p) => { return true; }, (p) => { LoginCommandExecute(p); });
        }

        public async void LoginCommandExecute(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var Password = passwordBox.Password;

            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu!", "Thông báo");
            }
            else
            {
                var customer = await LoginService.Instance.LoginAsync(UserName, Password);
                if (customer == null)
                {
                    MessageBox.Show("Tài khoản không tồn tại!", "Thông báo");
                }
                else
                {
                    if (!(bool)customer.IsAdmin)
                    {
                        MessageBox.Show("Bạn không có quyền quản trị!", "Thông báo");
                    }
                    else
                    {
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        window.Close();
                    }
                }
            }           
        }
    }
}
