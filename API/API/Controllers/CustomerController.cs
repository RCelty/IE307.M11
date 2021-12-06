﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
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
        public async Task<IHttpActionResult> GetAllCustomer()
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

        [Route("Api/CustomerController/GetCustomerByID/{ID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetCustomerByID(int ID)
        {
            return Ok(await CustomerDAO.Instance.GetCustomerByID(ID));
        }

        [Route("Api/CustomerController/GetFavoriteProductByCustomerID/{ID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetFavoriteProductByCustomerID(int ID)
        {
            return Ok(await FavoriteProductDAO.Instance.GetFavoriteProductByCustomerID(ID));
        }

        [Route("Api/CustomerController/AddFavoriteProduct")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> AddFavoriteProduct(FavoriteProductDTO favoriteProductDTO)
        {
            return Ok(await FavoriteProductDAO.Instance.AddFavoriteProduct(favoriteProductDTO));
        }

        [Route("Api/CustomerController/DeleteFavoriteProduct")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteFavoriteProduct(FavoriteProductDTO favoriteProductDTO)
        {
            return Ok(await FavoriteProductDAO.Instance.DeleteFavoriteProduct(favoriteProductDTO));
        }

        [Route("Api/CustomerController/DeleteFavoriteProductByID/{ID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> DeleteFavoriteProductByID(int ID)
        {
            return Ok(await FavoriteProductDAO.Instance.DeleteFavoriteProductByID(ID));
        }

        [Route("Api/CustomerController/UploadImage")]
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage UploadImage()
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count < 1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            foreach (string file in httpRequest.Files)
            {
                var postedFile = httpRequest.Files[file];
                var filePath = HttpContext.Current.Server.MapPath("~/Assets/Images/Customer/" + postedFile.FileName);
                postedFile.SaveAs(filePath);
            }

            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}
