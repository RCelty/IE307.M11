using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using API.Assets.Contain;
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


        [Route("Api/BrandController/UploadBrandImage")]
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage UploadBrandImage()
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count < 1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            foreach (string file in httpRequest.Files)
            {
                var postedFile = httpRequest.Files[file];
                var filePath = HttpContext.Current.Server.MapPath("~/Assets/Images/Brand/" + postedFile.FileName);
                postedFile.SaveAs(filePath);
            }

            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}
