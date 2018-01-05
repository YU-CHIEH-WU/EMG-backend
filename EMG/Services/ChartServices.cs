using EMG.Interface;
using EMG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMG.Services
{
    public class ChartServices : IChartServices
    {
        private IUnitOfWork _db;
        private IRepository<EMGData> _EMGrepository;
        private IRepository<Account_Course> _Account_Course;
        private IRepository<User> _Userepository;
        private IRepository<RecordEffect> _RecordEffect;

        public ChartServices(IUnitOfWork uow, IRepository<EMGData> EMGrepository, IRepository<Account_Course> Account_Course,
            IRepository<User> Userepository, IRepository<RecordEffect> RecordEffect)
        {
            this._db = uow;
            this._EMGrepository = EMGrepository;
            this._Userepository = Userepository;
            this._RecordEffect = RecordEffect;
            this._Account_Course = Account_Course;
        }

        #region APP

        #region 取得訓練日期動作
        public List<Account_Course> getAccountCourseAPP(string account)
        {
            var today = DateTime.Today;
            var acAll = _Account_Course.GetAll();
            var acList = acAll.Where(p => p.Account == account && p.Date <= today).OrderByDescending(p => p.Date).ToList();
            return acList;
        }
        #endregion

        #region 取得訓練成效
        public List<EMGData> getEMGDataAPP(string account, DateTime date, int Id)
        {
            var emgAll = _EMGrepository.GetAll();
            var emgList = emgAll.Where(p => p.Account == account && p.Date == date && p.E_Id == Id).ToList();
            return emgList;
        }
        #endregion

        #region 取得肌力
        public List<RecordEffect> getEffectDataAPP(string account, DateTime startDate, DateTime endDate)
        {
            var AllDate = _RecordEffect.GetAll();
            var reList = AllDate.Where(p => p.Account == account && p.Date >= startDate && p.Date <= endDate).OrderBy(p => p.Date).ToList();
            return reList;
        }
        #endregion

        
        #endregion
    }
}