using EMG.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMG.Controllers
{
    public class EMGController : Controller
    {
        private IEMGServices _emgServices;
        public EMGController(IEMGServices emgServices)
        {
            this._emgServices = emgServices;
        }

        // GET: EMG
        public ActionResult Index()
        {
            var data = _emgServices.getOutput();
            return View(data);
        }
    }
}