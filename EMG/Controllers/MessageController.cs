using AutoMapper;
using EMG.Interface;
using EMG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace EMG.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private IMessageServices _messageServices;
        private IUserServices _userServices;
        private IReplyServices _replyServices;
        public MessageController(IMessageServices messageServices, IUserServices userServices, IReplyServices replyServices)
        {
            this._messageServices = messageServices;
            this._userServices = userServices;
            this._replyServices = replyServices;
        }


        #region 開始頁面
        public ActionResult Index(string search, int page = 1)
        {
            //取得首頁資料
            MessageListView messageList = new MessageListView();
            messageList.Search = search;
            messageList.Paging = new ForPaging(page);
            messageList.MessageList = new List<MessageView>();
            List<Message> data = _messageServices.GetDataList(messageList.Paging, messageList.Search);
            messageList.MessageList = Mapper.Map<List<MessageView>>(data);
            foreach (var item in messageList.MessageList)
            {
                item.UserName = _userServices.GetNameByAccount(item.Account);
            }
            return  PartialView(messageList);        
        }
        #region 搜尋
        [HttpPost]
        //使用Bind的Inculde來定義指接受的欄位，用來避免傳入其他不相干值
        public ActionResult Index([Bind(Include = "Search")]MessageListView data)
        {
            return RedirectToAction("Index", new { Search = data.Search });
        }
        #endregion
        #endregion

        #region 新增留言
        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(MessageCreateView newData)
        {
            var account = _userServices.GetAccount(User.Identity.Name);
            var photo = _userServices.Get(account).Url;
            newData.Account = User.Identity.Name;
            newData.Photo = photo;
            newData.CreateTime = DateTime.Now;
            Message data = Mapper.Map<Message>(newData);
            _messageServices.Insert(data);
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region 修改留言
        [Authorize]
        public ActionResult Edit(int mId)
        {   
            Message data = _messageServices.GetDataById(mId);
            MessageView nowData = Mapper.Map<MessageView>(data);
            return PartialView(nowData);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Edit(MessageView nowData)
        {
            nowData.Account = User.Identity.Name;
            nowData.CreateTime = DateTime.Now;
            var data = Mapper.Map<Message>(nowData);
            _messageServices.Update(data);
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region 刪除留言
        [Authorize]
        public ActionResult Delete(int mId,ForPaging paging, Reply Data)
        {
            Message data = _messageServices.GetDataById(mId);
            List<Reply> replyList = data.Reply.ToList();
            foreach(Reply delete in replyList)
            {
                _replyServices.Delete(delete);
            }       
            _messageServices.Delete(data);
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}