using EMG.Interface;
using EMG.Model;
using EMG.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EMG.Controllers
{
    public class AlbumApiController : ApiController
    {
        private IAlbumServices _AlbumServices;

        public AlbumApiController(IAlbumServices albumServices)
        {
            this._AlbumServices = albumServices;
        }

        #region 新增相片
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public void UploadPhoto(HttpPostedFile File)
        {
            _AlbumServices.UploadPhoto(File);
        }

        #endregion

        #region 取得前五名
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "GET,OPTIONS")]
        public IEnumerable<PhotoListView> getRank()
        {
            return _AlbumServices.getRank();
        }
        #endregion

        #region 取得個人全部相片
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public IEnumerable<Photo> GetPersonPhoto(AlbumView data)
        {
            return _AlbumServices.GetPersonPhoto(data.account);
        }

        #endregion

        #region 刪除照片
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public void DeletePhoto(AlbumView data)
        {
            _AlbumServices.DeletePhoto(data.p_Id);
        }
            
        #endregion


    }
}