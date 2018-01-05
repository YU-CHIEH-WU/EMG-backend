using AutoMapper;
using EMG.Interface;
using EMG.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMG.Services
{
    public class MessageServices : IMessageServices
    {
        private IUnitOfWork _db;
        private IRepository<Message> _repository;
        private IRepository<Reply> _replyrepository;

        public MessageServices(IUnitOfWork uow, IRepository<Message> repository, IRepository<Reply> replyrepository)
        {
            this._db = uow;
            this._repository = repository;
            this._replyrepository = replyrepository;
        }

        #region 查詢相關
        #region 查詢一筆文章
        public Message GetDataById(int mId)
        {
            return _repository.Get(p => p.MId == mId);
        }
        #endregion
        #region 查詢文章陣列資料
        public List<Message> GetDataList(ForPaging paging, string search)
        {
            //設定要接受全部搜尋資料的物件
            IQueryable<Message> searchData;
            if (String.IsNullOrEmpty(search))
            {
                searchData = GetAllDataList(paging);
            }
            else
            {
                searchData = GetAllDataList(paging, search);
            }
            return searchData.OrderByDescending(p => p.MId)
                .Skip((paging.NowPage - 1) * paging.ItemNum)
                .Take(paging.ItemNum).ToList();
        }
        //無搜尋值的搜尋資料方法
        public IQueryable<Message> GetAllDataList(ForPaging paging)
        {
            IQueryable<Message> data = _repository.GetAll();
            paging.MaxPage = Convert.ToInt32(Math.Ceiling(
                Convert.ToDouble(data.Count()) / paging.ItemNum));
            paging.SetRightPage();
            return data;
        }
        //包含搜尋值的搜尋資料方法
        public IQueryable<Message> GetAllDataList(ForPaging paging, string search)
        {
            IQueryable<Message> data = _repository.GetAll()
                .Where(p => p.Title.Contains(search) ||
                    p.Messages.Contains(search));
            paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(data.Count()) /
                paging.ItemNum));
            paging.SetRightPage();
            return data;
        }

        #endregion
        #endregion

        #region 取得首頁
        public List<Message>GetIndex()
        {
            var data = _repository.GetAll().ToList();
            return data;
        }
        #endregion

        #region 新增留言
        public void Insert(Message data)
        {
            _repository.Create(data);
            _db.Save();
        }
        #endregion

        #region 修改檢查留言
        public bool CheckUpdate(int mId)
        {
            Message data = GetDataById(mId);
            return (data != null);
        }
        #endregion

        #region 修改留言
        public void Update(Message data)
        {
            _repository.Update(data);
            _db.Save();
        }
        #endregion

        #region 刪除文章
        public void Delete(Message data)
        {           
            _repository.Delete(data);
            _db.Save();
        }
        #endregion

        #region Api

        #region 取得全部資料

        public IEnumerable<MessageView> GetAll()
        {
            var messageList = _repository.GetAll().ToList().OrderByDescending(p => p.MId);
            List<MessageView> messageListView = Mapper.Map<List<MessageView>>(messageList);
            return messageListView;
        }

        #endregion

        #region 取得帳號全部資料

        public IEnumerable<MessageView> GetAllByAccount(string account)
        {
            var messageList = _repository.GetAll().Where(p => p.Account == account).ToList().OrderByDescending(p => p.MId);
            List<MessageView> messageListView = Mapper.Map<List<MessageView>>(messageList);
            return messageListView;
        }

        #endregion

        #region 取得一筆資料

        public MessageView GetOne(string MId)
        {
            int data = Convert.ToInt32(MId);
            var message = _repository.GetAll().Where(p => p.MId == data).FirstOrDefault();
            MessageView messageView = Mapper.Map<MessageView>(message);
            return messageView;
        }

        #endregion

        #region 新增留言

        public void CreateApi(MessageView newData)
        {
            newData.CreateTime = DateTime.Now;
            Message data = Mapper.Map<Message>(newData);
            _repository.Create(data);
            _db.Save();
        }

        #endregion

        #region 修改留言

        public void EditApi(MessageView newData)
        {
            var data = GetDataById(newData.MId);
            data.Title = newData.Title;
            data.Messages = newData.Messages;
            data.CreateTime = DateTime.Now;
            Update(data);
            _db.Save();
        }

        #endregion

        #region 刪除留言

        public void DeleteApi(string MId)
        {
            int mid = Convert.ToInt32(MId);
            Message data = GetDataById(mid);
            List<Reply> replyList = data.Reply.ToList();
            foreach(Reply Data in replyList)
            {
                _replyrepository.Delete(Data);
                _db.Save();
            }
            Delete(data);
            _db.Save();
        }

        #endregion

        #endregion
    }
}