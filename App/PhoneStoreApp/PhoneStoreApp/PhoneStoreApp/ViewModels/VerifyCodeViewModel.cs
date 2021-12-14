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

        private string minutes;
        public string Minutes
        {
            get => minutes;
            set
            {
                minutes = value;
                OnPropertyChanged();
                ResendCodeCommand.ChangeCanExecute();
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
                ResendCodeCommand.ChangeCanExecute();
            }
        }



        public int count;
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
            Timer timer = new Timer() { Date = new DateTime(DateTime.Now.Ticks + new TimeSpan(0, 0, 1, 30).Ticks) };
            Minutes = timer.Minutes;
            Seconds = timer.Seconds;

            count = 0;

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                var timespan = timer.Date - DateTime.Now;
                timer.Timespan = timespan;

                Minutes = null;
                Seconds = null;

                Minutes = timer.Minutes;
                Seconds = timer.Seconds;

                if (count == 89)
                {
                    return false;
                }
                else
                {
                    count++;
                    return true;
                }
            });
        }

        public class Timer
        {
            public DateTime Date { get; set; }
            public TimeSpan Timespan { get; set; }
            public string Days => Timespan.Days.ToString("00");
            public string Hours => Timespan.Hours.ToString("00");
            public string Minutes => Timespan.Minutes.ToString("00");
            public string Seconds => Timespan.Seconds.ToString("00");

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
            if (count == 89)
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
