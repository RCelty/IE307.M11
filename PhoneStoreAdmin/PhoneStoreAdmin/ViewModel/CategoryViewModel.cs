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
        public ICommand DeleteCategoryCommand { get; set; }     
        
        public CategoryViewModel()
        {
            LoadData();
            OpenAddCategoryCommand = new RelayCommand<object>((p) => { return true; }, (p) => { OpenAddCategoryCommandExecute(); });
            OpenEditCategoryCommand = new RelayCommand<Category>((p) => { return true; }, (p) => { OpenEditCategoryCommandExecute(p); });
            DeleteCategoryCommand = new RelayCommand<Category>((p) => { return true; }, (p) => { DeleteCategoryCommandExecute(p); });
        }
        

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
        
        async void DeleteCategoryCommandExecute(Category category)
        {
            await CategoryService.Instance.DeleteCategory(category);       
        }
    }
}
