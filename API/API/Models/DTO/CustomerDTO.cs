using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Assets.Contain;
using API.Models.EF;

namespace API.Models.DTO
{
    public class CustomerDTO
    {
        public int? ID { get; set; }

        public string UserName { get; set; }

        public string PassWord { get; set; }

        public string DisplayName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Avatar { get; set; }

        public bool? IsAdmin { get; set; }

        public CustomerDTO()
        {

        }

        public CustomerDTO(Customer customer)
        {
            ID = customer.ID;
            UserName = customer.UserName;
            PassWord = customer.PassWord;
            DisplayName = customer.DisplayName;
            PhoneNumber = customer.PhoneNumber;
            Email = customer.Email;
            Address = customer.Address;
            Avatar = Const.CustomerImagePath + customer.Avatar;
            IsAdmin = customer.IsAdmin;
        }       
    }
}