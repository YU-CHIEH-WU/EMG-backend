using AutoMapper;
using EMG.Interface;
using EMG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMG.Services
{
    public class ReplyServices : IReplyServices
    {
        private IUnitOfWork _db;
        private IRepository<Reply> _repository;

        public ReplyServices(IUnitOfWork uow, IRepository<Reply> repository)
        {
            this._db = uow;
            this._repository = repository;
        }

        #region 查詢相關
        #region 查詢一筆留言
        public Reply GetDataById(int rId)
        {
            return _repository.Get(p => p.RId == rId);
        }
        #endregion

        #region 查詢回覆陣列
        public List<Reply> GetDataList(ForPaging paging, int mId)
        {
            IQueryable<Reply> SearchData = GetAllDataList(paging, mId);
            return SearchData.OrderByDescending(p => p.MId).Skip((paging.NowPage - 1) * paging.ItemNum).Take(paging.ItemNum).ToList();
        }
        public IQueryable<Reply> GetAllDataList(ForPaging paging, int mId)
        {
            IQueryable<Reply> Data = _repository.GetAll().Where(p => p.MId == mId);
            paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Data.Count()) / paging.ItemNum));
            paging.SetRightPage();
            return Data;
        }
        #endregion
        #endregion

        #region 新增回覆
        public void Insert(Reply data)
        {
            _repository.Create(data);
            _db.Save();
        }
        #endregion

        #region 修改檢查回覆
        public bool CheckUpdate(int rId)
        {
            Reply data = GetDataById(rId);
            return (data != null);
        }
        #endregion

        #region 修改回覆
        public void Update(Reply data)
        {
            _repository.Update(data);
            _db.Save();
        }
        #endregion

        #region 刪除回覆
        public void Delete(Reply data)
        {
            _repository.Delete(data);
            _db.Save();
        }
        #endregion

        #region API

        #region 取得全部

        public IEnumerable<ReplyView> GetAll(string MId)
        {
            int data = Convert.ToInt32(MId);
            var replyList = _repository.GetAll().Where(p => p.MId == data).ToList().OrderByDescending(p => p.RId);
            List<ReplyView> replyListView = Mapper.Map<List<ReplyView>>(replyList);
            return replyListView;
        }

        #endregion

        //#region 取得全部

        //public IEnumerable<ReplyView> GetAll(int MId)
        //{
        //    //int data = Convert.ToInt32(MId);
        //    var replyList = _repository.GetAll().Where(p => p.MId == MId).ToList().OrderByDescending(p => p.RId);
        //    List<ReplyView> replyListView = Mapper.Map<List<ReplyView>>(replyList);
        //    return replyListView;
        //}

        //#endregion

        #region 新增留言

        public void CreateApi(ReplyView newData)
        {
            newData.ReplyTime = DateTime.Now;
            Reply data = Mapper.Map<Reply>(newData);
            _repository.Create(data);
            _db.Save();
        }

        #endregion

        #region 刪除回覆

        public void DeleteApi(string RId)
        {
            int rid = Convert.ToInt32(RId);
            Reply data = GetDataById(rid);
            Delete(data);
            _db.Save();
        }

        #endregion

        #endregion
    }
}