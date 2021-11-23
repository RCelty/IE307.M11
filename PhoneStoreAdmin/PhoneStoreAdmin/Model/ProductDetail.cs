using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStoreAdmin.Model
{
    public class ProductDetail : INotifyPropertyChanged
    {
        public int? ID { get; set; }
        public int? ProductID { get; set; }

        private string detailName;
        public string DetailName
        {
            get => detailName;
            set
            {
                detailName = value;
                OnPropertyChanged();
            }
        }

        private string detailDescription;
        public string DetailDescription
        {
            get => detailDescription;
            set
            {
                detailDescription = value;
                OnPropertyChanged();
            }
        }

        public bool? IsDeleted { get; set; }

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
