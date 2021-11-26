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

        private string displayName;

        public string DisplayName
        {
            get => displayName;
            set
            {
                displayName = value;
                OnPropertyChanged();
            }
        }

        public Window w { get; set; }

        public ICommand AddOrEditCategoryCommand { get; set; }
        public AddCategoryViewModel(Category category, Window window)
        {
            w = window;

            categoryGlobal = category;
            DisplayName = category.DisplayName;

            AddOrEditCategoryCommand = new RelayCommand<object>((p) => { if (string.IsNullOrEmpty(DisplayName)) return false; return true; },
                (p) => { AddOrEditCategoryCommandExecute(new Category { ID = categoryGlobal.ID, DisplayName = DisplayName }); });
        }

        async void AddOrEditCategoryCommandExecute(Category category)
        {
            if (!string.IsNullOrEmpty(category.DisplayName))
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
}
