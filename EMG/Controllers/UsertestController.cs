using AutoMapper;
using EMG.Interface;
using EMG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMG.Controllers
{
    public class UsertestController : Controller
    {
        private IUsertestServices _usertestservices;
        public UsertestController(IUsertestServices usertestservices)
        {
            this._usertestservices = usertestservices;
        }

        #region 註冊
        public ActionResult Registertest()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = "請選擇性別", Value = "", Selected = true });
            list.Add(new SelectListItem() { Text = "男生", Value = "男生" });
            list.Add(new SelectListItem() { Text = "女生", Value = "女生" });
            ViewBag.SexList = list;
            return View();
        }
        [HttpPost]
        public ActionResult Registertest(Register2View RegisterUser)
        {
            Usertest newUser = Mapper.Map<Usertest>(RegisterUser);
            _usertestservices.Register(newUser);
            return RedirectToAction("Index", "Home");
        }

        #endregion


    }
}