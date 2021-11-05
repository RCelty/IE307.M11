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

        public ICommand OpenAddCategoryCommand { get; set; }
        public ICommand OpenEditCategoryCommand { get; set; }        
        public CategoryViewModel()
        {
            LoadData();
            OpenAddCategoryCommand = new RelayCommand<object>((p) => { return true; }, (p) => { OpenAddCategoryCommandExecute(); });
            OpenEditCategoryCommand = new RelayCommand<Category>((p) => { return true; }, (p) => { OpenEditCategoryCommandExecute(p); });          
        }

        //public CategoryViewModel(Category category)
        //{
        //    LoadData();
        //    categoryGlobal = category;
        //    categoryGlobalDisplayName = categoryGlobal.DisplayName;
        //    //OpenAddCategoryCommand = new RelayCommand<object>((p) => { return true; }, (p) => { OpenAddCategoryCommandExecute(); });
        //    OpenEditCategoryCommand = new RelayCommand<Category>((p) => { return true; }, (p) => { OpenEditCategoryCommandExecute(p); });         
        //}

        private async void LoadData()
        {
            
            Categories = new ObservableCollection<Category>(await CategoryService.Instance.GetAllCategoryAsync());
                       
        }

        void OpenAddCategoryCommandExecute()
        {
            Category category = new Category();
            AddCategoryWindow addCategoryWindow = new AddCategoryWindow(category);
            addCategoryWindow.ShowDialog();
        }

        void OpenEditCategoryCommandExecute(Category category)
        {
            AddCategoryWindow addCategoryWindow = new AddCategoryWindow(category);
            addCategoryWindow.ShowDialog();
        }        
    }
}
