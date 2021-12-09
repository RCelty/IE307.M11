using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreAdmin.Model
{
    public class Bill : INotifyPropertyChanged
    {
        public int? ID { get; set; }

        public long? TotalPrice { get; set; }

        public DateTime? CreationDate { get; set; }

        public int? CustomerID { get; set; }

        public string CustomerName { get; set; }

        public bool? IsDelete { get; set; }

        private bool isCheckOut;

        public bool IsCheckOut
        {
            get => isCheckOut;
            set
            {
                isCheckOut = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
