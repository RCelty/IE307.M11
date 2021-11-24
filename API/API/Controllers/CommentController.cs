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
    public class CommentController : ApiController
    {
        [Route("Api/CommentController/GetAllComment")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllComment()
        {
            return Ok(await CommentDAO.Instance.GetAllComment());
        }

        [Route("Api/CommentController/AddComment")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> AddComment(CommentDTO commentDTO)
        {
            return Ok(await CommentDAO.Instance.AddComment(commentDTO));
        }

        [Route("Api/CommentController/UpdateComment")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateComment(CommentDTO commentDTO)
        {
            return Ok(await CommentDAO.Instance.UpdateComment(commentDTO));
        }

        [Route("Api/CommentController/DeleteComment/{ID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> DeleteComment(int ID)
        {
            return Ok(await CommentDAO.Instance.DeleteComment(ID));
        }

        [Route("Api/CommentController/GetCommentByProductID/{ID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetCommentByProductID(int ID)
        {
            return Ok(await CommentDAO.Instance.GetCommentByProductID(ID));
        }
    }
}
