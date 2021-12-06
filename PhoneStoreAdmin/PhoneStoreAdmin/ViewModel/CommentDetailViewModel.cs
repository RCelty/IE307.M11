using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneStoreAdmin.Assets.Contain;
using PhoneStoreAdmin.Model;
using PhoneStoreAdmin.Service;

namespace PhoneStoreAdmin.ViewModel
{
    public class CommentDetailViewModel : BaseViewModel
    {
        private Comment comment;

        public Comment Comment
        {
            get => comment;
            set
            {
                comment = value;
                OnPropertyChanged();
            }
        }

        private Product product;

        public Product Product
        {
            get => product;
            set
            {
                product = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Product> products;

        public ObservableCollection<Product> Products
        {
            get => products;
            set
            {
                products = value;
                OnPropertyChanged();
            }
        }

        private string productImage;
        public string ProductImage
        {
            get => productImage; set
            {
                productImage = value;
                OnPropertyChanged();
            }
        }

        private string productName;
        public string ProductName
        {
            get => productName; set
            {
                productName = value;
                OnPropertyChanged();
            }
        }

        private string customerName;
        public string CustomerName
        {
            get => customerName; set
            {
                customerName = value;
                OnPropertyChanged();
            }
        }

        private DateTime creationDate;

        public DateTime CreationDate
        {
            get => creationDate;
            set
            {
                creationDate = value;
                OnPropertyChanged();
            }
        }

        private int rating;
        public int Rating
        {
            get => rating; set
            {
                rating = value;
                OnPropertyChanged();
            }
        }

        private string content;
        public string Content
        {
            get => content; set
            {
                content = value;
                OnPropertyChanged();
            }
        }

        public CommentDetailViewModel(Comment comment)
        {
            if (comment != null)
            {
                Comment = comment;
            }

            LoadData();


        }

        public async void LoadData()
        {
            Products = new ObservableCollection<Product>(await ProductService.Instance.GetAllProductAsync());

            Product = Products.SingleOrDefault(p => p.ID == Comment.ProductID);
            if (Product != null)
            {
                string oldDomain = "http://10.0.2.2:88/";
                ProductImage = Product.Image1.Replace(oldDomain, Const.Domain);
                ProductName = Product.DisplayName;
                CustomerName = Comment.CustomerName;
                CreationDate = Comment.CreationDate;
                Rating = (int)Comment.Rating;
                Content = Comment.Content;
            }
        }
    }
}
