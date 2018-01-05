using EMG.Interface;
using EMG.Model;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EMG.Controllers
{
    public class ChartApiController : ApiController
    {
        private IChartServices _chartServices;

        public ChartApiController(IChartServices chartServices)
        {
            this._chartServices = chartServices;
        }

        #region WEB

        #region 訓練成效
        #endregion

        #region 肌肉成長
        #endregion

        #region 訓練姿勢之相對訓練成效
        #endregion

        #region 肌肉疲勞
        #endregion 

        #region 出力百分比
        #endregion

        #region 肌肉部位之相對肌肉成長
        #endregion

        #endregion

        #region APP

        #region 取得訓練日期動作
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public List<Account_Course> getAccountCourseAPP(Account_Course data)
        {
            return _chartServices.getAccountCourseAPP(data.Account);
        }
        #endregion

        #region 取得訓練成效
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public List<EMGData> getEMGDataAPP(Account_Course data)
        {
            return _chartServices.getEMGDataAPP(data.Account, data.Date, data.Id);
        }
        #endregion

        #region 取得肌力
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public List<RecordEffect> getEffectDataAPP(GrowView data)
        {
            return _chartServices.getEffectDataAPP(data.account, data.startDate, data.endDate);
        }
        #endregion

        public class GrowView
        {
            public string account { get; set; }
            public DateTime startDate { get; set; }
            public DateTime endDate { get; set; }
        }

        #endregion
    }
}
