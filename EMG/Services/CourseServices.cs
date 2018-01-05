using EMG.Interface;
using EMG.Model;
using EMG.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Security;

namespace EMG.Services
{
    public class CourseServices : ICourseServices
    {
        private IUnitOfWork _db;
        private IRepository<Posture> _Posturerepository;
        private IRepository<Course> _Courserepository;
        private IRepository<Account_Course> _Account_Courserepository;
        private IRepository<Course_Tag> _Course_Tagrepository;
        private IRepository<RecordEffect> _RecordEffectrepository;
        private IRepository<EMGData> _EMGrepository;
        private IRepository<UserStatus> _UserStatusrepository;
        private IRepository<User> _Userrepository;
        private IRepository<UserHabit> _UserHabitrepository;
        private IRepository<Notice> _Noticerepository;

        public double? Total;


        public CourseServices(IUnitOfWork uow, IRepository<Posture> Posturerepository, IRepository<Course> Courserepository, IRepository<Account_Course> Account_Courserepository, IRepository<Course_Tag> Course_Tagrepository, IRepository<RecordEffect> RecordEffectrepository, IRepository<EMGData> EMGrepository, IRepository<UserStatus> UserStatusrepository, IRepository<User> Userrepository, IRepository<UserHabit> UserHabitrepository, IRepository<Notice> Noticrepository)
        {
            this._db = uow;
            this._Posturerepository = Posturerepository;
            this._Courserepository = Courserepository;
            this._Account_Courserepository = Account_Courserepository;
            this._Course_Tagrepository = Course_Tagrepository;
            this._RecordEffectrepository = RecordEffectrepository;
            this._EMGrepository = EMGrepository;
            this._UserStatusrepository = UserStatusrepository;
            this._Userrepository = Userrepository;
            this._UserHabitrepository = UserHabitrepository;
            this._Noticerepository = Noticrepository;
        }

        #region 是否有課程
        public List<Account_Course> hasCourse(string account)
        {
            List<Account_Course> acList = new List<Account_Course>();
            var today = DateTime.Today;
            var dataAllList = _Account_Courserepository.GetAll();
            var data = dataAllList.Where(p => p.Account == account && p.Date >= today).Select(p => p.C_Id).FirstOrDefault();
            if (data == null)
            {
                return acList;
            }
            else
            {
                return dataAllList.Where(p => p.Account == account && p.Date >= today && p.C_Id == data).ToList();
            }
        }
        #endregion

        #region 取得課程
        public List<Account_Course> getCourse(string account)
        {
            List<Account_Course> acList = new List<Account_Course>();
            var dataAllList = _Account_Courserepository.GetAll();
            acList = dataAllList.Where(p => p.Account == account).ToList();
            return acList;
        }
        #endregion

        #region 取得熱門課程
        public List<Course> getPopular()
        {
            var dataAllList = _Course_Tagrepository.GetAll();
            var popularOne = dataAllList.OrderByDescending(p => p.CompleteTimes).FirstOrDefault();
            var popularId = popularOne.C_Id;
            var courseList = _Courserepository.GetAll();
            List<Course> popularList = courseList.Where(p => p.C_Id == popularId).ToList();
            return popularList;
        }
        #endregion

        #region 取得主題課程
        public List<Course> getTheme()
        {
            var dataAllList = _RecordEffectrepository.GetAll();
            var partList = dataAllList.OrderByDescending(p => p.Part).GroupBy(p => p.C_Id).ToList();  
            foreach (var item in partList)
            {
                int i = 0;
                var group = item.GroupBy(p => p.C_Id).ToList();
                var data = group[i].ToList();
                var account = data[i].Account;
                var account2 = data[0].Account;
                int count = data.Count();
                for (int m = 0; i < count; m++)
                {  
                    if (account == account2)
                    {
                        var id = data[i].Id;
                        var c_Id = data[i].C_Id;
                        var Account = data[i].Account;
                        var Date = data[i].Date;
                        var dataList = item.Where(p => p.Account == Account && p.C_Id == c_Id && p.Date == Date).OrderBy(p => p.C_Id).ToList();    
                        var strength = (dataList.Max(p => p.Strength) - dataList.Min(p => p.Strength)) / dataList.Min(p => p.Strength);
                        var endurance = (dataList.Max(p => p.Endurance) - dataList.Min(p => p.Endurance)) / dataList.Min(p => p.Endurance);
                        var effect = dataList.Average(p => p.Effect);
                        if (endurance == null)
                        {
                            var stotal = strength + effect;
                            var List = _RecordEffectrepository.GetAll();
                            RecordEffect rc = dataList.Where(p => p.Id == id
                                                                    && p.Account == Account
                                                                    && p.C_Id == c_Id
                                                                    && p.Date == data[i].Date
                                                                    && p.P_Name == data[i].P_Name
                                                                    && p.Part == data[i].Part
                                                                    && p.Strength == data[i].Strength
                                                                    && p.Endurance == data[i].Endurance
                                                                    && p.Effect == data[i].Effect).FirstOrDefault();
                            rc.Total = stotal;
                            _RecordEffectrepository.Update(rc);
                            _db.Save();
                            i++;
                        }
                        else if (strength == null)
                        {
                            var etotal = endurance + effect;
                            var List = _RecordEffectrepository.GetAll();
                            RecordEffect rc = dataList.Where(p => p.Id == id
                                                                    && p.Account == Account
                                                                    && p.C_Id == c_Id
                                                                    && p.Date == data[i].Date
                                                                    && p.P_Name == data[i].P_Name
                                                                    && p.Part == data[i].Part
                                                                    && p.Strength == data[i].Strength
                                                                    && p.Endurance == data[i].Endurance
                                                                    && p.Effect == data[i].Effect).FirstOrDefault();
                            rc.Total = etotal;
                            _RecordEffectrepository.Update(rc);
                            _db.Save();
                            i++;
                        }                        
                    }    
                    else
                    {
                        var id = data[i].Id;
                        var c_Id = data[i].C_Id;
                        var Account = data[i].Account;
                        var Date = data[i].Date;
                        var dataList = item.Where(p => p.Account == Account && p.C_Id == c_Id && p.Date == Date).OrderBy(p => p.C_Id).ToList();
                        var strength = (dataList.Max(p => p.Strength) - dataList.Min(p => p.Strength)) / dataList.Min(p => p.Strength);
                        var endurance = (dataList.Max(p => p.Endurance) - dataList.Min(p => p.Endurance)) / dataList.Min(p => p.Endurance);
                        var effect = dataList.Average(p => p.Effect);
                        if (endurance == null)
                        {
                            var stotal = strength + effect;
                            var List = _RecordEffectrepository.GetAll();
                            RecordEffect rc = dataList.Where(p => p.Id == id
                                                                    && p.Account == Account
                                                                    && p.C_Id == c_Id
                                                                    && p.Date == data[i].Date
                                                                    && p.P_Name == data[i].P_Name
                                                                    && p.Part == data[i].Part
                                                                    && p.Strength == data[i].Strength
                                                                    && p.Endurance == data[i].Endurance
                                                                    && p.Effect == data[i].Effect).FirstOrDefault();
                            rc.Total = stotal;
                            _RecordEffectrepository.Update(rc);
                            _db.Save();
                            i++;
                        }
                        else if (strength == null)
                        {
                            var etotal = endurance + effect;
                            var List = _RecordEffectrepository.GetAll();
                            RecordEffect rc = dataList.Where(p => p.Id == id
                                                                    && p.Account == Account
                                                                    && p.C_Id == c_Id
                                                                    && p.Date == data[i].Date
                                                                    && p.P_Name == data[i].P_Name
                                                                    && p.Part == data[i].Part
                                                                    && p.Strength == data[i].Strength
                                                                    && p.Endurance == data[i].Endurance
                                                                    && p.Effect == data[i].Effect).FirstOrDefault();
                            rc.Total = etotal;
                            _RecordEffectrepository.Update(rc);
                            _db.Save();
                            i++;
                        }                    
                    }
                }            
            }
            var rankList = dataAllList.OrderByDescending(p => p.Total).FirstOrDefault();
            var themeC_Id = rankList.C_Id;
            var courseList = _Courserepository.GetAll();
            List<Course> themeList = courseList.Where(p => p.C_Id == themeC_Id).ToList();
            return themeList;
        }
        #endregion

        #region 取得減脂課程
        public List<Course> getBodyfat()
        {
            var dataAllList = _UserStatusrepository.GetAll();
            var fatList = dataAllList.OrderByDescending(p => p.Bodyfat).GroupBy(p => p.C_Id).ToList();
            foreach(var item in fatList)
            {
                int i = 0;
                var group = item.GroupBy(p => p.C_Id).ToList();
                var data = group[i].ToList();
                var account = data[i].Account;
                var account2 = data[0].Account;
                int count = data.Count();
                for(int m = 0; i < count; m++)
                {
                    if (account == account2)
                    {
                        var c_Id = data[i].C_Id;
                        var Account = data[i].Account;
                        var Date = data[i].Date;
                        var dataList = item.Where(p => p.Account == Account && p.C_Id == c_Id && p.Date == Date).OrderBy(p => p.C_Id).ToList();
                        var bodyfat = (dataList.Max(p => p.Bodyfat) - dataList.Min(p => p.Bodyfat)) / dataList.Min(p => p.Bodyfat);
                        var List = _UserStatusrepository.GetAll();
                        UserStatus us = dataList.Where(p => p.Account == Account
                                                            && p.C_Id == c_Id
                                                            && p.Date == data[i].Date
                                                            && p.Photo == data[i].Photo
                                                            && p.Height == data[i].Height
                                                            && p.Weight == data[i].Weight
                                                            && p.Bodyfat == data[i].Bodyfat
                                                            && p.BMR == data[i].BMR
                                                            && p.Balance == data[i].Balance
                                                            && p.Strength == data[i].Strength
                                                            && p.Endurance == data[i].Endurance
                                                            && p.Effect == data[i].Effect
                                                            && p.Bodyrate == data[i].Bodyrate
                                                            && p.Burden == data[i].Burden).FirstOrDefault();
                        us.Rank = bodyfat;
                        _UserStatusrepository.Update(us);
                        _db.Save();
                        i++;
                    }
                    else
                    {
                        var c_Id = data[i].C_Id;
                        var Account = data[i].Account;
                        var Date = data[i].Date;
                        var dataList = item.Where(p => p.Account == Account && p.C_Id == c_Id && p.Date == Date).OrderBy(p => p.C_Id).ToList();
                        var bodyfat = (dataList.Max(p => p.Bodyfat) - dataList.Min(p => p.Bodyfat)) / dataList.Min(p => p.Bodyfat);
                        var List = _UserStatusrepository.GetAll();
                        UserStatus us = dataList.Where(p => p.Account == Account
                                                            && p.C_Id == c_Id
                                                            && p.Date == data[i].Date
                                                            && p.Photo == data[i].Photo
                                                            && p.Height == data[i].Height
                                                            && p.Weight == data[i].Weight
                                                            && p.Bodyfat == data[i].Bodyfat
                                                            && p.BMR == data[i].BMR
                                                            && p.Balance == data[i].Balance
                                                            && p.Strength == data[i].Strength
                                                            && p.Endurance == data[i].Endurance
                                                            && p.Effect == data[i].Effect
                                                            && p.Bodyrate == data[i].Bodyrate
                                                            && p.Burden == data[i].Burden).FirstOrDefault();
                        us.Rank = bodyfat;
                        _UserStatusrepository.Update(us);
                        _db.Save();
                        i++;
                    }
                }
            }
            var rankList = dataAllList.OrderByDescending(p => p.Rank).FirstOrDefault();
            var bodyfatC_Id = rankList.C_Id;
            var courseList = _Courserepository.GetAll();
            List<Course> bodyfatList = courseList.Where(p => p.C_Id == bodyfatC_Id).ToList();
            return bodyfatList;
        }
        #endregion

        #region 個人推薦課程

        #region 取得相同性別、歲數群組課程

        public List<Course> getAge(string account)
        {        
            List<Course> ageList = new List<Course>();
            var dataAllList = _Userrepository.GetAll();
            var self = dataAllList.Where(p => p.Account == account).FirstOrDefault();
            int sameAge = self.Age;
            var sameSex = self.Sex;
            int age1 = sameAge - 2;
            int age2 = sameAge + 3;
            var accountList = dataAllList.Where(p => p.Sex == sameSex && p.Age >= age1 && p.Age <= age2).OrderByDescending(p => p.Account).ToList();
            var effectList = _RecordEffectrepository.GetAll();
            var account_course = effectList.OrderByDescending(p => p.Total).ToList();
            var group = accountList.GroupBy(p => p.Account).ToList();
            int count = accountList.Count();
            int i = 0;
            foreach(var item in accountList)
            {
                var data = group[i].ToList();
                var Account = data[0].Account;
                var dataList = account_course.Where(p => p.Account == Account).FirstOrDefault();
                if (dataList != null)
                {
                    var c_id = dataList.C_Id;
                    var courseList = _Courserepository.GetAll();
                    List<Course> sameList = courseList.Where(p => p.C_Id == c_id).ToList();
                    ageList = sameList;
                    i++;
                }
                else
                {
                    i++;
                }
                
            }
            return ageList;
        }

        #endregion

        #region 取得相同運動項目課程

        public List<Course> getSports(string account)
        {
            List<Course> sportsList = new List<Course>();
            var dataAllList = _UserHabitrepository.GetAll();
            var self = dataAllList.Where(p => p.Account == account).FirstOrDefault();
            var sports = self.Sports;
            var accountList = dataAllList.Where(p => p.Sports == sports).ToList();
            var effectList = _RecordEffectrepository.GetAll();
            var account_course = effectList.OrderByDescending(p => p.Total).ToList();
            var group = accountList.GroupBy(p => p.Account).ToList();
            int count = accountList.Count();
            int i = 0;
            foreach (var item in accountList)
            {
                var data = group[i].ToList();
                var Account = data[0].Account;
                var dataList = account_course.Where(p => p.Account == Account).FirstOrDefault();
                if (dataList != null)
                {
                    var c_id = dataList.C_Id;
                    var courseList = _Courserepository.GetAll();
                    List<Course> sameList = courseList.Where(p => p.C_Id == c_id).ToList();
                    sportsList = sameList;
                    i++;
                }
                else
                {
                    i++;
                }

            }
            return sportsList;
        }

        #endregion

        #region 取得注重相同肌肉部位

        public List<Course> getPlace(string account)
        {
            List<Course> placeList = new List<Course>();
            var dataAllList = _UserHabitrepository.GetAll();
            var self = dataAllList.Where(p => p.Account == account).FirstOrDefault();
            var place = self.Place;
            var accountList = dataAllList.Where(p => p.Place == place).ToList();
            var effectList = _RecordEffectrepository.GetAll();
            var account_course = effectList.OrderByDescending(p => p.Total).ToList();
            var group = accountList.GroupBy(p => p.Account).ToList();
            int count = accountList.Count();
            int i = 0;
            foreach (var item in accountList)
            {
                var data = group[i].ToList();
                var Account = data[0].Account;
                var dataList = account_course.Where(p => p.Account == Account).FirstOrDefault();
                if (dataList != null)
                {
                    var c_id = dataList.C_Id;
                    var courseList = _Courserepository.GetAll();
                    List<Course> sameList = courseList.Where(p => p.C_Id == c_id).ToList();
                    placeList = sameList;
                    i++;
                }
                else
                {
                    i++;
                }

            }
            return placeList;
        }

        #endregion

        #region 取得注重相同肌肉強度

        public List<Course> getMotivation(string account)
        {
            List<Course> motivationList = new List<Course>();
            var dataAllList = _UserHabitrepository.GetAll();
            var self = dataAllList.Where(p => p.Account == account).FirstOrDefault();
            var motivation = self.Motivation;
            var accountList = dataAllList.Where(p => p.Motivation == motivation).ToList();
            var effectList = _RecordEffectrepository.GetAll();
            var account_course = effectList.OrderByDescending(p => p.Total).ToList();
            var group = accountList.GroupBy(p => p.Account).ToList();
            int count = accountList.Count();
            int i = 0;
            foreach (var item in accountList)
            {
                var data = group[i].ToList();
                var Account = data[0].Account;
                var dataList = account_course.Where(p => p.Account == Account).FirstOrDefault();
                if (dataList != null)
                {
                    var c_id = dataList.C_Id;
                    var courseList = _Courserepository.GetAll();
                    List<Course> sameList = courseList.Where(p => p.C_Id == c_id).ToList();
                    motivationList = sameList;
                    i++;
                }
                else
                {
                    i++;
                }

            }
            return motivationList;
        }

        #endregion

        #region 取得完成了相同課程
        
        public List<Course> getSame(string account)
        {
            List<Course> sameList = new List<Course>();
            int i = 0;
            int m = 0;
            var dataAllList = _RecordEffectrepository.GetAll();
            var selfList = dataAllList.Where(p => p.Account == account).OrderBy(p => p.Date).ToList();
            var self = selfList.LastOrDefault();
            var lastC_Id = self.C_Id;
            var otherList = dataAllList.Where(p => p.Account != account && p.C_Id == lastC_Id).OrderBy(p => p.Account).ToList();
            var otherGroup = otherList.GroupBy(p => p.Account).ToList();  
            var otherCount = otherGroup.Count();
            var courseList = _Courserepository.GetAll();
            for(i = 0; i < otherCount; i++)
            {
                var otherData = otherGroup[i].ToList();
                var Account = otherData[i].Account;
                var theOne = dataAllList.Where(p => p.Account == Account && p.C_Id != lastC_Id).OrderByDescending(p => p.Total).FirstOrDefault();
                var c_id = theOne.C_Id;
                var course = courseList.Where(p => p.C_Id == c_id).ToList();
                int count = course.Count();
                for (m = 0; m < count; m++)
                {                  
                    var day = course[m].Days;
                    var like = _Courserepository.Get(p => p.C_Id == c_id && p.Days == day);
                    sameList.Add(like);
                }
            }
            return sameList;
        }

        #endregion

        #region 取得關注課程

        public List<Course> getWatch(string account)
        {
            
            List<Course> noticeList = new List<Course>();
            var dataAllList = _Noticerepository.GetAll();
            var c_idList = dataAllList.Where(p => p.Account == account).ToList();
            var courseList = _Courserepository.GetAll();
            var data = courseList.ToList();            
            foreach(var item in c_idList)
            {
                var course = data.Where(p => p.C_Id == item.C_Id).ToList();
                int count = course.Count();
                int i = 0;
                for (int m = 0; i < count; m++)
                {
                    var day = course[i].Days;
                    var notice = _Courserepository.Get(p => p.C_Id == item.C_Id && p.Days == day);
                    noticeList.Add(notice);
                    i++;
                }     
            }
            return noticeList;
        }

        #endregion

        #endregion

        #region 產生一週課程
        public List<Course> CreateCourse(CourseSelectView data)
        {
            List<Course> courseList = new List<Course>();
            var ciddate = DateTime.Today;
            List<string> partList = new List<string>();
            List<string> deviceList = new List<string>();
            deviceList.Add(null);
            int countPart = 0;
            int countDevice = 0;
            Random random = new Random();
            IEnumerable<Posture> postureAllList = _Posturerepository.GetAll();

            if (data.complex == "複合")
            {
                countPart = 8;
            }
            else
            {
                if (data.chest == "胸")
                {
                    countPart++;
                    partList.Add(data.chest);
                }
                if (data.three == "肱三頭")
                {
                    countPart++;
                    partList.Add(data.three);
                }
                if (data.back == "背")
                {
                    countPart++;
                    partList.Add(data.back);
                }
                if (data.two == "肱二頭")
                {
                    countPart++;
                    partList.Add(data.two);
                }
                if (data.foot == "腳")
                {
                    countPart++;
                    partList.Add(data.foot);
                }
                if (data.shoulder == "肩")
                {
                    countPart++;
                    partList.Add(data.shoulder);
                }
                if (data.belly == "腹")
                {
                    countPart++;
                    partList.Add(data.belly);
                }
            }

            if (data.TBar == "T-Bar划船訓練機")
            {
                countDevice++;
                deviceList.Add(data.TBar);
            }
            if (data.WBarbell == "W槓")
            {
                countDevice++;
                deviceList.Add(data.WBarbell);
            }
            if (data.Barbell == "槓鈴")
            {
                countDevice++;
                deviceList.Add(data.Barbell);
            }
            if (data.Dumbbell == "啞鈴")
            {
                countDevice++;
                deviceList.Add(data.Dumbbell);
            }
            if (data.Smith == "史密斯機架")
            {
                countDevice++;
                deviceList.Add(data.Smith);
            }
            if (data.LegPress == "腿舉機")
            {
                countDevice++;
                deviceList.Add(data.LegPress);
            }
            if (data.Butterfly == "蝴蝶機")
            {
                countDevice++;
                deviceList.Add(data.Butterfly);
            }
            if (data.PullBack == "拉背機")
            {
                countDevice++;
                deviceList.Add(data.PullBack);
            }
            if (data.LegExtension == "腿部伸展機")
            {
                countDevice++;
                deviceList.Add(data.LegExtension);
            }
            if (data.PullUp == "引體上升器")
            {
                countDevice++;
                deviceList.Add(data.PullUp);
            }
            if (data.Squat == "蹲舉機")
            {
                countDevice++;
                deviceList.Add(data.Squat);
            }
            if (data.Pulley == "滑輪機和滑輪握把")
            {
                countDevice++;
                deviceList.Add(data.Pulley);
            }
            if (data.Boating == "划船機")
            {
                countDevice++;
                deviceList.Add(data.Boating);
            }
            if (data.PulleyChest == "滑輪擴胸機")
            {
                countDevice++;
                deviceList.Add(data.PulleyChest);
            }
            if (data.WeightBench == "舉重椅")
            {
                countDevice++;
                deviceList.Add(data.WeightBench);
            }
            if (data.ParallelBars == "雙槓")
            {
                countDevice++;
                deviceList.Add(data.ParallelBars);
            }
            if (data.Cross == "交叉訓練機")
            {
                countDevice++;
                deviceList.Add(data.Cross);
            }

            List<Posture> postureListRep = new List<Posture>();
            foreach (var item in deviceList)
            {
                var postureData = postureAllList.Where(p => p.Device == item).ToList();
                foreach (var item2 in postureData)
                {
                    postureListRep.Add(item2);
                }
            }

            #region 選1個部位
            if (countPart == 1)
            {
                List<Posture> postureList = postureListRep.Where(p => p.Part == partList[0]).ToList();
                for (int i = 1; i <= 2; i++)
                {
                    Course course = new Course();
                    course.C_Id = ciddate.ToString("yyyyMMdd") + "_" + data.account;
                    course.Days = i;
                    for (int j = 1; j <= 6; j++)
                    {
                        string posname = "";
                        int num = random.Next(postureList.Count);
                        if (j == 1) {
                            course.Pos1 = postureList[num].Name.ToString();
                            posname = course.Pos1;
                        }
                        else if (j == 2)
                        {
                            course.Pos2 = postureList[num].Name.ToString();
                            posname = course.Pos2;
                        }
                        else if (j == 3)
                        {
                            course.Pos3 = postureList[num].Name.ToString();
                            posname = course.Pos3;
                        }
                        else if (j == 4)
                        {
                            course.Pos4 = postureList[num].Name.ToString();
                            posname = course.Pos4;
                        }
                        else if (j == 5)
                        {
                            course.Pos5 = postureList[num].Name.ToString();
                            posname = course.Pos5;
                        }
                        else if (j == 6)
                        {
                            course.Pos6 = postureList[num].Name.ToString();
                            posname = course.Pos6;
                        }
                    }
                    courseList.Add(course);
                }
            }
            #endregion

            #region 選2個部位
            if (countPart == 2)
            {
                List<Posture> postureList = new List<Posture>();
                List<Posture> postureList1 = postureListRep.Where(p => p.Part == partList[0]).ToList();
                List<Posture> postureList2 = postureListRep.Where(p => p.Part == partList[1]).ToList();
                for (int i = 1; i <= 4; i++)
                {
                    Course course = new Course();
                    course.C_Id = ciddate.ToString("yyyyMMdd") + "_" + data.account;
                    course.Days = i;
                    if (i % 2 == 0)
                    {
                        postureList = postureList2;
                    }
                    else
                    {
                        postureList = postureList1;
                    }

                    for (int j = 1; j <= 6; j++)
                    {
                        string posname = "";
                        int num = random.Next(postureList.Count);
                        if (j == 1)
                        {
                            course.Pos1 = postureList[num].Name.ToString();
                            posname = course.Pos1;
                        }
                        else if (j == 2)
                        {
                            course.Pos2 = postureList[num].Name.ToString();
                            posname = course.Pos2;
                        }
                        else if (j == 3)
                        {
                            course.Pos3 = postureList[num].Name.ToString();
                            posname = course.Pos3;
                        }
                        else if (j == 4)
                        {
                            course.Pos4 = postureList[num].Name.ToString();
                            posname = course.Pos4;
                        }
                        else if (j == 5)
                        {
                            course.Pos5 = postureList[num].Name.ToString();
                            posname = course.Pos5;
                        }
                        else if (j == 6)
                        {
                            course.Pos6 = postureList[num].Name.ToString();
                            posname = course.Pos6;
                        }
                    }
                    courseList.Add(course);
                }
            }
            #endregion

            #region 選3個部位
            if (countPart == 3)
            {
                List<Posture> postureList = new List<Posture>();
                List<Posture> postureList1 = postureListRep.Where(p => p.Part == partList[0]).ToList();
                List<Posture> postureList2 = postureListRep.Where(p => p.Part == partList[1]).ToList();
                List<Posture> postureList3 = postureListRep.Where(p => p.Part == partList[2]).ToList();
                for (int i = 1; i <= 6; i++)
                {
                    Course course = new Course();
                    course.C_Id = ciddate.ToString("yyyyMMdd") + "_" + data.account;
                    course.Days = i;
                    if (i % 3 == 0)
                    {
                        postureList = postureList3;
                    }
                    else if (i == 2 || i == 5)
                    {
                        postureList = postureList2;
                    }
                    else
                    {
                        postureList = postureList1;
                    }

                    for (int j = 1; j <= 6; j++)
                    {
                        string posname = "";
                        int num = random.Next(postureList.Count);
                        if (j == 1)
                        {
                            course.Pos1 = postureList[num].Name.ToString();
                            posname = course.Pos1;
                        }
                        else if (j == 2)
                        {
                            course.Pos2 = postureList[num].Name.ToString();
                            posname = course.Pos2;
                        }
                        else if (j == 3)
                        {
                            course.Pos3 = postureList[num].Name.ToString();
                            posname = course.Pos3;
                        }
                        else if (j == 4)
                        {
                            course.Pos4 = postureList[num].Name.ToString();
                            posname = course.Pos4;
                        }
                        else if (j == 5)
                        {
                            course.Pos5 = postureList[num].Name.ToString();
                            posname = course.Pos5;
                        }
                        else if (j == 6)
                        {
                            course.Pos6 = postureList[num].Name.ToString();
                            posname = course.Pos6;
                        }
                    }
                    courseList.Add(course);
                }
            }
            #endregion

            #region 選4個部位
            if (countPart == 4)
            {
                List<Posture> postureList_temp1 = new List<Posture>();
                List<Posture> postureList_temp2 = new List<Posture>();
                List<Posture> postureList1 = postureListRep.Where(p => p.Part == partList[0]).ToList();
                List<Posture> postureList2 = postureListRep.Where(p => p.Part == partList[1]).ToList();
                List<Posture> postureList3 = postureListRep.Where(p => p.Part == partList[2]).ToList();
                List<Posture> postureList4 = postureListRep.Where(p => p.Part == partList[3]).ToList();
                for (int i = 1; i <= 4; i++)
                {
                    Course course = new Course();
                    course.C_Id = ciddate.ToString("yyyyMMdd") + "_" + data.account;
                    course.Days = i;
                    if (i % 2 == 0)
                    {
                        postureList_temp2 = postureList4;
                        postureList_temp1 = postureList3;
                    }
                    else
                    {
                        postureList_temp2 = postureList2;
                        postureList_temp1 = postureList1;
                    }

                    for (int j = 1; j <= 6; j++)
                    {
                        string posname = "";
                        int num = 0;
                        if (j <= 3)
                        {
                            num = random.Next(postureList_temp1.Count);
                                
                        }
                        else
                        {
                            num = random.Next(postureList_temp2.Count);
                                
                        }
                        if (j == 1)
                        {
                            course.Pos1 = postureList_temp1[num].Name.ToString();
                            posname = course.Pos1;
                        }
                        else if (j == 2)
                        {
                            course.Pos2 = postureList_temp1[num].Name.ToString();
                            posname = course.Pos2;
                        }
                        else if (j == 3)
                        {
                            course.Pos3 = postureList_temp1[num].Name.ToString();
                            posname = course.Pos3;
                        }
                        else if (j == 4)
                        {
                            course.Pos4 = postureList_temp2[num].Name.ToString();
                            posname = course.Pos4;
                        }
                        else if (j == 5)
                        {
                            course.Pos5 = postureList_temp2[num].Name.ToString();
                            posname = course.Pos5;
                        }
                        else if (j == 6)
                        {
                            course.Pos6 = postureList_temp2[num].Name.ToString();
                            posname = course.Pos6;
                        }
                    }
                    courseList.Add(course);
                }
            }
            #endregion

            #region 選5個部位
            if (countPart == 5)
            {
                List<Posture> postureList_temp1 = new List<Posture>();
                List<Posture> postureList_temp2 = new List<Posture>();
                List<Posture> postureList1 = postureListRep.Where(p => p.Part == partList[0]).ToList();
                List<Posture> postureList2 = postureListRep.Where(p => p.Part == partList[1]).ToList();
                List<Posture> postureList3 = postureListRep.Where(p => p.Part == partList[2]).ToList();
                List<Posture> postureList4 = postureListRep.Where(p => p.Part == partList[3]).ToList();
                List<Posture> postureList5 = postureListRep.Where(p => p.Part == partList[4]).ToList();
                for (int i = 1; i <= 6; i++)
                {
                    Course course = new Course();
                    course.C_Id = ciddate.ToString("yyyyMMdd") + "_" + data.account;
                    course.Days = i;
                    if (i % 3 == 0)
                    {
                        postureList_temp1 = postureList5;
                    }
                    else if (i == 2 || i == 5)
                    {
                        postureList_temp2 = postureList4;
                        postureList_temp1 = postureList3;
                    }
                    else
                    {
                        postureList_temp2 = postureList2;
                        postureList_temp1 = postureList1;
                    }

                    for (int j = 1; j <= 6; j++)
                    {
                        string posname = "";
                        int num = 0;
                        if (i % 3 == 0)
                        {
                            num = random.Next(postureList_temp1.Count);
                            if (j == 1)
                            {
                                course.Pos1 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos1;
                            }
                            else if (j == 2)
                            {
                                course.Pos2 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos2;
                            }
                            else if (j == 3)
                            {
                                course.Pos3 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos3;
                            }
                            else if (j == 4)
                            {
                                course.Pos4 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos4;
                            }
                            else if (j == 5)
                            {
                                course.Pos5 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos5;
                            }
                            else if (j == 6)
                            {
                                course.Pos6 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos6;
                            }
                        }
                        else
                        {
                            if (j <= 3)
                            {
                                num = random.Next(postureList_temp1.Count);
                            }
                            else
                            {
                                num = random.Next(postureList_temp2.Count);
                            }
                            if (j == 1)
                            {
                                course.Pos1 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos1;
                            }
                            else if (j == 2)
                            {
                                course.Pos2 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos2;
                            }
                            else if (j == 3)
                            {
                                course.Pos3 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos3;
                            }
                            else if (j == 4)
                            {
                                course.Pos4 = postureList_temp2[num].Name.ToString();
                                posname = course.Pos4;
                            }
                            else if (j == 5)
                            {
                                course.Pos5 = postureList_temp2[num].Name.ToString();
                                posname = course.Pos5;
                            }
                            else if (j == 6)
                            {
                                course.Pos6 = postureList_temp2[num].Name.ToString();
                                posname = course.Pos6;
                            }
                        }
                    }
                    courseList.Add(course);
                }
            }
            #endregion

            #region 選6個部位
            if (countPart == 6)
            {
                List<Posture> postureList_temp1 = new List<Posture>();
                List<Posture> postureList_temp2 = new List<Posture>();
                List<Posture> postureList1 = postureListRep.Where(p => p.Part == partList[0]).ToList();
                List<Posture> postureList2 = postureListRep.Where(p => p.Part == partList[1]).ToList();
                List<Posture> postureList3 = postureListRep.Where(p => p.Part == partList[2]).ToList();
                List<Posture> postureList4 = postureListRep.Where(p => p.Part == partList[3]).ToList();
                List<Posture> postureList5 = postureListRep.Where(p => p.Part == partList[4]).ToList();
                List<Posture> postureList6 = postureListRep.Where(p => p.Part == partList[5]).ToList();
                for (int i = 1; i <= 6; i++)
                {
                    Course course = new Course();
                    course.C_Id = ciddate.ToString("yyyyMMdd") + "_" + data.account;
                    course.Days = i;
                    if (i % 3 == 0)
                    {
                        postureList_temp2 = postureList5;
                        postureList_temp1 = postureList6;
                    }
                    else if (i == 2 || i == 5)
                    {
                        postureList_temp2 = postureList4;
                        postureList_temp1 = postureList3;
                    }
                    else
                    {
                        postureList_temp2 = postureList2;
                        postureList_temp1 = postureList1;
                    }

                    for (int j = 1; j <= 6; j++)
                    {
                        string posname = "";
                        int num = 0;
                        if (j <= 3)
                        {
                            num = random.Next(postureList_temp1.Count);
                        }
                        else
                        {
                            num = random.Next(postureList_temp2.Count);
                        }
                        if (j == 1)
                        {
                            course.Pos1 = postureList_temp1[num].Name.ToString();
                            posname = course.Pos1;
                        }
                        else if (j == 2)
                        {
                            course.Pos2 = postureList_temp1[num].Name.ToString();
                            posname = course.Pos2;
                        }
                        else if (j == 3)
                        {
                            course.Pos3 = postureList_temp1[num].Name.ToString();
                            posname = course.Pos3;
                        }
                        else if (j == 4)
                        {
                            course.Pos4 = postureList_temp2[num].Name.ToString();
                            posname = course.Pos4;
                        }
                        else if (j == 5)
                        {
                            course.Pos5 = postureList_temp2[num].Name.ToString();
                            posname = course.Pos5;
                        }
                        else if (j == 6)
                        {
                            course.Pos6 = postureList_temp2[num].Name.ToString();
                            posname = course.Pos6;
                        }
                    }
                    courseList.Add(course);
                }
            }
            #endregion

            #region 選7個部位或複合
            if (countPart == 7 || countPart == 8)
            {
                List<Posture> postureList_temp1 = new List<Posture>();
                List<Posture> postureList_temp2 = new List<Posture>();
                List<Posture> postureList1 = postureListRep.Where(p => p.Part == "胸").ToList();
                List<Posture> postureList2 = postureListRep.Where(p => p.Part == "肱三頭").ToList();
                List<Posture> postureList3 = postureListRep.Where(p => p.Part == "背").ToList();
                List<Posture> postureList4 = postureListRep.Where(p => p.Part == "肱二頭").ToList();
                List<Posture> postureList5 = postureListRep.Where(p => p.Part == "腳").ToList();
                List<Posture> postureList6 = postureListRep.Where(p => p.Part == "肩").ToList();
                List<Posture> postureList7 = postureListRep.Where(p => p.Part == "腹").ToList();
                for (int i = 1; i <= 5; i++)
                {
                    Course course = new Course();
                    course.C_Id = ciddate.ToString("yyyyMMdd") + "_" + data.account;
                    course.Days = i;
                    if (i <= 2)
                    {
                        if (i == 1)
                        {
                            postureList_temp2 = postureList2;
                            postureList_temp1 = postureList1;
                        }
                        if (i == 2)
                        {
                            postureList_temp2 = postureList4;
                            postureList_temp1 = postureList3;
                        }
                    }
                    else
                    {
                        if (i == 3)
                        {
                            postureList_temp1 = postureList5;
                        }
                        if (i == 4)
                        {
                            postureList_temp1 = postureList6;
                        }
                        if (i == 5)
                        {
                            postureList_temp1 = postureList7;
                        }
                    }

                    for (int j = 1; j <= 6; j++)
                    {
                        string posname = "";
                        int num = 0;
                        if (i <= 2)
                        {
                            if (j <= 3)
                            {
                                num = random.Next(postureList_temp1.Count);
                            }
                            else
                            {
                                num = random.Next(postureList_temp2.Count);
                            }
                            if (j == 1)
                            {
                                course.Pos1 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos1;
                            }
                            else if (j == 2)
                            {
                                course.Pos2 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos2;
                            }
                            else if (j == 3)
                            {
                                course.Pos3 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos3;
                            }
                            else if (j == 4)
                            {
                                course.Pos4 = postureList_temp2[num].Name.ToString();
                                posname = course.Pos4;
                            }
                            else if (j == 5)
                            {
                                course.Pos5 = postureList_temp2[num].Name.ToString();
                                posname = course.Pos5;
                            }
                            else if (j == 6)
                            {
                                course.Pos6 = postureList_temp2[num].Name.ToString();
                                posname = course.Pos6;
                            }
                        }
                        else
                        {
                            num = random.Next(postureList_temp1.Count);
                            if (j == 1)
                            {
                                course.Pos1 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos1;
                            }
                            else if (j == 2)
                            {
                                course.Pos2 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos2;
                            }
                            else if (j == 3)
                            {
                                course.Pos3 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos3;
                            }
                            else if (j == 4)
                            {
                                course.Pos4 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos4;
                            }
                            else if (j == 5)
                            {
                                course.Pos5 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos5;
                            }
                            else if (j == 6)
                            {
                                course.Pos6 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos6;
                            }
                        }
                    }
                    courseList.Add(course);
                }
            }
            #endregion

            return courseList;
        }
        #endregion

        #region 儲存課程
        public List<Account_Course> SaveCourse(SaveCourseView data)
        {
            List<Account_Course> account_courseList = new List<Account_Course>();
            List<Account_Course> account_courseList_Pre = _Account_Courserepository.GetAll().ToList();
            List<Posture> postureList = _Posturerepository.GetAll().ToList();
            int series = 3;
            var startDate = data.date;
            int times = 8;
            if (data.goal == "肌耐力")
            {
                series += 2;
                times -= 3;
            }
            for (int k = 1; k <= 4; k++)
            {
                #region 訓練天數2
                if (data.courseList.Count == 2)
                {
                    for (int i = 0; i < data.courseList.Count; i++)
                    {
                        for (int j = 1; j <= 6; j++)
                        {
                            Course course = new Course();
                            course.C_Id = data.courseList[i].C_Id;
                            course.Days = data.courseList[i].Days;
                            Account_Course account_course = new Account_Course();
                            account_course.Account = data.account;
                            account_course.C_Id = data.courseList[i].C_Id;
                            account_course.Date = data.date;
                            account_course.Series = series;
                            account_course.Times = times;
                            account_course.Complete = false;
                            if (j == 1)
                            {
                                account_course.P_Name = data.courseList[i].Pos1;
                            }
                            else if (j == 2)
                            {
                                account_course.P_Name = data.courseList[i].Pos2;
                            }
                            else if (j == 3)
                            {
                                account_course.P_Name = data.courseList[i].Pos3;
                            }
                            else if (j == 4)
                            {
                                account_course.P_Name = data.courseList[i].Pos4;
                            }
                            else if (j == 5)
                            {
                                account_course.P_Name = data.courseList[i].Pos5;
                            }
                            else if (j == 6)
                            {
                                account_course.P_Name = data.courseList[i].Pos6;
                            }
                            account_course.Part = postureList.Where(p => p.Name == account_course.P_Name).Select(p => p.Part).FirstOrDefault();
                            var power = account_courseList_Pre.Where(p => p.Account == data.account && p.P_Name == account_course.P_Name).Select(p => p.Power).LastOrDefault();
                            if (power != null)
                            {
                                account_course.Power = power;
                            }
                            account_courseList.Add(account_course);
                            _Account_Courserepository.Create(account_course);
                        }
                        data.date = data.date.AddDays(3);
                        //_Courserepository.Create(data.courseList[i]);
                    }
                }
                #endregion

                #region 訓練天數4
                if (data.courseList.Count == 4)
                {
                    for (int i = 0; i < data.courseList.Count; i++)
                    {
                        for (int j = 1; j <= 6; j++)
                        {
                            Account_Course account_course = new Account_Course();
                            account_course.Account = data.account;
                            account_course.C_Id = data.courseList[i].C_Id;
                            account_course.Date = data.date;
                            account_course.Series = series;
                            account_course.Times = times;
                            account_course.Complete = false;
                            if (j == 1)
                            {
                                account_course.P_Name = data.courseList[i].Pos1;
                            }
                            else if (j == 2)
                            {
                                account_course.P_Name = data.courseList[i].Pos2;
                            }
                            else if (j == 3)
                            {
                                account_course.P_Name = data.courseList[i].Pos3;
                            }
                            else if (j == 4)
                            {
                                account_course.P_Name = data.courseList[i].Pos4;
                            }
                            else if (j == 5)
                            {
                                account_course.P_Name = data.courseList[i].Pos5;
                            }
                            else if (j == 6)
                            {
                                account_course.P_Name = data.courseList[i].Pos6;
                            }

                            account_course.Part = postureList.Where(p => p.Name == account_course.P_Name).Select(p => p.Part).FirstOrDefault();
                            var power = account_courseList_Pre.Where(p => p.Account == data.account && p.P_Name == account_course.P_Name).Select(p => p.Power).LastOrDefault();
                            if (power != null)
                            {
                                account_course.Power = power;
                            }
                            account_courseList.Add(account_course);
                            _Account_Courserepository.Create(account_course);
                        }
                        if (i == 1)
                        {
                            data.date = data.date.AddDays(2);
                        }
                        else
                        {
                            data.date = data.date.AddDays(1);
                        }
                        //_Courserepository.Create(data.courseList[i]);
                    }
                }
                #endregion

                #region 訓練天數5
                if (data.courseList.Count == 5)
                {
                    for (int i = 0; i < data.courseList.Count; i++)
                    {
                        for (int j = 1; j <= 6; j++)
                        {
                            Account_Course account_course = new Account_Course();
                            account_course.Account = data.account;
                            account_course.C_Id = data.courseList[i].C_Id;
                            account_course.Date = data.date;
                            account_course.Series = series;
                            account_course.Times = times;
                            account_course.Complete = false;
                            if (j == 1)
                            {
                                account_course.P_Name = data.courseList[i].Pos1;
                            }
                            else if (j == 2)
                            {
                                account_course.P_Name = data.courseList[i].Pos2;
                            }
                            else if (j == 3)
                            {
                                account_course.P_Name = data.courseList[i].Pos3;
                            }
                            else if (j == 4)
                            {
                                account_course.P_Name = data.courseList[i].Pos4;
                            }
                            else if (j == 5)
                            {
                                account_course.P_Name = data.courseList[i].Pos5;
                            }
                            else if (j == 6)
                            {
                                account_course.P_Name = data.courseList[i].Pos6;
                            }

                            account_course.Part = postureList.Where(p => p.Name == account_course.P_Name).Select(p => p.Part).FirstOrDefault();
                            var power = account_courseList_Pre.Where(p => p.Account == data.account && p.P_Name == account_course.P_Name).Select(p => p.Power).LastOrDefault();
                            if (power != null)
                            {
                                account_course.Power = power;
                            }
                            account_courseList.Add(account_course);
                            _Account_Courserepository.Create(account_course);
                        } 
                        if (i == 1)
                        {
                            data.date = data.date.AddDays(2);
                        }
                        else
                        {
                            data.date = data.date.AddDays(1);
                        }
                        //_Courserepository.Create(data.courseList[i]);
                    }
                }
                #endregion

                #region 訓練天數6
                if (data.courseList.Count == 6)
                {
                    for (int i = 0; i < data.courseList.Count; i++)
                    {
                        for (int j = 1; j <= 6; j++)
                        {
                            Account_Course account_course = new Account_Course();
                            account_course.Account = data.account;
                            account_course.C_Id = data.courseList[i].C_Id;
                            account_course.Date = data.date;
                            account_course.Series = series;
                            account_course.Times = times;
                            account_course.Complete = false;
                            if (j == 1)
                            {
                                account_course.P_Name = data.courseList[i].Pos1;
                            }
                            else if (j == 2)
                            {
                                account_course.P_Name = data.courseList[i].Pos2;
                            }
                            else if (j == 3)
                            {
                                account_course.P_Name = data.courseList[i].Pos3;
                            }
                            else if (j == 4)
                            {
                                account_course.P_Name = data.courseList[i].Pos4;
                            }
                            else if (j == 5)
                            {
                                account_course.P_Name = data.courseList[i].Pos5;
                            }
                            else if (j == 6)
                            {
                                account_course.P_Name = data.courseList[i].Pos6;
                            }

                            account_course.Part = postureList.Where(p => p.Name == account_course.P_Name).Select(p => p.Part).FirstOrDefault();
                            var power = account_courseList_Pre.Where(p => p.Account == data.account && p.P_Name == account_course.P_Name).Select(p => p.Power).LastOrDefault();
                            if (power != null)
                            {
                                account_course.Power = power;
                            }
                            account_courseList.Add(account_course);
                            _Account_Courserepository.Create(account_course);
                        }
                        data.date = data.date.AddDays(1);
                        //_Courserepository.Create(data.courseList[i]);
                    }
                }
                #endregion
                data.date = startDate.AddDays(7);
                startDate = data.date;
            }

            Course_Tag ct = new Course_Tag();
            ct.C_Id = data.courseList[0].C_Id;
            ct.CompleteTimes = 0;
            ct.SumDays = data.courseList.Count*4;
            ct.Goal = data.goal;
            _Course_Tagrepository.Create(ct);

            _db.Save();

            return account_courseList;
        }
        #endregion

        #region 取得姿勢介紹
        public Posture getPosData(string P_Name)
        {
            return _Posturerepository.Get(p => p.Name == P_Name);
        }
        #endregion

        #region APP

        #region 取得最近課程
        public List<Account_Course> getOneCourse(string account)
        {
            var dataAllList = _Account_Courserepository.GetAll();
            var today = DateTime.Today;
            var C_Id = dataAllList.Where(p => p.Account == account && p.Date >= today).Select(p => p.C_Id).FirstOrDefault();
            return dataAllList.Where(p => p.Account == account && p.Date >= today && p.C_Id == C_Id)
                .OrderBy(p => p.Date).Take(6).ToList();
        }
        #endregion

        #region 取得課程列表
        public List<Account_Course> getCourseList(string account)
        {
            List<Account_Course> acList = new List<Account_Course>();
            var dataAllList = _Account_Courserepository.GetAll();
            var today = DateTime.Today;
            var C_Id = dataAllList.Where(p => p.Account == account && p.Date >= today).Select(p => p.C_Id).FirstOrDefault();
            var dataList = dataAllList.Where(p => p.Account == account && p.C_Id == C_Id)
                .OrderBy(p => p.Date).GroupBy(p => new { p.C_Id, p.Date }).ToList();
            foreach(var item in dataList){
                Account_Course ac = new Account_Course();
                ac.C_Id = item.Key.C_Id;
                ac.Date = item.Key.Date;
                acList.Add(ac);
            }
            return acList;
        }
        #endregion

        #region 取得課程內容
        public List<Account_Course> getCourseDetail(string account, string C_Id, DateTime Date)
        {
            var dataAllList = _Account_Courserepository.GetAll();
            return dataAllList.Where(p => p.Account == account && p.C_Id == C_Id && p.Date == Date).ToList();
        }
        #endregion

        #region 產生一週課程
        public List<Course> CreateCourseAPP(CourseSelectView data)
        {
            List<Course> courseList = new List<Course>();
            var ciddate = DateTime.Today;
            List<string> partList = new List<string>();
            List<string> deviceList = new List<string>();
            deviceList.Add(null);
            int countPart = 0;
            int countDevice = 0;
            Random random = new Random();
            IEnumerable<Posture> postureAllList = _Posturerepository.GetAll();

            if (data.complex == "複合")
            {
                countPart = 8;
            }
            else
            {
                if (data.chest == "胸")
                {
                    countPart++;
                    partList.Add(data.chest);
                }
                if (data.three == "肱三頭")
                {
                    countPart++;
                    partList.Add(data.three);
                }
                if (data.back == "背")
                {
                    countPart++;
                    partList.Add(data.back);
                }
                if (data.two == "肱二頭")
                {
                    countPart++;
                    partList.Add(data.two);
                }
                if (data.foot == "腳")
                {
                    countPart++;
                    partList.Add(data.foot);
                }
                if (data.shoulder == "肩")
                {
                    countPart++;
                    partList.Add(data.shoulder);
                }
                if (data.belly == "腹")
                {
                    countPart++;
                    partList.Add(data.belly);
                }
            }

            if (data.TBar == "T-Bar划船訓練機")
            {
                countDevice++;
                deviceList.Add(data.TBar);
            }
            if (data.WBarbell == "W槓")
            {
                countDevice++;
                deviceList.Add(data.WBarbell);
            }
            if (data.Barbell == "槓鈴")
            {
                countDevice++;
                deviceList.Add(data.Barbell);
            }
            if (data.Dumbbell == "啞鈴")
            {
                countDevice++;
                deviceList.Add(data.Dumbbell);
            }
            if (data.Smith == "史密斯機架")
            {
                countDevice++;
                deviceList.Add(data.Smith);
            }
            if (data.LegPress == "腿舉機")
            {
                countDevice++;
                deviceList.Add(data.LegPress);
            }
            if (data.Butterfly == "蝴蝶機")
            {
                countDevice++;
                deviceList.Add(data.Butterfly);
            }
            if (data.PullBack == "拉背機")
            {
                countDevice++;
                deviceList.Add(data.PullBack);
            }
            if (data.LegExtension == "腿部伸展機")
            {
                countDevice++;
                deviceList.Add(data.LegExtension);
            }
            if (data.PullUp == "引體上升器")
            {
                countDevice++;
                deviceList.Add(data.PullUp);
            }
            if (data.Squat == "蹲舉機")
            {
                countDevice++;
                deviceList.Add(data.Squat);
            }
            if (data.Pulley == "滑輪機和滑輪握把")
            {
                countDevice++;
                deviceList.Add(data.Pulley);
            }
            if (data.Boating == "划船機")
            {
                countDevice++;
                deviceList.Add(data.Boating);
            }
            if (data.PulleyChest == "滑輪擴胸機")
            {
                countDevice++;
                deviceList.Add(data.PulleyChest);
            }
            if (data.WeightBench == "舉重椅")
            {
                countDevice++;
                deviceList.Add(data.WeightBench);
            }
            if (data.ParallelBars == "雙槓")
            {
                countDevice++;
                deviceList.Add(data.ParallelBars);
            }
            if (data.Cross == "交叉訓練機")
            {
                countDevice++;
                deviceList.Add(data.Cross);
            }

            List<Posture> postureListRep = new List<Posture>();
            foreach (var item in deviceList)
            {
                var postureData = postureAllList.Where(p => p.Device == item).ToList();
                foreach (var item2 in postureData)
                {
                    postureListRep.Add(item2);
                }
            }

            #region 選1個部位
            if (countPart == 1)
            {
                List<Posture> postureList = postureListRep.Where(p => p.Part == partList[0]).ToList();
                for (int i = 1; i <= 2; i++)
                {
                    Course course = new Course();
                    course.C_Id = ciddate.ToString("yyyyMMdd") + "_" + data.account;
                    course.Days = i;
                    for (int j = 1; j <= 6; j++)
                    {
                        string posname = "";
                        int num = random.Next(postureList.Count);
                        if (j == 1)
                        {
                            course.Pos1 = postureList[num].Name.ToString();
                            posname = course.Pos1;
                        }
                        else if (j == 2)
                        {
                            course.Pos2 = postureList[num].Name.ToString();
                            posname = course.Pos2;
                        }
                        else if (j == 3)
                        {
                            course.Pos3 = postureList[num].Name.ToString();
                            posname = course.Pos3;
                        }
                        else if (j == 4)
                        {
                            course.Pos4 = postureList[num].Name.ToString();
                            posname = course.Pos4;
                        }
                        else if (j == 5)
                        {
                            course.Pos5 = postureList[num].Name.ToString();
                            posname = course.Pos5;
                        }
                        else if (j == 6)
                        {
                            course.Pos6 = postureList[num].Name.ToString();
                            posname = course.Pos6;
                        }
                    }
                    courseList.Add(course);
                    _Courserepository.Create(course);
                }
            }
            #endregion

            #region 選2個部位
            if (countPart == 2)
            {
                List<Posture> postureList = new List<Posture>();
                List<Posture> postureList1 = postureListRep.Where(p => p.Part == partList[0]).ToList();
                List<Posture> postureList2 = postureListRep.Where(p => p.Part == partList[1]).ToList();
                for (int i = 1; i <= 4; i++)
                {
                    Course course = new Course();
                    course.C_Id = ciddate.ToString("yyyyMMdd") + "_" + data.account;
                    course.Days = i;
                    if (i % 2 == 0)
                    {
                        postureList = postureList2;
                    }
                    else
                    {
                        postureList = postureList1;
                    }

                    for (int j = 1; j <= 6; j++)
                    {
                        string posname = "";
                        int num = random.Next(postureList.Count);
                        if (j == 1)
                        {
                            course.Pos1 = postureList[num].Name.ToString();
                            posname = course.Pos1;
                        }
                        else if (j == 2)
                        {
                            course.Pos2 = postureList[num].Name.ToString();
                            posname = course.Pos2;
                        }
                        else if (j == 3)
                        {
                            course.Pos3 = postureList[num].Name.ToString();
                            posname = course.Pos3;
                        }
                        else if (j == 4)
                        {
                            course.Pos4 = postureList[num].Name.ToString();
                            posname = course.Pos4;
                        }
                        else if (j == 5)
                        {
                            course.Pos5 = postureList[num].Name.ToString();
                            posname = course.Pos5;
                        }
                        else if (j == 6)
                        {
                            course.Pos6 = postureList[num].Name.ToString();
                            posname = course.Pos6;
                        }
                    }
                    courseList.Add(course);
                    _Courserepository.Create(course);
                }
            }
            #endregion

            #region 選3個部位
            if (countPart == 3)
            {
                List<Posture> postureList = new List<Posture>();
                List<Posture> postureList1 = postureListRep.Where(p => p.Part == partList[0]).ToList();
                List<Posture> postureList2 = postureListRep.Where(p => p.Part == partList[1]).ToList();
                List<Posture> postureList3 = postureListRep.Where(p => p.Part == partList[2]).ToList();
                for (int i = 1; i <= 6; i++)
                {
                    Course course = new Course();
                    course.C_Id = ciddate.ToString("yyyyMMdd") + "_" + data.account;
                    course.Days = i;
                    if (i % 3 == 0)
                    {
                        postureList = postureList3;
                    }
                    else if (i == 2 || i == 5)
                    {
                        postureList = postureList2;
                    }
                    else
                    {
                        postureList = postureList1;
                    }

                    for (int j = 1; j <= 6; j++)
                    {
                        string posname = "";
                        int num = random.Next(postureList.Count);
                        if (j == 1)
                        {
                            course.Pos1 = postureList[num].Name.ToString();
                            posname = course.Pos1;
                        }
                        else if (j == 2)
                        {
                            course.Pos2 = postureList[num].Name.ToString();
                            posname = course.Pos2;
                        }
                        else if (j == 3)
                        {
                            course.Pos3 = postureList[num].Name.ToString();
                            posname = course.Pos3;
                        }
                        else if (j == 4)
                        {
                            course.Pos4 = postureList[num].Name.ToString();
                            posname = course.Pos4;
                        }
                        else if (j == 5)
                        {
                            course.Pos5 = postureList[num].Name.ToString();
                            posname = course.Pos5;
                        }
                        else if (j == 6)
                        {
                            course.Pos6 = postureList[num].Name.ToString();
                            posname = course.Pos6;
                        }
                    }
                    courseList.Add(course);
                    _Courserepository.Create(course);
                }
            }
            #endregion

            #region 選4個部位
            if (countPart == 4)
            {
                List<Posture> postureList_temp1 = new List<Posture>();
                List<Posture> postureList_temp2 = new List<Posture>();
                List<Posture> postureList1 = postureListRep.Where(p => p.Part == partList[0]).ToList();
                List<Posture> postureList2 = postureListRep.Where(p => p.Part == partList[1]).ToList();
                List<Posture> postureList3 = postureListRep.Where(p => p.Part == partList[2]).ToList();
                List<Posture> postureList4 = postureListRep.Where(p => p.Part == partList[3]).ToList();
                for (int i = 1; i <= 4; i++)
                {
                    Course course = new Course();
                    course.C_Id = ciddate.ToString("yyyyMMdd") + "_" + data.account;
                    course.Days = i;
                    if (i % 2 == 0)
                    {
                        postureList_temp2 = postureList4;
                        postureList_temp1 = postureList3;
                    }
                    else
                    {
                        postureList_temp2 = postureList2;
                        postureList_temp1 = postureList1;
                    }

                    for (int j = 1; j <= 6; j++)
                    {
                        string posname = "";
                        int num = 0;
                        if (j <= 3)
                        {
                            num = random.Next(postureList_temp1.Count);

                        }
                        else
                        {
                            num = random.Next(postureList_temp2.Count);

                        }
                        if (j == 1)
                        {
                            course.Pos1 = postureList_temp1[num].Name.ToString();
                            posname = course.Pos1;
                        }
                        else if (j == 2)
                        {
                            course.Pos2 = postureList_temp1[num].Name.ToString();
                            posname = course.Pos2;
                        }
                        else if (j == 3)
                        {
                            course.Pos3 = postureList_temp1[num].Name.ToString();
                            posname = course.Pos3;
                        }
                        else if (j == 4)
                        {
                            course.Pos4 = postureList_temp2[num].Name.ToString();
                            posname = course.Pos4;
                        }
                        else if (j == 5)
                        {
                            course.Pos5 = postureList_temp2[num].Name.ToString();
                            posname = course.Pos5;
                        }
                        else if (j == 6)
                        {
                            course.Pos6 = postureList_temp2[num].Name.ToString();
                            posname = course.Pos6;
                        }
                    }
                    courseList.Add(course);
                    _Courserepository.Create(course);
                }
            }
            #endregion

            #region 選5個部位
            if (countPart == 5)
            {
                List<Posture> postureList_temp1 = new List<Posture>();
                List<Posture> postureList_temp2 = new List<Posture>();
                List<Posture> postureList1 = postureListRep.Where(p => p.Part == partList[0]).ToList();
                List<Posture> postureList2 = postureListRep.Where(p => p.Part == partList[1]).ToList();
                List<Posture> postureList3 = postureListRep.Where(p => p.Part == partList[2]).ToList();
                List<Posture> postureList4 = postureListRep.Where(p => p.Part == partList[3]).ToList();
                List<Posture> postureList5 = postureListRep.Where(p => p.Part == partList[4]).ToList();
                for (int i = 1; i <= 6; i++)
                {
                    Course course = new Course();
                    course.C_Id = ciddate.ToString("yyyyMMdd") + "_" + data.account;
                    course.Days = i;
                    if (i % 3 == 0)
                    {
                        postureList_temp1 = postureList5;
                    }
                    else if (i == 2 || i == 5)
                    {
                        postureList_temp2 = postureList4;
                        postureList_temp1 = postureList3;
                    }
                    else
                    {
                        postureList_temp2 = postureList2;
                        postureList_temp1 = postureList1;
                    }

                    for (int j = 1; j <= 6; j++)
                    {
                        string posname = "";
                        int num = 0;
                        if (i % 3 == 0)
                        {
                            num = random.Next(postureList_temp1.Count);
                            if (j == 1)
                            {
                                course.Pos1 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos1;
                            }
                            else if (j == 2)
                            {
                                course.Pos2 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos2;
                            }
                            else if (j == 3)
                            {
                                course.Pos3 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos3;
                            }
                            else if (j == 4)
                            {
                                course.Pos4 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos4;
                            }
                            else if (j == 5)
                            {
                                course.Pos5 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos5;
                            }
                            else if (j == 6)
                            {
                                course.Pos6 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos6;
                            }
                        }
                        else
                        {
                            if (j <= 3)
                            {
                                num = random.Next(postureList_temp1.Count);
                            }
                            else
                            {
                                num = random.Next(postureList_temp2.Count);
                            }
                            if (j == 1)
                            {
                                course.Pos1 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos1;
                            }
                            else if (j == 2)
                            {
                                course.Pos2 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos2;
                            }
                            else if (j == 3)
                            {
                                course.Pos3 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos3;
                            }
                            else if (j == 4)
                            {
                                course.Pos4 = postureList_temp2[num].Name.ToString();
                                posname = course.Pos4;
                            }
                            else if (j == 5)
                            {
                                course.Pos5 = postureList_temp2[num].Name.ToString();
                                posname = course.Pos5;
                            }
                            else if (j == 6)
                            {
                                course.Pos6 = postureList_temp2[num].Name.ToString();
                                posname = course.Pos6;
                            }
                        }
                    }
                    courseList.Add(course);
                    _Courserepository.Create(course);
                }
            }
            #endregion

            #region 選6個部位
            if (countPart == 6)
            {
                List<Posture> postureList_temp1 = new List<Posture>();
                List<Posture> postureList_temp2 = new List<Posture>();
                List<Posture> postureList1 = postureListRep.Where(p => p.Part == partList[0]).ToList();
                List<Posture> postureList2 = postureListRep.Where(p => p.Part == partList[1]).ToList();
                List<Posture> postureList3 = postureListRep.Where(p => p.Part == partList[2]).ToList();
                List<Posture> postureList4 = postureListRep.Where(p => p.Part == partList[3]).ToList();
                List<Posture> postureList5 = postureListRep.Where(p => p.Part == partList[4]).ToList();
                List<Posture> postureList6 = postureListRep.Where(p => p.Part == partList[5]).ToList();
                for (int i = 1; i <= 6; i++)
                {
                    Course course = new Course();
                    course.C_Id = ciddate.ToString("yyyyMMdd") + "_" + data.account;
                    course.Days = i;
                    if (i % 3 == 0)
                    {
                        postureList_temp2 = postureList5;
                        postureList_temp1 = postureList6;
                    }
                    else if (i == 2 || i == 5)
                    {
                        postureList_temp2 = postureList4;
                        postureList_temp1 = postureList3;
                    }
                    else
                    {
                        postureList_temp2 = postureList2;
                        postureList_temp1 = postureList1;
                    }

                    for (int j = 1; j <= 6; j++)
                    {
                        string posname = "";
                        int num = 0;
                        if (j <= 3)
                        {
                            num = random.Next(postureList_temp1.Count);
                        }
                        else
                        {
                            num = random.Next(postureList_temp2.Count);
                        }
                        if (j == 1)
                        {
                            course.Pos1 = postureList_temp1[num].Name.ToString();
                            posname = course.Pos1;
                        }
                        else if (j == 2)
                        {
                            course.Pos2 = postureList_temp1[num].Name.ToString();
                            posname = course.Pos2;
                        }
                        else if (j == 3)
                        {
                            course.Pos3 = postureList_temp1[num].Name.ToString();
                            posname = course.Pos3;
                        }
                        else if (j == 4)
                        {
                            course.Pos4 = postureList_temp2[num].Name.ToString();
                            posname = course.Pos4;
                        }
                        else if (j == 5)
                        {
                            course.Pos5 = postureList_temp2[num].Name.ToString();
                            posname = course.Pos5;
                        }
                        else if (j == 6)
                        {
                            course.Pos6 = postureList_temp2[num].Name.ToString();
                            posname = course.Pos6;
                        }
                    }
                    courseList.Add(course);
                    _Courserepository.Create(course);
                }
            }
            #endregion

            #region 選7個部位或複合
            if (countPart == 7 || countPart == 8)
            {
                List<Posture> postureList_temp1 = new List<Posture>();
                List<Posture> postureList_temp2 = new List<Posture>();
                List<Posture> postureList1 = postureListRep.Where(p => p.Part == "胸").ToList();
                List<Posture> postureList2 = postureListRep.Where(p => p.Part == "肱三頭").ToList();
                List<Posture> postureList3 = postureListRep.Where(p => p.Part == "背").ToList();
                List<Posture> postureList4 = postureListRep.Where(p => p.Part == "肱二頭").ToList();
                List<Posture> postureList5 = postureListRep.Where(p => p.Part == "腳").ToList();
                List<Posture> postureList6 = postureListRep.Where(p => p.Part == "肩").ToList();
                List<Posture> postureList7 = postureListRep.Where(p => p.Part == "腹").ToList();
                for (int i = 1; i <= 5; i++)
                {
                    Course course = new Course();
                    course.C_Id = ciddate.ToString("yyyyMMdd") + "_" + data.account;
                    course.Days = i;
                    if (i <= 2)
                    {
                        if (i == 1)
                        {
                            postureList_temp2 = postureList2;
                            postureList_temp1 = postureList1;
                        }
                        if (i == 2)
                        {
                            postureList_temp2 = postureList4;
                            postureList_temp1 = postureList3;
                        }
                    }
                    else
                    {
                        if (i == 3)
                        {
                            postureList_temp1 = postureList5;
                        }
                        if (i == 4)
                        {
                            postureList_temp1 = postureList6;
                        }
                        if (i == 5)
                        {
                            postureList_temp1 = postureList7;
                        }
                    }

                    for (int j = 1; j <= 6; j++)
                    {
                        string posname = "";
                        int num = 0;
                        if (i <= 2)
                        {
                            if (j <= 3)
                            {
                                num = random.Next(postureList_temp1.Count);
                            }
                            else
                            {
                                num = random.Next(postureList_temp2.Count);
                            }
                            if (j == 1)
                            {
                                course.Pos1 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos1;
                            }
                            else if (j == 2)
                            {
                                course.Pos2 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos2;
                            }
                            else if (j == 3)
                            {
                                course.Pos3 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos3;
                            }
                            else if (j == 4)
                            {
                                course.Pos4 = postureList_temp2[num].Name.ToString();
                                posname = course.Pos4;
                            }
                            else if (j == 5)
                            {
                                course.Pos5 = postureList_temp2[num].Name.ToString();
                                posname = course.Pos5;
                            }
                            else if (j == 6)
                            {
                                course.Pos6 = postureList_temp2[num].Name.ToString();
                                posname = course.Pos6;
                            }
                        }
                        else
                        {
                            num = random.Next(postureList_temp1.Count);
                            if (j == 1)
                            {
                                course.Pos1 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos1;
                            }
                            else if (j == 2)
                            {
                                course.Pos2 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos2;
                            }
                            else if (j == 3)
                            {
                                course.Pos3 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos3;
                            }
                            else if (j == 4)
                            {
                                course.Pos4 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos4;
                            }
                            else if (j == 5)
                            {
                                course.Pos5 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos5;
                            }
                            else if (j == 6)
                            {
                                course.Pos6 = postureList_temp1[num].Name.ToString();
                                posname = course.Pos6;
                            }
                        }
                    }
                    courseList.Add(course);
                    _Courserepository.Create(course);
                }
            }
            #endregion
            _db.Save();
            return courseList;
        }
        #endregion

        #region 刪除課程
        public void DeleteCourseAPP(string C_Id)
        {
            var dataAllList = _Courserepository.GetAll();
            List<Course> courseList = dataAllList.Where(p => p.C_Id == C_Id).ToList();
            foreach (var item in courseList)
            {
                _Courserepository.Delete(item);
            }
            _db.Save();
        }
        #endregion

        #region 儲存課程
        public List<Account_Course> SaveCourseAPP(SaveCourseViewAPP data)
        {
            List<Account_Course> account_courseList = new List<Account_Course>();
            List<Account_Course> account_courseList_Pre = _Account_Courserepository.GetAll().ToList();
            List<Posture> postureList = _Posturerepository.GetAll().ToList();
            var courseData = _Courserepository.GetAll();
            List<Course> courseList = courseData.Where(p => p.C_Id == data.C_Id).ToList();
            int series = 3;
            var startDate = data.date;
            int times = 8;
            if (data.goal == "肌耐力")
            {
                series += 2;
                times -= 3;
            }
            for (int k = 1; k <= 4; k++)
            {
                #region 訓練天數2
                if (courseList.Count == 2)
                {
                    for (int i = 0; i < courseList.Count; i++)
                    {
                        for (int j = 1; j <= 6; j++)
                        {
                            Account_Course account_course = new Account_Course();
                            account_course.Id = j;
                            account_course.Account = data.account;
                            account_course.C_Id = courseList[i].C_Id;
                            account_course.Date = data.date;
                            account_course.Series = series;
                            account_course.Times = times;
                            account_course.Complete = false;
                            if (j == 1)
                            {
                                account_course.P_Name = courseList[i].Pos1;
                            }
                            else if (j == 2)
                            {
                                account_course.P_Name = courseList[i].Pos2;
                            }
                            else if (j == 3)
                            {
                                account_course.P_Name = courseList[i].Pos3;
                            }
                            else if (j == 4)
                            {
                                account_course.P_Name = courseList[i].Pos4;
                            }
                            else if (j == 5)
                            {
                                account_course.P_Name = courseList[i].Pos5;
                            }
                            else if (j == 6)
                            {
                                account_course.P_Name = courseList[i].Pos6;
                            }
                            account_course.Part = postureList.Where(p => p.Name == account_course.P_Name).Select(p => p.Part).FirstOrDefault();
                            var power = account_courseList_Pre.Where(p => p.Account == data.account && p.P_Name == account_course.P_Name).Select(p => p.Power).LastOrDefault();
                            if (power != null)
                            {
                                account_course.Power = power;
                            }
                            account_courseList.Add(account_course);
                            _Account_Courserepository.Create(account_course);
                        }
                        data.date = data.date.AddDays(3);
                    }
                }
                #endregion

                #region 訓練天數4
                if (courseList.Count == 4)
                {
                    for (int i = 0; i < courseList.Count; i++)
                    {
                        for (int j = 1; j <= 6; j++)
                        {
                            Account_Course account_course = new Account_Course();
                            account_course.Id = j;
                            account_course.Account = data.account;
                            account_course.C_Id = courseList[i].C_Id;
                            account_course.Date = data.date;
                            account_course.Series = series;
                            account_course.Times = times;
                            account_course.Complete = false;
                            if (j == 1)
                            {
                                account_course.P_Name = courseList[i].Pos1;
                            }
                            else if (j == 2)
                            {
                                account_course.P_Name = courseList[i].Pos2;
                            }
                            else if (j == 3)
                            {
                                account_course.P_Name = courseList[i].Pos3;
                            }
                            else if (j == 4)
                            {
                                account_course.P_Name = courseList[i].Pos4;
                            }
                            else if (j == 5)
                            {
                                account_course.P_Name = courseList[i].Pos5;
                            }
                            else if (j == 6)
                            {
                                account_course.P_Name = courseList[i].Pos6;
                            }

                            account_course.Part = postureList.Where(p => p.Name == account_course.P_Name).Select(p => p.Part).FirstOrDefault();
                            var power = account_courseList_Pre.Where(p => p.Account == data.account && p.P_Name == account_course.P_Name).Select(p => p.Power).LastOrDefault();
                            if (power != null)
                            {
                                account_course.Power = power;
                            }
                            account_courseList.Add(account_course);
                            _Account_Courserepository.Create(account_course);
                        }
                        if (i == 1)
                        {
                            data.date = data.date.AddDays(2);
                        }
                        else
                        {
                            data.date = data.date.AddDays(1);
                        }
                    }
                }
                #endregion

                #region 訓練天數5
                if (courseList.Count == 5)
                {
                    for (int i = 0; i < courseList.Count; i++)
                    {
                        for (int j = 1; j <= 6; j++)
                        {
                            Account_Course account_course = new Account_Course();
                            account_course.Id = j;
                            account_course.Account = data.account;
                            account_course.C_Id = courseList[i].C_Id;
                            account_course.Date = data.date;
                            account_course.Series = series;
                            account_course.Times = times;
                            account_course.Complete = false;
                            if (j == 1)
                            {
                                account_course.P_Name = courseList[i].Pos1;
                            }
                            else if (j == 2)
                            {
                                account_course.P_Name = courseList[i].Pos2;
                            }
                            else if (j == 3)
                            {
                                account_course.P_Name = courseList[i].Pos3;
                            }
                            else if (j == 4)
                            {
                                account_course.P_Name = courseList[i].Pos4;
                            }
                            else if (j == 5)
                            {
                                account_course.P_Name = courseList[i].Pos5;
                            }
                            else if (j == 6)
                            {
                                account_course.P_Name = courseList[i].Pos6;
                            }

                            account_course.Part = postureList.Where(p => p.Name == account_course.P_Name).Select(p => p.Part).FirstOrDefault();
                            var power = account_courseList_Pre.Where(p => p.Account == data.account && p.P_Name == account_course.P_Name).Select(p => p.Power).LastOrDefault();
                            if (power != null)
                            {
                                account_course.Power = power;
                            }
                            account_courseList.Add(account_course);
                            _Account_Courserepository.Create(account_course);
                        }
                        if (i == 1)
                        {
                            data.date = data.date.AddDays(2);
                        }
                        else
                        {
                            data.date = data.date.AddDays(1);
                        }
                    }
                }
                #endregion

                #region 訓練天數6
                if (courseList.Count == 6)
                {
                    for (int i = 0; i < courseList.Count; i++)
                    {
                        for (int j = 1; j <= 6; j++)
                        {
                            Account_Course account_course = new Account_Course();
                            account_course.Id = j;
                            account_course.Account = data.account;
                            account_course.C_Id = courseList[i].C_Id;
                            account_course.Date = data.date;
                            account_course.Series = series;
                            account_course.Times = times;
                            account_course.Complete = false;
                            if (j == 1)
                            {
                                account_course.P_Name = courseList[i].Pos1;
                            }
                            else if (j == 2)
                            {
                                account_course.P_Name = courseList[i].Pos2;
                            }
                            else if (j == 3)
                            {
                                account_course.P_Name = courseList[i].Pos3;
                            }
                            else if (j == 4)
                            {
                                account_course.P_Name = courseList[i].Pos4;
                            }
                            else if (j == 5)
                            {
                                account_course.P_Name = courseList[i].Pos5;
                            }
                            else if (j == 6)
                            {
                                account_course.P_Name = courseList[i].Pos6;
                            }

                            account_course.Part = postureList.Where(p => p.Name == account_course.P_Name).Select(p => p.Part).FirstOrDefault();
                            var power = account_courseList_Pre.Where(p => p.Account == data.account && p.P_Name == account_course.P_Name).Select(p => p.Power).LastOrDefault();
                            if (power != null)
                            {
                                account_course.Power = power;
                            }
                            account_courseList.Add(account_course);
                            _Account_Courserepository.Create(account_course);
                        }
                        data.date = data.date.AddDays(1);
                    }
                }
                #endregion
                data.date = startDate.AddDays(7);
                startDate = data.date;
            }

            Course_Tag ct = new Course_Tag();
            ct.C_Id = courseList[0].C_Id;
            ct.CompleteTimes = 0;
            ct.SumDays = courseList.Count*4;
            ct.Goal = data.goal;
            _Course_Tagrepository.Create(ct);

            _db.Save();

            return account_courseList;
        }
        #endregion

        #region 儲存重量
        public void savePower(string account, string C_Id, string P_Name, int? Power)
        {
            var dataAllList = _Account_Courserepository.GetAll();
            List<Account_Course> acList = dataAllList.Where(p => p.Account == account && p.C_Id == C_Id && p.P_Name == P_Name).ToList();
            foreach (var item in acList)
            {
                item.Power = Power;
                _Account_Courserepository.Update(item);
            }
            _db.Save();
        }
        #endregion

        #region 訓練結束
        public void endTrain(Account_Course data)
        {
            #region 儲存課程完成
            var dataAll = _Account_Courserepository.GetAll();
            Account_Course ac = dataAll.Where(p => p.Id == data.Id 
                                                && p.Account == data.Account 
                                                && p.C_Id == data.C_Id 
                                                && p.Date == data.Date 
                                                && p.P_Name == data.P_Name).FirstOrDefault();
            ac.Complete = true;
            _Account_Courserepository.Update(ac);
            #endregion

            #region 儲存成效表
            RecordEffect re = new RecordEffect();
            re.Id = data.Id;
            re.Account = data.Account;
            re.C_Id = data.C_Id;
            re.Date = data.Date;
            re.P_Name = data.P_Name;
            re.Part = data.Part;
            
            var ct = _Course_Tagrepository.Get(p => p.C_Id == data.C_Id);
            var EMGAlldata = _EMGrepository.GetAll();
            var EMGdataList = EMGAlldata.Where(p => p.Account == data.Account
                                                    && p.C_Id == data.C_Id
                                                    && p.Date == data.Date
                                                    && p.P_Name == data.P_Name).ToList();
            double sumPMVC = 0;
            int countEffectPMVC = 0;
            int countUsefulPMVC = 0;
            foreach (var item in EMGdataList)
            {
                //有效出力
                if (item.PMVC >= 80 && item.PMVC < 100)
                {
                    countEffectPMVC++;
                }
                //有用出力
                if (item.PMVC < 100)
                {
                    countUsefulPMVC++;
                    sumPMVC += item.PMVC;
                }
            }
            re.Effect = (countEffectPMVC / countUsefulPMVC) * 100;

            if (ct.Goal == "肌力")
            {
                re.Strength = data.Power * (100 / (sumPMVC / countUsefulPMVC));
            }
            if (ct.Goal == "肌耐力")
            {
                re.Endurance = data.Power * (70 / (sumPMVC / countUsefulPMVC));
            }

            _RecordEffectrepository.Create(re);
            #endregion

            #region 依照%MVC改變重量
            /*var today = DateTime.Today;
            var acList = dataAll.Where(p => p.Account == data.Account
                                            && p.C_Id == data.C_Id
                                            && p.P_Name == data.P_Name
                                            && p.Date > today).ToList();
            int power = Convert.ToInt32(data.Power * (80 / (sumPMVC / countUsefulPMVC)));
            foreach (var item in acList)
            {
                item.Power = power;
                _Account_Courserepository.Update(item);
            }*/

            #endregion

            _db.Save();
        }

        #endregion

        #endregion
    }
}