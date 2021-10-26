using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Models.EF;

namespace API.Models.DTO
{
    public class CategoryDTO
    {
        public int ID { get; set; }

        public string DisplayName { get; set; }

        public CategoryDTO()
        {

        }

        public CategoryDTO(Category category)
        {
            DisplayName = category.DisplayName;
            ID = category.ID;
        }
    }
}