using EMG.Interface;
using EMG.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace EMG.Services
{
    public class AlbumServices : IAlbumServices
    {
        private IUnitOfWork _db;
        private IRepository<Photo> _Photorepository;
        private IRepository<RecordEffect> _RecordEffectrepository;
        private IRepository<User> _Userrepository;

        public AlbumServices(IUnitOfWork uow, IRepository<Photo> Photorepository, IRepository<RecordEffect> RecordEffectrepository, IRepository<User> Userrepository)
        {
            this._db = uow;
            this._Photorepository = Photorepository;
            this._RecordEffectrepository = RecordEffectrepository;
            this._Userrepository = Userrepository;
        }

        #region 取得前五名

        public List<PhotoListView> getRank()
        {
            List<RankView> rkList = new List<RankView>();
            List<EffectTable> etList = new List<EffectTable>();
            List<PhotoListView> photoList = new List<PhotoListView>();
            var dataAllList = _RecordEffectrepository.GetAll();
            var photoAllList = _Photorepository.GetAll();
            var accountList = dataAllList.GroupBy(p => p.Account).ToList();
            var testList = dataAllList.OrderBy(p => p.Account).ToList();
            var aCount = accountList.Count();
            for (int i = 0; i < aCount; i++)
            {
                var account = accountList[i].Key;
                double? avgEffect = accountList[i].Average(p => p.Effect);
                EffectTable et = new EffectTable();
                et.Account = account;
                et.avgEffect = avgEffect;
                etList.Add(et);
            }
            var rankList = etList.OrderByDescending(p => p.avgEffect).ToList();
            int x = 1;
            foreach (var item in rankList.Take(5).ToList())
            {         
                RankView rv = new RankView();
                rv.Account = item.Account;
                rv.rank += x;
                x++;
                rkList.Add(rv);
            }
            foreach(var item in rkList)
            {
                var data = photoAllList.Where(p => p.Account == item.Account).ToList();
                var name = _Userrepository.Get(p => p.Account == item.Account).Name;
                PhotoListView pv = new PhotoListView();
                pv.Name = name;
                pv.Rank = item.rank;
                pv.photoList = data;
                photoList.Add(pv);
            }
            return photoList;
        }

        #endregion


        #region 取得個人全部相片

        public List<Photo> GetPersonPhoto(string account)
        {
            var dataAllList = _Photorepository.GetAll();
            return dataAllList.Where(p => p.Account == account).ToList();
        }

        #endregion

        //#region 上傳相片

        //public void UploadPhoto(Photo image, string img, string url)
        //{
        //    image.Account = HttpContext.Current.User.Identity.Name;
        //    image.Img = img;
        //    image.Url = url;
        //    image.CreateTime = DateTime.Now;
        //    _Photorepository.Create(image);
        //    _db.Save();
        //}

        //#endregion

        #region 上傳相片

        public void UploadPhoto(HttpPostedFile File)
        {           
            if(File != null)
            {
                string Url = Path.Combine(HttpContext.Current.Server.MapPath("~/FileUpload"), File.FileName);
                string url = "http://163.17.136.197:8080/EMG/FileUpload/" + File.FileName;
                File.SaveAs(Url);
                Photo image = new Photo();
                image.Account = HttpContext.Current.User.Identity.Name;
                image.Url = url;
                image.Img = File.FileName;
                image.CreateTime = DateTime.Now;
                _Photorepository.Create(image);
            }
            
        }

        #endregion


        #region 刪除相片

        public void DeletePhoto(int P_Id)
        {
            Photo DeleteData = _Photorepository.Get(p => p.P_Id == P_Id);
            _Photorepository.Delete(DeleteData);
            _db.Save();
        }

        #endregion

        public class EffectTable
        {
            public string Account { get; set; }
            public double? avgEffect { get; set; }
        }
    }
}