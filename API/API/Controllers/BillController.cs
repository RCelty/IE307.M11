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
    public class BillController : ApiController
    {
        [Route("Api/BillController/GetAllBill")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllBill()
        {
            return Ok(await BillDAO.Instance.GetAllBill());
        }

        [Route("Api/BillController/CreateBill")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> CreateBill(BillDTO billDTO)
        {
            return Ok(await BillDAO.Instance.CreateBill(billDTO));
        }

        [Route("Api/BillController/GetAllBillByCustomerID/{ID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllBillByCustomerID(int ID)
        {
            return Ok(await BillDAO.Instance.GetAllBillByCustomerID(ID));
        }

        [Route("Api/BillController/GetAllBillDetail")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllBillDetail()
        {
            return Ok(await BillDetailDAO.Instance.GetAllBillDetail());
        }

        [Route("Api/BillController/GetAllBillDetailByBillID/{ID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllBillDetailByBillID(int ID)
        {
            return Ok(await BillDetailDAO.Instance.GetAllBillDetailByBillID(ID));
        }

        [Route("Api/BillController/GetAllBillDetailByProductID/{ID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllBillDetailByProductID(int ID)
        {
            return Ok(await BillDetailDAO.Instance.GetAllBillDetailByProductID(ID));
        }

        [Route("Api/BillController/AddBillDetail")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> AddBillDetail(BillDetailDTO billDetailDTO)
        {
            return Ok(await BillDetailDAO.Instance.AddBillDetail(billDetailDTO));
        }

        [Route("Api/BillController/DeleteBillDetail")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteBillDetail(BillDetailDTO billDetailDTO)
        {
            return Ok(await BillDetailDAO.Instance.DeleteBillDetail(billDetailDTO));
        }

        [Route("Api/BillController/DeleteBillDetailByID/{ID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> DeleteBillDetailByID(int ID)
        {
            return Ok(await BillDetailDAO.Instance.DeleteBillDetailByID(ID));
        }
    }
}
