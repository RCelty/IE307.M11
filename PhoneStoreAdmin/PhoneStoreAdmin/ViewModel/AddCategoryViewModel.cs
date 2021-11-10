using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PhoneStoreAdmin.Model;
using PhoneStoreAdmin.Service;

namespace PhoneStoreAdmin.ViewModel
{
    public class AddCategoryViewModel : BaseViewModel
    {
        public Category categoryGlobal { get; set; }

        public string DisplayName { get; set; }

        public Window w { get; set; }

        public ICommand AddOrEditCategoryCommand { get; set; }
        public AddCategoryViewModel(Category category, Window window)
        {
            w = window;

            categoryGlobal = category;
            DisplayName = category.DisplayName;

            AddOrEditCategoryCommand = new RelayCommand<object>((p) => { return true; }, (p) => { AddOrEditCategoryCommandExecute(new Category { ID = categoryGlobal.ID, DisplayName = DisplayName }); });
        }

        async void AddOrEditCategoryCommandExecute(Category category)
        {
            if (category.ID == null)
            {               
                await CategoryService.Instance.AddCategory(category);
                w.Close();
            }
            else
            {
                await CategoryService.Instance.UpdateCategory(category);
                w.Close();
            }
        }
    }
}
