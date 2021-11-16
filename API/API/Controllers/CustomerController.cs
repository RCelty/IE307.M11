using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using API.Models.DAO;
using API.Models.DTO;

namespace API.Controllers
{
    public class CustomerController : ApiController
    {
        [Route("Api/CustomerController/GetAllCustomer")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllCategory()
        {
            return Ok(await CustomerDAO.Instance.GetAllCustomer());
        }

        [Route("Api/CustomerController/Login")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> AddCategory(CustomerDTO customerDTO)
        {
            return Ok(await CustomerDAO.Instance.Login(customerDTO));
        }

       
    }
}
