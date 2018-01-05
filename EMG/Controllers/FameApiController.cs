using EMG.Interface;
using EMG.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EMG.Controllers
{
    public class FameApiController : ApiController
    {
        private IFameServices _fameServices;

        public FameApiController(IFameServices fameServices)
        {
            this._fameServices = fameServices;
        }

        #region 取得名人榜
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public IEnumerable<FameView> getFame()
        {
            return _fameServices.getFame();
        }
        #endregion
    }
}
