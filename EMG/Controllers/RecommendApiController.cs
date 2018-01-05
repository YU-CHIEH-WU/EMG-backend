using EMG.Interface;
using EMG.Model;
using EMG.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EMG.Controllers
{
    public class RecommendApiController : ApiController
    {
        private ICourseServices _courseServices;

        public RecommendApiController(ICourseServices courseServices)
        {
            this._courseServices = courseServices;
        }

        //#region 取得推薦課程
        //[HttpGet]
        //[EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        //public IEnumerable<Course> getCourse()
        //{
        //    //return _courseServices.getRecommend(User.Identity.Name);
        //}
        //#endregion
    }
}