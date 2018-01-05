using EMG.Interface;
using EMG.Model;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EMG.Controllers
{
    public class ReplyApiController : ApiController
    {
        private IUserServices _userServices;
        private IMessageServices _messageServices;
        private IReplyServices _replyServices;
        public ReplyApiController(IUserServices userServices, IMessageServices messageServices, IReplyServices replyServices)
        {
            this._replyServices = replyServices;
            this._messageServices = messageServices;
            this._userServices = userServices;
        }

        #region 取得全部資料

        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public IEnumerable<ReplyView> GetAll(GetMidView data)
        {
            return _replyServices.GetAll(data.MId);
        }

        #endregion

        //#region 取得一筆資料

        //[HttpPost]
        //public MessageView GetOne(PostMId MId)
        //{
        //    return _messageservice.GetOne(MId.MId);
        //}

        //#endregion

        #region 新增留言
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public void Create(ReplyView newData)
        {
            _replyServices.CreateApi(newData);
        }

        #endregion

        //#region 修改留言

        //public void Edit(MessageView newData)
        //{
        //    _messageservice.EditApi(newData);
        //}

        //#endregion

        #region 刪除回覆
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        [HttpPost]
        public void Delete(PostId Id)
        {
            _replyServices.DeleteApi(Id.RId);
        }

        #endregion

        //#endregion
    }
}
