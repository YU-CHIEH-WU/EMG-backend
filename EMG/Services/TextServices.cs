using EMG.Interface;
using EMG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMG.Services
{
    public class TextServices : ITextServices
    {
        private IUnitOfWork _db;

        private IRepository<Text> _repository;

        public TextServices(IUnitOfWork uow, IRepository<Text> repository)
        {
            this._db = uow;
            this._repository = repository;
        }

        #region 搜尋內文
        public Text GetDataByTitle(string title, Text Data)
        {
            var data = _repository.Get(p => p.Title == title);
            if (data != null)
            {
                return data;
            }
            else
            {
                Insert(Data);
                return Data;
            }
        }
        #endregion

        #region 新增內文 
        public void Insert(Text data)
        {
            _repository.Create(data);
            _db.Save();
        }
        #endregion

        //#region 修改檢查內文
        //public bool CheckUpdate(string title)
        //{
        //    Text data = GetDataByTitle(title);
        //    return (data != null);
        //}
        //#endregion

        #region 修改內文
        public bool Update(Text data)
        {
            var oldData = _repository.Get(p => p.Title == data.Title);
            if (oldData != null)
            {
                oldData.Contents = data.Contents;
                oldData.CreateTime = DateTime.Now;
                _db.Save();
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}