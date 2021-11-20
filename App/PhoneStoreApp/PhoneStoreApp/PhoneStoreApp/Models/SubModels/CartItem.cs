using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace PhoneStoreApp.Models.SubModels
{
    public class CartItem : INotifyPropertyChanged
    {
        public int ID { get; set; }

        public string DisplayName { get; set; }

        public string Image1 { get; set; }

        public int? Price { get; set; }

        public int? DiscountPercent { get; set; }

        public int? DiscountPrice { get; set; }

        public double? Rating { get; set; }

        public int? CommentCount { get; set; }

        private int count;

        public int Count
        {
            get => count;
            set
            {
                count = value;
                OnPropertyChanged();
            }
        }

        private bool isSelected;

        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
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
