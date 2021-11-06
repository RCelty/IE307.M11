﻿using System;
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
    public class CategoryController : ApiController
    {
        [Route("Api/CategoryController/GetAllCategory")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllCategory()
        {
            return Ok(await CategoryDAO.Instance.GetAllCategory());
        }

        [Route("Api/CategoryController/AddCategory")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> AddCategory(CategoryDTO categoryDTO)
        {
            return Ok(await CategoryDAO.Instance.AddCategory(categoryDTO));
        }

        [Route("Api/CategoryController/UpdateCategory")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateCategory(CategoryDTO categoryDTO)
        {
            return Ok(await CategoryDAO.Instance.UpdateCategory(categoryDTO));
        }

        [Route("Api/CategoryController/DeleteCategory")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteCategory(CategoryDTO categoryDTO)
        {
            return Ok(await CategoryDAO.Instance.DeleteCategory(categoryDTO));
        }

        [Route("Api/CategoryController/RestoreAllCategory")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> RestoreAllCategory()
        {
            return Ok(await CategoryDAO.Instance.RestoreAllCategory());
        }
    }
}
