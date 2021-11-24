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

        public string CustomerName { get; set; }

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
            CustomerName = comment.Customer.DisplayName;
            Content = comment.Content;
            Rating = comment.Rating;
        }
    }
}