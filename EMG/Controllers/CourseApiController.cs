using EMG.Interface;
using EMG.Model;
using EMG.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EMG.Controllers
{
    public class CourseApiController : ApiController
    {
        private ICourseServices _courseServices;

        public CourseApiController(ICourseServices courseServices)
        {
            this._courseServices = courseServices;
        }

        #region 是否有課程
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public IEnumerable<Account_Course> hasCourse()
        {
            return _courseServices.hasCourse(User.Identity.Name);
        }
        #endregion

        #region 是否有課程
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public IEnumerable<Account_Course> getCourse()
        {
            return _courseServices.getCourse(User.Identity.Name);
        }
        #endregion

        #region 取得熱門課程
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "GET,OPTIONS")]
        public IEnumerable<Course> getPopular()
        {
            return _courseServices.getPopular();
        }
        #endregion

        #region 取得主題課程
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "GET,OPTIONS")]
        public IEnumerable<Course> getTheme()
        {
            return _courseServices.getTheme();
        }
        #endregion

        #region 取得減脂課程
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "GET,OPTIONS")]
        public IEnumerable<Course> getBodyfat()
        {
            return _courseServices.getBodyfat();
        }
        #endregion

        #region 取得相同性別、歲數群組課程
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public IEnumerable<Course> getAge(PostId data)
        {
            return _courseServices.getAge(data.account);
        }
        #endregion

        #region 取得相同運動項目課程
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public IEnumerable<Course> getSports(PostId data)
        {
            return _courseServices.getSports(data.account);
        }
        #endregion

        #region 取得注重相同肌肉部位課程
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public IEnumerable<Course> getPlace(PostId data)
        {
            return _courseServices.getPlace(data.account);
        }
        #endregion

        #region 取得注重相同肌肉強度
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public IEnumerable<Course> getMotivation(PostId data)
        {
            return _courseServices.getMotivation(data.account);
        }
        #endregion

        #region 取得完成相同課程
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public IEnumerable<Course> getSame(PostId data)
        {
            return _courseServices.getSame(data.account);
        }
        #endregion

        #region 取得關注課程
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public IEnumerable<Course> getWatch(PostId data)
        {
            return _courseServices.getWatch(data.account);
        }
        #endregion

        #region 產生一週課程
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public IEnumerable<Course> CreateCourse(CourseSelectView data)
        {
            if (data.account == null || data.account == "")
                data.account = User.Identity.Name;
            return _courseServices.CreateCourse(data);
        }
        #endregion

        #region 儲存課程
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public IEnumerable<Account_Course> SaveCourse(SaveCourseView data)
        {
            if (data.account == null || data.account == "")
                data.account = User.Identity.Name;
            return _courseServices.SaveCourse(data);
        }
        #endregion

        #region 取得姿勢介紹課程
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public Posture getPosData(GetPosView data)
        {
            return _courseServices.getPosData(data.Name);
        }
        #endregion

        
     
        #region APP

        #region 是否有課程
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public IEnumerable<Account_Course> hasCourseApp(PostId data)
        {
            return _courseServices.hasCourse(data.account);
        }
        #endregion

        #region 取得最近課程
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public IEnumerable<Account_Course> getOneCourse(PostId data)
        {
            return _courseServices.getOneCourse(data.account);
        }
        #endregion

        #region 取得課程列表
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public IEnumerable<Account_Course> getCourseList(PostId data)
        {
            return _courseServices.getCourseList(data.account);
        }
        #endregion

        #region 取得課程內容
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public IEnumerable<Account_Course> getCourseDetail(Account_Course data)
        {
            return _courseServices.getCourseDetail(data.Account, data.C_Id, data.Date);
        }
        #endregion

        #region 取得姿勢介紹課程
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public Posture getPosDataAPP(Posture data)
        {
            return _courseServices.getPosData(data.Name);
        }
        #endregion

        #region 產生一週課程
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public IEnumerable<Course> CreateCourseAPP(CourseSelectView data)
        {
            if (data.account == null || data.account == "")
                data.account = User.Identity.Name;
            return _courseServices.CreateCourseAPP(data);
        }
        #endregion

        #region 刪除課程
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public void DeleteCourseAPP(Course data)
        {
            _courseServices.DeleteCourseAPP(data.C_Id);
        }
        #endregion

        #region 儲存課程
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public IEnumerable<Account_Course> SaveCourseAPP(SaveCourseViewAPP data)
        {
            if (data.account == null || data.account == "")
                data.account = User.Identity.Name;
            return _courseServices.SaveCourseAPP(data);
        }
        #endregion

        #region 儲存重量
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public void savePower(Account_Course data)
        {
            _courseServices.savePower(data.Account, data.C_Id, data.P_Name, data.Power);
        }
        #endregion

        #region 訓練結束
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public void EndTrain(Account_Course data)
        {
            _courseServices.endTrain(data);
        }
        #endregion

        #endregion
    }
}
