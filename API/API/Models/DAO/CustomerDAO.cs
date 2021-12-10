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
            catch (Exception e)
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
            catch (Exception e)
            {
                return null;
                throw e;
            }
        }

        public async Task<CustomerDTO> GetCustomerByUserName(string userName)
        {
            try
            {
                var myCustomer = await db.Customers.SingleOrDefaultAsync(customer => customer.UserName == userName);
                if (myCustomer != null)
                {
                    return new CustomerDTO(myCustomer);
                }
                else return null;
            }
            catch (Exception e)
            {
                return null;
                throw e;
            }
        }

        public async Task<int> IsRegisterAble(CustomerDTO customerDTO)
        {
            if (await db.Customers.SingleOrDefaultAsync(c => c.UserName == customerDTO.UserName || c.Email == customerDTO.Email) != null)
            {
                return -1; //Tên đăng nhập hoặc email đã tồn tại
            }
            return await Const.VerifyEmail(customerDTO.Email) ? 1 : -2; //1: có thể đăng ký; -2: email không hợp lệ
        }

        public async Task<int> Register(CustomerDTO customerDTO)
        {
            try
            {
                string passWord = Const.CreateMD5(customerDTO.PassWord);

                Customer customer = new Customer()
                {
                    UserName = customerDTO.UserName,
                    PassWord = passWord,
                    DisplayName = customerDTO.DisplayName,
                    PhoneNumber = customerDTO.PhoneNumber,
                    Email = customerDTO.Email,
                    Address = customerDTO.Address,
                    IsAdmin = false
                };
                if (string.IsNullOrEmpty(customerDTO.Avatar))
                {
                    customer.Avatar = "default.jpg";
                }
                else
                {
                    customer.Avatar = customerDTO.Avatar;
                }

                db.Customers.Add(customer);
                await db.SaveChangesAsync();

                return customer.ID;
            }
            catch (Exception e)
            {
                return -1;
                throw e;
            }
        }

        public async Task<string> SendOTP(CustomerDTO customerDTO)
        {
            Task<string> task = new Task<string>(new Func<string>(() =>
            {
                try
                {
                    string OTP = new Random().Next(1000, 10000).ToString();
                    var body = "";

                    body += "<hr/>";
                    body += "Xin chào <b>" + customerDTO.DisplayName + "</b>,<br/><br/>";

                    body += "Mã xác thực OTP Chotech của bạn là: <b>" + OTP + "</b><br/><br/>";
                    body += "Vui lòng không cung cấp mã OTP cho bất kì ai khác.<br/><br/>";
                    body += "Cảm ơn đã đăng ký,<br/><br/>";
                    body += "-----------------------------<br/>";
                    body += "<b>D.A Nguyen - Leader</b><br/>";
                    body += "<b>Phone:</b> (84) 00 7143 619<br/>";
                    body += "<b>Facebook: </b><a href=" + "https://www.facebook.com/duyanh.nguyenngoc.14/" + ">https://www.facebook.com/duyanh.nguyenngoc.14/<a/>";
                    body += "<hr/>";

                    var result = Const.SendMail(customerDTO.Email, "Xác thực Email - ChoTech", body);
                    if (result)
                        return OTP;
                    else
                        return "";

                }
                catch (Exception e)
                {
                    return "";
                    throw e;
                }
            }));

            task.Start();

            return await task;
        }

        public async Task<bool> DeleteCustomer(int ID)
        {
            var result = db.Customers.SingleOrDefault(b => b.ID == ID);

            try
            {
                db.Customers.Remove(result);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

        public async Task<bool> UpdateCustomer(CustomerDTO customerDTO)
        {
            var result = db.Customers.SingleOrDefault(c => c.ID == customerDTO.ID);
            try
            {
                if (!string.IsNullOrWhiteSpace(customerDTO.DisplayName))
                    result.DisplayName = customerDTO.DisplayName;
                if (!string.IsNullOrWhiteSpace(customerDTO.PassWord))
                    result.PassWord = Const.CreateMD5(customerDTO.PassWord);
                if (!string.IsNullOrWhiteSpace(customerDTO.Address))
                    result.Address = customerDTO.Address;
                if (!string.IsNullOrWhiteSpace(customerDTO.PhoneNumber))
                    result.PhoneNumber = customerDTO.PhoneNumber;
                if (!string.IsNullOrWhiteSpace(customerDTO.Avatar))
                    result.Avatar = customerDTO.Avatar;

                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

        public async Task<bool> ChangeCustomerRole(int ID)
        {
            var result = db.Customers.SingleOrDefault(c => c.ID == ID);
            try
            {
                result.IsAdmin = !result.IsAdmin;
                await db.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                return false;
                throw e;
            }
        }
    }
}