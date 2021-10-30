using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using API.Models.DAO;

namespace API.Controllers
{
    public class BrandController : ApiController
    {
        [Route("Api/BrandController/GetAllBrand")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllBrand()
        {
            return Ok(await BrandDAO.Instance.GetAllBrand());
        }
    }
}
