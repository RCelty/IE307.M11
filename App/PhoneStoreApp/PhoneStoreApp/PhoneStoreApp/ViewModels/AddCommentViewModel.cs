using PhoneStoreApp.Assets.Contains;
using PhoneStoreApp.Models;
using PhoneStoreApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PhoneStoreApp.ViewModels
{
    class AddCommentViewModel : BaseViewModel
    {
        private int starNumber;
        public int StarNumber { get => starNumber; set { starNumber = value; OnPropertyChanged(); } }
        private string star1;
        public string Star1 { get => star1; set { star1 = value; OnPropertyChanged(); } }
        private string star2;
        public string Star2 { get => star2; set { star2 = value; OnPropertyChanged(); } }
        private string star3;
        public string Star3 { get => star3; set { star3 = value; OnPropertyChanged(); } }
        private string star4;
        public string Star4 { get => star4; set { star4 = value; OnPropertyChanged(); } }
        private string star5;
        public string Star5 { get => star5; set { star5 = value; OnPropertyChanged(); } }

        private string lbl;
        public string Lbl { get => lbl; set { lbl = value; OnPropertyChanged(); } }

        private string content;
        public string Content { get => content; set { content = value; OnPropertyChanged(); } }
        private int productID;
        public int ProductID { get => productID; set { productID = value; OnPropertyChanged(); } }
        private Comment commentglobal;
        public Comment Commentglobal { get => commentglobal; set { commentglobal = value; OnPropertyChanged(); } }
        private Product cmtproduct;
        public Product CmtProduct { get => cmtproduct; set { cmtproduct = value; OnPropertyChanged(); } }
        public Command SelectStarCommand { get; set; }
        public Command PostCommentCommand { get; set; }
        public Command GoBackOnClick { get; set; }
        public AddCommentViewModel(Comment comment, Product product)
        {
            if (comment.ID != null)
            {
                SelectStarCommandExcute((comment.Rating).ToString());
                Content = comment.Content;
                PostCommentCommand = new Command(PostCommentCommandExcute, () => true);
            }
            else
            {
                Star1 = "faL";
                Star2 = "faL";
                Star3 = "faL";
                Star4 = "faL";
                Star5 = "faL";
                Lbl = "Số sao (0 sao)";
                PostCommentCommand = new Command(PostCommentCommandExcute, () => true);
            }
            CmtProduct = product;
            commentglobal = comment;
            SelectStarCommand = new Command<string>(SelectStarCommandExcute);
            GoBackOnClick = new Command(GoBackOnClickExcute, () => true);
            ProductID = comment.ProductID;


        }
        public void SelectStarCommandExcute(string value)
        {
            int selectedValue = int.Parse(value);
            switch (selectedValue)
            {
                case 1:
                    Star1 = "faS";
                    Star2 = "faL";
                    Star3 = "faL";
                    Star4 = "faL";
                    Star5 = "faL";
                    StarNumber = selectedValue;
                    Lbl = "Số sao (" + selectedValue + " sao)";
                    break;
                case 2:
                    Star1 = "faS";
                    Star2 = "faS";
                    Star3 = "faL";
                    Star4 = "faL";
                    Star5 = "faL";
                    StarNumber = selectedValue;
                    Lbl = "Số sao (" + selectedValue + " sao)";
                    break;
                case 3:
                    Star1 = "faS";
                    Star2 = "faS";
                    Star3 = "faS";
                    Star4 = "faL";
                    Star5 = "faL";
                    StarNumber = selectedValue;
                    Lbl = "Số sao (" + selectedValue + " sao)";
                    break;
                case 4:
                    Star1 = "faS";
                    Star2 = "faS";
                    Star3 = "faS";
                    Star4 = "faS";
                    Star5 = "faL";
                    StarNumber = selectedValue;
                    Lbl = "Số sao (" + selectedValue + " sao)";
                    break;
                case 5:
                    Star1 = "faS";
                    Star2 = "faS";
                    Star3 = "faS";
                    Star4 = "faS";
                    Star5 = "faS";
                    StarNumber = selectedValue;
                    Lbl = "Số sao (" + selectedValue + " sao)";
                    break;

            }
        }
        async void PostCommentCommandExcute()
        {
            if (commentglobal.ID == null)
            {
                Comment comment = new Comment() { ProductID = ProductID, CustomerID = Const.CurrentCustomerID, Content = Content, Rating = StarNumber };
                if (await CommentService.Instance.AddComment(comment) != -1)
                {
                    await App.Current.MainPage.DisplayAlert("Thông báo", "Thêm đánh giá thành công", "Ok");

                    await App.Current.MainPage.Navigation.PopAsync();
                    await App.Current.MainPage.Navigation.PushAsync(new ProductDetailPage(ProductID));

                }
            }
            else
            {
                Commentglobal.Content = Content;
                Commentglobal.Rating = StarNumber;
                if (await CommentService.Instance.UpdateComment(commentglobal) == true)
                {
                    await App.Current.MainPage.DisplayAlert("Thông báo", "Sửa đánh giá thành công", "Ok");
                    await App.Current.MainPage.Navigation.PopAsync();
                    await App.Current.MainPage.Navigation.PushAsync(new ProductDetailPage(ProductID));
                }
            }
        }
        private async void GoBackOnClickExcute()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
