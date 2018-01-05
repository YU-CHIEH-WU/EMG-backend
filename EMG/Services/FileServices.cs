using EMG.Interface;
using EMG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMG.Services
{
    public class FileServices : IFileServices
    {
        private IUnitOfWork _db;

        private IRepository<File> _repository;

        public FileServices(IUnitOfWork uow, IRepository<File> repository)
        {
            this._db = uow;
            this._repository = repository;
        }

        #region 新增檔案
        
        public void Create(string fileName, string type, string path, int size)
        {
            File newFile = new File();
            newFile.Id = GetAllCount();
            newFile.Name = fileName;
            newFile.Url = path;
            newFile.Type = type;
            newFile.Size = size;
            newFile.Account = HttpContext.Current.User.Identity.Name;
            newFile.Edition = 0;
            newFile.Date = DateTime.Now;
            newFile.IsLatest = true;
            _repository.Create(newFile);
            _db.Save();
        }

        #endregion

        #region 新增版本

        public void Update(int id, string fileName, int editionCount, string type, string path, int size)
        {
            File newFile = new File();
            newFile.Id = id;
            newFile.Edition = editionCount;
            newFile.Name = fileName;
            newFile.Url = path;
            newFile.Type = type;
            newFile.Size = size;
            newFile.Account = HttpContext.Current.User.Identity.Name;
            newFile.Date = DateTime.Now;
            newFile.IsLatest = true;
            _repository.Create(newFile);
            File preFile = Get(id, editionCount - 1);
            preFile.IsLatest = false;
            _db.Save();
        }

        #endregion

        #region 放入待刪

        public void Delete(int id, int edition)
        {
            File nowFile = Get(id, edition);
            nowFile.Date = DateTime.Now;
            nowFile.IsTrash = true;
            _db.Save();
        }

        #endregion

        #region 確認刪除

        public void DeleteConfirm(int id, int edition)
        {
            File nowFile = Get(id, edition);
            if (nowFile.IsTrash)
            {
                _repository.Delete(nowFile);
                _db.Save();
            }
        }

        #endregion

        #region 取得單一版本檔案

        public File Get(int id, int edition)
        {
            return _repository.Get(p => p.Id == id && p.Edition == edition);
        }

        #endregion

        #region 取得所有檔案(最新版本)

        public List<File> GetAll()
        {
            return _repository.GetAll().Where(p => p.IsLatest == true && p.IsTrash == false).OrderByDescending(p => p.Date).ToList();
        }

        #endregion

        #region 取得單一檔案所有版本

        public List<File> GetEdition(int id)
        {
            return _repository.GetAll().Where(p => p.Id == id && p.IsTrash == false).OrderByDescending(p => p.Date).ToList();
        }

        #endregion

        #region 取得待刪列表

        public List<File> GetTrash()
        {
            return _repository.GetAll().Where(p => p.IsTrash == true &&
            p.Account == HttpContext.Current.User.Identity.Name).OrderByDescending(p => p.Date).ToList();
        }

        #endregion

        public int GetAllCount()
        {
            return _repository.GetAll().Where(p => p.IsLatest == true).Count();
        }

        public int GetEditionCount(int id)
        {
            return _repository.GetAll().Where(p => p.Id == id).Count();
        }
    }
}