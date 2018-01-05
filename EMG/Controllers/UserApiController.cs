using AutoMapper;
using EMG.Interface;
using EMG.Model;
using Microsoft.AspNet.Identity;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EMG.Controllers
{
    public class UserApiController : ApiController
    {
        private IUnitOfWork _db;
        private IRepository<User> _repository;
        private IUserServices _userservice;
        private IRepository<UserHabit> _userHabitrepository;

        public UserApiController(IUnitOfWork uow, IRepository<User> repository, IUserServices userservice, IRepository<UserHabit> userHabitrepository)
        {
            this._db = uow;
            this._repository = repository;
            this._userservice = userservice;
            this._userHabitrepository = userHabitrepository;
        }

        #region 註冊

        public void Register(RegisterView registerUser)
        {
            User newUser = Mapper.Map<User>(registerUser);
            DateTime today = DateTime.Today;
            int age = today.Year - newUser.Birth.Year;
            if (newUser.Birth > today.AddYears(-age)) age--;
            Double BMI = newUser.Weight / ((newUser.Height / 100) * (newUser.Height / 100));
            //HashPassword
            newUser.Password = _userservice.HashPassword(newUser.Password);
            newUser.Age = age;
            if (newUser.Sex == "男")
            {
                newUser.BMR = newUser.Weight * 13.7 + newUser.Height * 5.0 - (6.8 * age) + 66;
                newUser.Bodyfat = (1.2 * BMI) + (0.23 * age) - 5.4 - (10.8 * 1);
            }
            else if (newUser.Sex == "女")
            {
                newUser.BMR = newUser.Weight * 9.6 + newUser.Height * 1.8 - (4.7 * age) + 655;
                newUser.Bodyfat = (1.2 * BMI) + (0.23 * age) - 5.4 - (10.8 * 0);
            }
            UserHabit newHabit = Mapper.Map<UserHabit>(registerUser);
            _repository.Create(newUser);
            newHabit.CreateTime = DateTime.Now;
            _userHabitrepository.Create(newHabit);
            _db.Save();
            //_userservice.Register(newUser, null, null, null, 0, newHabit);, fileName, type, path, size, newHabit);
        }

        public string AccountCheck(AccountCheckView Account)
        {
            string result;
            if (_userservice.AccountCheck(Account.Account))
                result = "true";
            else
                result = "false";
            return result;
        }

        #endregion

        #region 修改會員資料

        public void updateProfile(ProfileView profileUser)
        {
            profileUser.Account = User.Identity.Name;
            var nowUser = Mapper.Map<User>(profileUser);
            _userservice.Update(nowUser);
        }

        #endregion

        #region 登入
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public Dictionary<string, string> Login(LoginView loginUser)
        {
            var checkStr = _userservice.LoginCheck(loginUser.Account, loginUser.Password);
            var userPhoto = "";
            var userName = "";
            if (checkStr == "")
            {
                userName = _userservice.GetNameByAccount(loginUser.Account);
                userPhoto = _userservice.Get(loginUser.Account).Url;

                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, loginUser.Account)
                }, "ApplicationCookie");

                var ctx = HttpContext.Current.GetOwinContext();//Request.GetOwinContext();
                var authManager = ctx.Authentication;
                authManager.SignIn(identity);
            }
            var result = new Dictionary<string, string>()
            {
                {"checkStr",checkStr },
                { "userName",userName},
                { "userPhoto",userPhoto }
            };
            return result;
        }

        #endregion

        #region 取得用戶資料

        [System.Web.Http.HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public RegisterView getUser([FromBody]PostId data)
        {
            User user = _userservice.Get(data.account);
            RegisterView nowUser = Mapper.Map<RegisterView>(user);
            return nowUser;
        }

        #endregion

        #region 取得用戶問卷

        [System.Web.Http.HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public RegisterView getHabit([FromBody]PostId data)
        {
            UserHabit userHabit = _userservice.GetUserHabit(data.account);
            RegisterView nowUser = Mapper.Map<RegisterView>(userHabit);
            return nowUser;
        }

        #endregion

        #region 登出

        [System.Web.Http.HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "GET,OPTIONS")]
        public void LogOut()
        {
            var ctx = HttpContext.Current.Request.GetOwinContext();
            var authManager = ctx.Authentication;
            authManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        #endregion

        #region APP

        #region 註冊

        public void RegisterAPP(RegisterView registerUser)
        {
            User newUser = Mapper.Map<User>(registerUser);
            DateTime today = DateTime.Today;
            int age = today.Year - newUser.Birth.Year;
            if (newUser.Birth > today.AddYears(-age)) age--;
            Double BMI = newUser.Weight / ((newUser.Height / 100) * (newUser.Height / 100));
            //HashPassword
            newUser.Password = _userservice.HashPassword(newUser.Password);
            newUser.Age = age;
            if (newUser.Sex == "男")
            {
                newUser.BMR = newUser.Weight * 13.7 + newUser.Height * 5.0 - (6.8 * age) + 66;
                newUser.Bodyfat = (1.2 * BMI) + (0.23 * age) - 5.4 - (10.8 * 1);
            }
            else if (newUser.Sex == "女")
            {
                newUser.BMR = newUser.Weight * 9.6 + newUser.Height * 1.8 - (4.7 * age) + 655;
                newUser.Bodyfat = (1.2 * BMI) + (0.23 * age) - 5.4 - (10.8 * 0);
            }
            _repository.Create(newUser);
            _db.Save();
        }

        #endregion

        #region 儲存問卷

        public void saveHabit(UserHabit data)
        {
            _userservice.saveHabit(data);
        }

        #endregion

        #region 取得個人資料

        [System.Web.Http.HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public ProfileView getProfile(PostId account)
        {
            User nowUser = _userservice.Get(account.account);
            ProfileView profileUser = Mapper.Map<ProfileView>(nowUser);
            return profileUser;
        }

        #endregion

        #region 修改個人資料

        public void editProfile(ProfileView data)
        {
            _userservice.editProfile(data);
        }

        #endregion

        #region 修改密碼

        public void ChangePasswordWord(User data)
        {
            User user = _repository.Get(p => p.Account == data.Account);
            _userservice.ChangePassword(user, data.Password);
        }

        #endregion

        #endregion
    }
}
