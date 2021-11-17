using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using API.Assets.Contain;
using API.Models.DTO;
using API.Models.EF;

namespace API.Models.DAO
{
    public class CustomerDAO
    {
        private static CustomerDAO instance;

        public static CustomerDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CustomerDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        PhoneStoreEntities1 db = new PhoneStoreEntities1();

        public async Task<List<CustomerDTO>> GetAllCustomer()
        {
            var CustomerList = (await db.Customers
                        .ToListAsync())
                        .Select(customer => new CustomerDTO(customer))
                        .ToList();
            return CustomerList;
        }

        public async Task<CustomerDTO> Login(CustomerDTO customerDTO)
        {
            var userName = customerDTO.UserName;
            var passWord = customerDTO.PassWord;

            passWord = Const.CreateMD5(passWord);

            try
            {
                var myCustomer = await db.Customers.SingleOrDefaultAsync(customer => customer.UserName == userName && customer.PassWord == passWord);
                if (myCustomer != null)
                {
                    return new CustomerDTO(myCustomer);
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                return null;
                throw e;
            }
        }

        public async Task<CustomerDTO> GetCustomerByID(int ID)
        {
            try
            {
                var myCustomer = await db.Customers.SingleOrDefaultAsync(customer => customer.ID == ID);
                if (myCustomer != null)
                {
                    return new CustomerDTO(myCustomer);
                }
                else return null;
            }
            catch(Exception e)
            {
                return null;
                throw e;
            }
        }
    }
}