using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PhoneStoreAdmin.Model;
using PhoneStoreAdmin.Service;

namespace PhoneStoreAdmin.ViewModel
{
    public class CategoryViewModel : BaseViewModel
    {
        private ObservableCollection<Category> categories;

        public ObservableCollection<Category> Categories
        {
            get => categories;
            set
            {
                categories = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCategoryCommand { get; set; }
        public CategoryViewModel()
        {
            LoadData();           
        }

        private async void LoadData()
        {
            
            Categories = new ObservableCollection<Category>(await CategoryService.Instance.GetAllCategoryAsync());
            
        }
    }
}
