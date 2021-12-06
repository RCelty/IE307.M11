using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Models.EF;

namespace API.Models.DTO
{
    public class CommentDTO
    {
        public int? ID { get; set; }

        public int? ProductID { get; set; }

        public int? CustomerID { get; set; }

        public DateTime? CreationDate { get; set; }

        public string CustomerName { get; set; }

        public string CustomerAvatar { get; set; }

        public string Content { get; set; }

        public int? Rating { get; set; }

        public CommentDTO()
        {

        }

        public CommentDTO(Comment comment)
        {
            ID = comment.ID;
            ProductID = comment.ProductID;
            CustomerID = comment.CustomerID;
            CreationDate = comment.CreationDate;
            CustomerName = comment.Customer.DisplayName;
            CustomerAvatar = comment.Customer.Avatar;
            Content = comment.Content;
            Rating = comment.Rating;
        }
    }
}