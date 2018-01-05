using EMG.Interface;
using EMG.Model;
using EMG.Model.ViewModel.News;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EMG.Controllers
{
    public class NewsApiController : ApiController
    {
        private INewsServices _newsServices;

        public NewsApiController(INewsServices newsServices)
        {
            this._newsServices = newsServices;
        }

        #region 取得全部資料

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<NewsView> GetAll()
        {
            return _newsServices.GetAll();
        }

        #endregion

        #region 新增新聞

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public void Create(NewsView newData)
        {
            _newsServices.Create(newData);
        }

        #endregion

        #region 修改新聞

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public void Edit(NewsView nowData)
        {
            _newsServices.Edit(nowData);
        }

        #endregion

        #region 刪除新聞

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public void Delete(PostId Id)
        {
            _newsServices.Delete(Id.N_Id);
        }

        #endregion
    }
}