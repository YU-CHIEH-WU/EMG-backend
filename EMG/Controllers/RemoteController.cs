using EMG.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMG.Controllers
{
    public class RemoteController : Controller
    {
        private IUserServices _userservice;

        public RemoteController(IUserServices userservice)
        {
            this._userservice = userservice;
        }
        public JsonResult CheckAccount(string account)
        {
            bool isValid = _userservice.AccountCheck(account);
            return Json(isValid, JsonRequestBehavior.AllowGet);
        }
    }
}