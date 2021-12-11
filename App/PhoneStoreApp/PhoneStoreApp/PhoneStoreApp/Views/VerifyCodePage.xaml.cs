using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PhoneStoreApp.ViewModels;
using PhoneStoreApp.Models;

namespace PhoneStoreApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerifyCodePage : ContentPage
    {
        public VerifyCodePage()
        {
            InitializeComponent();
        }
        public VerifyCodePage(Customer customer, bool isSignUp)
        {
            InitializeComponent();
            BindingContext = new VerifyCodeViewModel(customer, isSignUp);
        }
        /*
        public VerifyCodePage(string email)
        {
            InitializeComponent();
            lbEmailVerify.Text = "Mã xác thực đã được gửi đến mail cho " + email;
            btnResendCode.IsEnabled = false;
            InitCouwndown();
        }
        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void btnToResetPassword_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ResetPasswordPage());
        }

        public List<Timer> timers { get; set; }
        public int count;

        public List<Timer> InitTimer()
        {
            return new List<Timer>(){
               new Timer { Date = new DateTime(DateTime.Now.Ticks + new TimeSpan(0, 0, 1, 30).Ticks) }
            };
        }
        public void InitCouwndown()
        {
            timers = InitTimer();
            CouwndownVerifyCode.ItemsSource = timers;
            count = 0;

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                foreach (var timer in timers)
                {
                    var timespan = timer.Date - DateTime.Now;
                    timer.Timespan = timespan;
                }

                CouwndownVerifyCode.ItemsSource = null;
                CouwndownVerifyCode.ItemsSource = timers;

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
            btnResendCode.IsEnabled = true;

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

        private async void btnResendCode_Clicked(object sender, EventArgs e)
        {
            btnResendCode.IsEnabled = false;
            await DisplayAlert("", "Đã gửi lại mã xác thực", "Ok");
            InitCouwndown();
        }*/
    }
}