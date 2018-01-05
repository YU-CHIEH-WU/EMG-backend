using AutoMapper;
using EMG.Interface;
using EMG.Model;
using EMG.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMG.Services
{
    public class EMGServices : IEMGServices
    {
        private IUnitOfWork _db;
        private IRepository<EMGData> _EMGrepository;
        private IRepository<IEMGData> _IEMGrepository;
        private IRepository<RMSData> _RMSrepository;
        private IRepository<User> _Userepository;
        private IRepository<RecordEffect> _RecordEffect;
        private IRepository<Course_Tag> _Course_Tag;
        private IRepository<Rest_Max> _Rest_Max;

        public EMGServices(IUnitOfWork uow, IRepository<EMGData> EMGrepository, IRepository<IEMGData> IEMGrepository, IRepository<RMSData> RMSrepository,
            IRepository<User> Userepository, IRepository<RecordEffect> RecordEffect, IRepository<Course_Tag> Course_Tag, IRepository<Rest_Max> Rest_Max)
        {
            this._db = uow;
            this._EMGrepository = EMGrepository;
            this._IEMGrepository = IEMGrepository;
            this._RMSrepository = RMSrepository;
            this._Userepository = Userepository;
            this._RecordEffect = RecordEffect;
            this._Course_Tag = Course_Tag;
            this._Rest_Max = Rest_Max;
        }
    
        #region Api

        #region 取得資料

        public void GetData(EMGView data)
        {
            EMGData emg = new EMGData();
            //IEMGData iemg = new IEMGData();
            //RMSData rms = new RMSData();

            var Series = Convert.ToInt32(data.SeriesString);
            var day = DateTime.Now.ToString("yyyyMMddHHmmss");
            var EMGdataAll = _EMGrepository.GetAll();
            EMGData ed = EMGdataAll.Where(p => p.E_Id == data.Id
                                               && p.Account == data.Account
                                               && p.C_Id == data.C_Id
                                               && p.Date == data.Date
                                               && p.P_Name == data.P_Name
                                               && p.Series == Series
                                               && p.Times == data.Times
                                               && p.Type == data.type).FirstOrDefault();
            if (ed == null)
            {
                emg.E_Id = data.Id;
                emg.Account = data.Account;
                //iemg.Account = data.Account;
                //rms.Account = data.Account;
                emg.C_Id = data.C_Id;
                //iemg.C_Id = data.C_Id;
                //rms.C_Id = data.C_Id;
                emg.Date = data.Date;
                //iemg.Date = data.Date;
                //rms.Date = data.Date;
                emg.P_Name = data.P_Name;
                //iemg.P_Name = data.P_Name;
                //rms.P_Name = data.P_Name;
                emg.Part = data.Part;
                //iemg.Part = data.Part;
                //rms.Part = data.Part;
                emg.Series = Series;
                //iemg.Series = Series;
                //rms.Series = Series;
                emg.Times = data.Times;
                //iemg.Times = data.Times;
                //rms.Times = data.Times;
                emg.Type = data.type;
                //iemg.Type = data.type;
                //rms.Type = data.type;
                emg.PMVC = Convert.ToDouble(data.PMVC);
                //iemg.PMVC = Convert.ToDouble(data.PMVC);
                //rms.PMVC = Convert.ToDouble(data.PMVC);
                emg.CreateTime = DateTime.Now;
                //iemg.CreateTime = DateTime.Now;
                //rms.CreateTime = DateTime.Now;
                emg.Training = day.ToString();
                //iemg.Training = day.ToString();
                //rms.Training = day.ToString();
                emg.EMG = data.EMG;
                emg.IEMG = Convert.ToDouble(data.IEMG);
                emg.RMS = Convert.ToDouble(data.RMS);
                //iemg.IEMG = Convert.ToDouble(data.IEMG);
                //rms.RMS = Convert.ToDouble(data.RMS);
                _EMGrepository.Create(emg);
                //_IEMGrepository.Create(iemg);
                //_RMSrepository.Create(rms);
            }
            else
            {
                ed.CreateTime = DateTime.Now;
                ed.Training = day.ToString();
                ed.PMVC = Convert.ToDouble(data.PMVC);
                ed.EMG = data.EMG;
                ed.IEMG = Convert.ToDouble(data.IEMG);
                ed.RMS = Convert.ToDouble(data.RMS);
                _EMGrepository.Update(ed);
            }

            _db.Save();
        }

        #endregion

        #region 儲存放鬆出力檢測值
        public void saveRest_Max(Rest_Max data)
        {
            var dataAll = _Rest_Max.GetAll();
            Rest_Max rm = dataAll.Where(p => p.Id == data.Id
                                            && p.Account == data.Account
                                            && p.C_Id == data.C_Id
                                            && p.Date == data.Date
                                            && p.P_Name == data.P_Name).FirstOrDefault();
            if (rm == null)
            {
                _Rest_Max.Create(data);
            }
            else
            {
                rm.restRMS = data.restRMS;
                rm.maxRMS = data.maxRMS;
                _Rest_Max.Update(rm);
            }
            
            _db.Save();
        }
        #endregion

        #region 取得出力百分比

        public List<OutputView> getOutput()
        {
            List<OutputView> outputList = new List<OutputView>();
            List<EMGData> test = new List<EMGData>();
            var EMGdataAll = _EMGrepository.GetAll();
            var groupList = EMGdataAll.GroupBy(p => p.Date).ToList();
            int gCount = groupList.Count();
            int i = 0;
            int pmvcMin = 0;
            int pmvcMax = 100;
            foreach(var item in groupList)
            {
                var accountList = item.GroupBy(p => p.Account).ToList();
                foreach(var item2 in accountList)
                {
                    var dataList = item2.GroupBy(p => p.E_Id).ToList();
                    foreach(var item3 in dataList)
                    {
                        int sum = Convert.ToInt32(item3.Sum(p => p.PMVC));
                        var data = item3.ToList();
                        if(data[i].PMVC < pmvcMin)
                        {                           
                            OutputView op = new OutputView();
                            op.Account = data[i].Account;
                            op.C_Id = data[i].C_Id;
                            op.Date = data[i].Date;
                            op.P_Name = data[i].P_Name;
                            op.Series = data[i].Series;
                            op.Times = data[i].Times;
                            op.type = data[i].Type;
                            op.PMVC = 0;
                            outputList.Add(op);
                            outputList.Add(op);
                        }
                        else if (data[i].PMVC > pmvcMax)
                        {
                            OutputView op = new OutputView();
                            op.Account = data[i].Account;
                            op.C_Id = data[i].C_Id;
                            op.Date = data[i].Date;
                            op.P_Name = data[i].P_Name;
                            op.Series = data[i].Series;
                            op.Times = data[i].Times;
                            op.type = data[i].Type;
                            op.PMVC = 100;
                            outputList.Add(op);
                        }
                        else
                        {
                            OutputView op = new OutputView();
                            op.Account = data[i].Account;
                            op.C_Id = data[i].C_Id;
                            op.Date = data[i].Date;
                            op.P_Name = data[i].P_Name;
                            op.Series = data[i].Series;
                            op.Times = data[i].Times;
                            op.type = data[i].Type;
                            op.PMVC = Convert.ToInt32(data[i].PMVC);
                            outputList.Add(op);
                        }
                        i++;
                    }
                }               
            }
            return outputList;
        }

        #endregion

        #endregion
    }
}