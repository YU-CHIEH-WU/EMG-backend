using EMG.Interface;
using EMG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMG.Services
{
    public class ActImgService
    {
        EMGDataBaseEntities db = new EMGDataBaseEntities();

        #region 查詢相關

        #region 查詢一筆相片
        public Image GetDataById(int act_Id)
        {
            return db.Image.Find(act_Id);
        }
        #endregion

        #region 查詢一本相簿
        public ActImg GetDataBySort(string Sort)
        {
            return db.ActImg.Find(Sort);
        }
        #endregion

        #region 查詢相簿陣列資料
        public List<ActImg> GetDataList()
        {
            return db.ActImg.ToList();
        }
        #endregion

        #region 查詢相片陣列資料
        public List<Image> GetData(string Sort)
        {
            return db.Image.Where(p => p.Sort == Sort).OrderBy(p => p.Act_Id).ToList();
        }
        #endregion

        #endregion

        #region 相簿相關

        #region 建立相簿
        public void Insert(ActImgView File, string account)
        {
            ActImg Data = new ActImg();
            Data.Sort = File.Sort;
            Data.Brief = File.Brief;
            Data.CreateTime = DateTime.Now;
            Data.Img = File.upload.FileName;
            Data.Account = HttpContext.Current.User.Identity.Name;
            db.ActImg.Add(Data);
            db.SaveChanges();
        }
        #endregion

        #region 刪除相簿
        public void DeleteAlbum(string Sort)
        {
            List<Image> Data = DeleteImg(Sort);
            db.Image.RemoveRange(Data);
            db.SaveChanges();
            ActImg DeleteData = db.ActImg.Find(Sort);
            db.ActImg.Remove(DeleteData);
            db.SaveChanges();
        }
        public List<Image> DeleteImg(string Sort)
        {
            IQueryable<Image> Deleteimg = db.Image;
            return Deleteimg.Where(p => p.Sort == Sort).ToList();
        }
        #endregion

        #endregion

        #region 照片相關

        #region 上傳相片
        public void UploadFile(string Sort, string Img, string Url)
        {
            Image newFile = new Image();
            newFile.Sort = Sort;
            newFile.Img = Img;
            newFile.Url = Url;
            db.Image.Add(newFile);
            db.SaveChanges();
        }
        #endregion

        #region 刪除相片
        public void DeleteFile(int Act_Id)
        {
            Image DeleteData = db.Image.Find(Act_Id);
            db.Image.Remove(DeleteData);
            db.SaveChanges();
        }
        #endregion

        #endregion
    }
}