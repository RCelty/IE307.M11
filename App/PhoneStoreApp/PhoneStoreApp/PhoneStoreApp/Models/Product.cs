using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneStoreApp.Models
{
    class Product
    {
        public int ID { get; set; }
        public string DisplayName { get; set; }
        public string Img { get; set; }
        public long Price { get; set; }
        public double Rating { get; set; }
        public int CommentCount { get; set; }           
    }
}
