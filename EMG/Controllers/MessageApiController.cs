using EMG.Interface;
using EMG.Model;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EMG.Controllers
{
    public class MessageApiController : ApiController
    {
        private IMessageServices _messageservice;

        public MessageApiController(IMessageServices messageservice)
        {
            this._messageservice = messageservice;
        }

        #region 取得全部資料
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public IEnumerable<MessageView> GetAll()
        {
            return _messageservice.GetAll();
        }

        #endregion

        #region 取得全部資料
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public IEnumerable<MessageView> GetAllByAccount(PostId data)
        {
            return _messageservice.GetAllByAccount(data.account);
        }

        #endregion

        #region 取得一筆資料
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public MessageView GetOne(PostId Id)
        {
            return _messageservice.GetOne(Id.MId);
        }

        #endregion      

        #region 新增留言
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public void Create(MessageView newData)
        {
            _messageservice.CreateApi(newData);
        }

        #endregion

        #region 修改留言
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public void Edit(MessageView newData)
        {
            _messageservice.EditApi(newData);
        }

        #endregion

        #region 刪除
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public void Delete(PostId Id)
        {
            _messageservice.DeleteApi(Id.MId);
        }

        #endregion


    }
}
