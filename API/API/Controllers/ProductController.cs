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
    public class ProductController : ApiController
    {
        [Route("Api/ProductController/GetAllProduct")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllProduct()
        {
            return Ok(await ProductDAO.Instance.GetAllProduct());
        }

        //[Route("Api/ProductController/CreateProductDetail")]
        //[AllowAnonymous]
        //[HttpPost]
        //public ProductDetailDTO CreateProductDetail(ProductDetailDTO productDetailDTO)
        //{
        //    productDetailDTO.DetailDescription += "delete";
        //    return productDetailDTO;
        //}
    }
}
