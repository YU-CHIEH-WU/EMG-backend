using AutoMapper;
using EMG.Interface;
using EMG.Model;
using EMG.Model.ViewModel.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMG.Services
{
    public class NewsServices : INewsServices
    {
        private IUnitOfWork _db;

        private IRepository<News> _newsrepository;

        public NewsServices(IUnitOfWork uow, IRepository<News> newsrepository)
        {
            this._db = uow;
            this._newsrepository = newsrepository;
        }

        #region 取得全部資料
        public IEnumerable<NewsView> GetAll()
        {
            var newList = _newsrepository.GetAll().OrderBy(p => p.N_Id).ToList();
            List<NewsView> newsListView = Mapper.Map<List<NewsView>>(newList);
            return newsListView;
        }
        #endregion

        #region 取得一筆資料
        public News GetDataById(int nId)
        {
            return _newsrepository.Get(p => p.N_Id == nId);
        }


        #endregion

        #region 新增新聞
        public void Create(NewsView newData)
        {
            newData.CreateTime = DateTime.Now;
            News data = Mapper.Map<News>(newData);
            _newsrepository.Create(data);
            _db.Save();
        }
        #endregion

        #region 修改新聞
        public void Edit(NewsView nowData)
        {
            var data = GetDataById(nowData.N_Id);
            data.Title = nowData.Title;
            data.Detail = nowData.Detail;
            data.CreateTime = DateTime.Now;
            _newsrepository.Update(data);
            _db.Save();
        }
        #endregion

        #region 刪除新聞
        public void Delete(string n_Id)
        {
            int nid = Convert.ToInt32(n_Id);
            News data = GetDataById(nid);
            _newsrepository.Delete(data);
            _db.Save();
        }
        #endregion
    }
}