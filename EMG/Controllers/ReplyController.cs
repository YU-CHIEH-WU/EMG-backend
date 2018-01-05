using AutoMapper;
using EMG.Interface;
using EMG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMG.Controllers
{
    [Authorize]
    public class ReplyController : Controller
    {
        private IUserServices _userServices;
        private IMessageServices _messageServices;
        private IReplyServices _replyServices;
        public ReplyController(IUserServices userServices, IMessageServices messageServices, IReplyServices replyServices)
        {
            this._replyServices = replyServices;
            this._messageServices = messageServices;
            this._userServices = userServices;
        }


        #region 開始頁面
        public ActionResult Index(int mId, string Identity, string Role)
        {
            ViewData["M_Id"] = mId;
            return PartialView();
        }
        #endregion

        #region 回覆陣列
        public ActionResult DataList(int mId, string Identity, string Role, int page = 1)
        {
            ReplyListView data = new ReplyListView();
            data.Paging = new ForPaging(page);
            data.mId = mId;
            data.Identity = Identity;
            data.Role = Role;
            data.DataList = _replyServices.GetDataList(data.Paging, data.mId);
            return PartialView(data);
        }
        #endregion

        #region 新增回覆
        public ActionResult Create(int mId)
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(ReplyCreatView newData, int mId)
        {
            var account = _userServices.GetAccount(User.Identity.Name);
            var photo = _userServices.Get(account).Url;
            newData.Account = User.Identity.Name;
            newData.Photo = photo;
            newData.mId = mId;
            newData.ReplyTime = DateTime.Now;
            Reply data = Mapper.Map<Reply>(newData);
            _replyServices.Insert(data);
            return RedirectToAction("Index", "Message");
        }
        #endregion

        #region 修改回覆
        public ActionResult Edit(int rId)
        {
            Reply data = _replyServices.GetDataById(rId);
            ReplyView nowData = Mapper.Map<ReplyView>(data);
            return PartialView(nowData);
        }
        [HttpPost]
        public ActionResult Edit(ReplyView nowData)
        {
            nowData.Account = User.Identity.Name;
            nowData.ReplyTime = DateTime.Now;
            var data = Mapper.Map<Reply>(nowData);
            _replyServices.Update(data);
            return RedirectToAction("Index", "Message");
        }
        #endregion

        #region 刪除回復
        public ActionResult Delete(int rId)
        {
            Reply data = _replyServices.GetDataById(rId);
            _replyServices.Delete(data);
            return RedirectToAction("Index", "Message");
        }
        #endregion
    }
}