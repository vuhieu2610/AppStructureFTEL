using AppOutSideAPI.Business;
using EntityData;
using EntityData.Common;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AppOutSideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase, IDisposable
    {
        #region initial
        private UserBusiness _bus = null;
        private UserBusiness Business
        {
            get
            {
                if (_bus == null)
                {
                    _bus = new UserBusiness();
                }
                return _bus;
            }
        }
        public void Dispose()
        {
            Business.Dispose();
        }
        #endregion


        #region http
        [HttpPost]
        public ActionResult<ReturnResult<Users>> GetPaging(BaseCondition condition)
        {
            return Business.GetPaging(condition);

        }

        [HttpPost]
        public ActionResult<ReturnResult<Users>> GetSingle(Users item)
        {
            return Business.GetSingle(item);
        }
        [HttpPost]
        public ActionResult<ReturnResult<Users>> InsertSingle(Users item)
        {
            return Business.InsertSingle(item);
        }
        [HttpPost]
        public ActionResult<ReturnResult<Users>> UpdateSingle(Users item)
        {
            return Business.UpdateSingle(item);
        }

        [HttpPost]
        public ActionResult<ReturnResult<Users>> Delete(Users item)
        {
            return Business.Delete(item);
        }
        #endregion

        

    }
}