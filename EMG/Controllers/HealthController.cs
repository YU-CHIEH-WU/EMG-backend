using EMG.Interface;
using EMG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMG.Controllers
{
    public class HealthController : Controller
    {
        private IUserServices _userservices;
        private ITextServices _textservices;
        public HealthController(IUserServices userservices, ITextServices textservices)
        {
            this._userservices = userservices;
            this._textservices = textservices;
        }

        #region 前台
        public ActionResult Front(string Title, Text Data)
        {
            if (Title == null)
            {
                return RedirectToAction("Front", "Home");
            }
            var data = _textservices.GetDataByTitle(Title,Data);
            return View(data);
        }
        #endregion

        #region 開始頁面
        public ActionResult Index(string title, Text Data)
        {
            if (title == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var data = _textservices.GetDataByTitle(title, Data);
            return View(data);
        }
        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(Text data, string title)
        {
            data.Title = title;
            if(_textservices.Update(data))
            {
                ViewBag.result = "修改成功!";
            }
            else
            {
                ViewBag.result = "修改失敗!";
            }
            return View(data);
        }
        #endregion
    }
}