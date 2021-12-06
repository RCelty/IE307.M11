using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace PhoneStoreApp.Models
{
    public class Comment : INotifyPropertyChanged
    {
        public int? ID { get; set; }

        public int ProductID { get; set; }

        public int CustomerID { get; set; }

        public DateTime? CreationDate { get; set; }

        public string CustomerName { get; set; }

        private string content;

        public string Content
        {
            get => content;
            set
            {
                content = value;
                OnPropertyChanged();
            }
        }

        public int? Rating { get; set; }

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
