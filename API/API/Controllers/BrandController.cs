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
using API.Models.DTO;

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

        [Route("Api/BrandController/AddBrand")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> AddBrand(BrandDTO brandDTO)
        {
            return Ok(await BrandDAO.Instance.AddBrand(brandDTO));
        }

        [Route("Api/BrandController/UpdateBrand")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateBrand(BrandDTO brandDTO)
        {
            return Ok(await BrandDAO.Instance.UpdateBrand(brandDTO));
        }

        [Route("Api/BrandController/DeleteBrand")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteBrand(BrandDTO brandDTO)
        {
            return Ok(await BrandDAO.Instance.DeleteBrand(brandDTO));
        }

        [Route("Api/BrandController/RestoreAllBrand")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> RestoreAllBrand()
        {
            return Ok(await BrandDAO.Instance.RestoreAllBrand());
        }
    }
}
