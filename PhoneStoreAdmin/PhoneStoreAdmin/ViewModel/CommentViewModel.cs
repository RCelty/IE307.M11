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
    public class CommentViewModel : BaseViewModel
    {
        private ObservableCollection<Comment> comments;

        public ObservableCollection<Comment> Comments
        {
            get => comments;
            set
            {
                comments = value;
                OnPropertyChanged();
            }
        }

        #region Command
        public ICommand DeleteCommand { get; set; }
        public ICommand DetailCommand { get; set; }
        #endregion

        public CommentViewModel()
        {
            LoadData();

            DeleteCommand = new RelayCommand<Comment>((p) => { return true; }, (p) => { DeleteCommandExecute(p); });
            DetailCommand = new RelayCommand<Comment>((p) => { return true; }, (p) => { DetailCommandExecute(p); });
        }

        public async void LoadData()
        {
            Comments = new ObservableCollection<Comment>(await CommentService.Instance.GetAllCommentAsync());
        }

        public async void DeleteCommandExecute(Comment comment)
        {
            var result = await CommentService.Instance.DeleteComment((int)comment.ID);

            if (result)
            {
                Comments.Remove(comment);
            }           
        }

        void DetailCommandExecute(Comment comment)
        {
            CommentDetailWindow commentDetailWindow = new CommentDetailWindow(comment);
            commentDetailWindow.ShowDialog();
        }
    }
}
