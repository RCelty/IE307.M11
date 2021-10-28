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
    public class AdvertisementController : ApiController
    {
        [Route("Api/AdvertisementController/GetAllAdvertisement")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllAdvertisement()
        {
            return Ok(await AdvertisementDAO.Instance.GetAllAdvertisement());
        }
    }
}
