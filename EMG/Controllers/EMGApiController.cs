using EMG.Interface;
using EMG.Model;
using EMG.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace EMG.Controllers
{
    public class EMGApiController : ApiController
    {
        private IEMGServices _emgServices;
        private IUserServices _userServices;

        public EMGApiController(IEMGServices emgservice, IUserServices userservice)
        {
            this._emgServices = emgservice;
            this._userServices = userservice;
        }

        #region 回傳

        [HttpPost]
        public string PostSensor(EMGView data)
        {
            _emgServices.GetData(data);
            return "true";
        }

        #endregion

        #region 儲存放鬆出力檢測值
        [HttpPost]
        public void saveRest_Max(Rest_Max data)
        {
            _emgServices.saveRest_Max(data);
        }
        #endregion

    }
}