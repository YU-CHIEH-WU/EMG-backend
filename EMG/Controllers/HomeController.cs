using EMG.Interface;
using EMG.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMG.Controllers
{
    public class HomeController : Controller
    {
        private ICourseServices _courseServices;

        public HomeController(ICourseServices courseServices)
        {
            this._courseServices = courseServices;
        }
        //總覽
        public ActionResult Index()
        {          
            return View();
        }

        //首頁
        public ActionResult Front()
        {
            return View();
        }

        //個人頁面
        public ActionResult Board()
        {
            BoardView bv = new BoardView();
            return View(bv);
        }
    }
}