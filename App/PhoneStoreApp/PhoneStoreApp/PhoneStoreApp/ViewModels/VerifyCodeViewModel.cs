using PhoneStoreApp.Models;
using PhoneStoreApp.Views;
using System;
using System.Collections.Generic;
using System.Text;

using PhoneStoreApp.Models;
using PhoneStoreApp.Services;
using PhoneStoreApp.Views;
using Xamarin.Forms;

namespace PhoneStoreApp.ViewModels
{
    class VerifyCodeViewModel : BaseViewModel
    {
        private string lbemailverify;
        public string lbEmailVerify
        {
            get => lbemailverify;
            set
            {
                lbemailverify = value;
                OnPropertyChanged();
            }
        }

        private string opt1;
        public string OTP1
        {
            get => opt1;
            set
            {
                opt1 = value;
                OnPropertyChanged();
                ConfirmOTP.ChangeCanExecute();
            }
        }

        private string opt2;
        public string OTP2
        {
            get => opt2;
            set
            {
                opt2 = value;
                OnPropertyChanged();
                ConfirmOTP.ChangeCanExecute();
            }
        }

        private string opt3;
        public string OTP3
        {
            get => opt3;
            set
            {
                opt3 = value;
                OnPropertyChanged();
                ConfirmOTP.ChangeCanExecute();
            }
        }

        private string opt4;
        public string OTP4
        {
            get => opt4;
            set
            {
                opt4 = value;
                OnPropertyChanged();
                ConfirmOTP.ChangeCanExecute();
            }
        }

        private string seconds;
        public string Seconds
        {
            get => seconds;
            set
            {
                seconds = value;
                OnPropertyChanged();
            }
        }



        private int count;
        public int CountDown
        {
            get => count;
            set
            {
                count = value;
                OnPropertyChanged();
                //ResendCodeCommand.ChangeCanExecute();
            }
        }
        public string OTP;
        public Customer customer;
        public bool isSingUp;

        #region Command
        public Command GoOnBackClick { get; set; }
        public Command ResendCodeCommand { get; set; }
        public Command ConfirmOTP { get; set; }
        public Command GoBackOnClick { get; set; }
        #endregion

        public VerifyCodeViewModel(Customer customerTemp, bool isSigningUpTemp) // isSigningUpTemp = false -> lost password
        {
            setOTP(customerTemp);
            startTimeSpan();
            customer = customerTemp;
            isSingUp = isSigningUpTemp;
            lbEmailVerify = "Mã xác thực đã được gửi đến mail " + customer.Email;

            GoOnBackClick = new Command(GoOnBackClickExecute, () => true);
            ResendCodeCommand = new Command(ResendCodeCommandExecute, () => ResendCodeCommandCanExecute());
            ConfirmOTP = new Command(ConfirmOTPExecute, () => ConfirmOTPCanExecute());
            GoBackOnClick = new Command(GoBackOnClickExecute, () => true);
        }
        public async void setOTP(Customer customer)
        {
            var otp = await Services.LoginServices.Instance.SendOTP(customer);
            OTP = otp;
        }

        public void startTimeSpan()
        {
            CountDown = 60;

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                CountDown--;
                Seconds = "("+ CountDown.ToString()+")";
                if (CountDown == 0)
                {
                    return false;
                }
                return true;
            });
        }


        public async void GoOnBackClickExecute()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        public async void ResendCodeCommandExecute()
        {
            setOTP(customer);
            startTimeSpan();
        }
        public async void ConfirmOTPExecute()
        {
            string optConfirm = string.Concat(OTP1, OTP2, OTP3, OTP4);

            if (optConfirm == OTP)
            {
                if (isSingUp == true)
                {
                    var RegisterID = await Services.LoginServices.Instance.Register(customer);
                    if (RegisterID != -1)
                    {
                        await App.Current.MainPage.Navigation.PushAsync(new SignUpSuccessPage());
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Thông báo", "Lỗi đăng ký", "Ok");
                    }
                }
                else
                {
                    await App.Current.MainPage.Navigation.PushAsync(new ResetPasswordPage(customer, true));
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Mã xác thực không đúng", "Ok");
            }
        }

        public async void GoBackOnClickExecute()
        {
            await App.Current.MainPage.Navigation.PopAsync();

        }

        public bool ResendCodeCommandCanExecute()
        {
            if (CountDown == 0)
            {
                return true;
            }
            return false;
        }

        public bool ConfirmOTPCanExecute()
        {
            if (string.IsNullOrWhiteSpace(OTP1) ||
                string.IsNullOrWhiteSpace(OTP2) ||
                string.IsNullOrWhiteSpace(OTP3) ||
                string.IsNullOrWhiteSpace(OTP4))
            {
                return false;
            }
            return true;

        }

    }
}
