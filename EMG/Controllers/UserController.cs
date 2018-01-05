using AutoMapper;
using EMG.Interface;
using EMG.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;
using System.Web.Security;

namespace EMG.Controllers
{
    public class UserController : Controller
    {
        private IMessageServices _messageServices;
        private IUserServices _userservices;
        private IReplyServices _replyServices;
        private IFileServices _fileServices;

        public UserController(IUserServices userservices, IMessageServices messageServices, IReplyServices replyServices)
        {
            this._messageServices = messageServices;
            this._userservices = userservices;
            this._replyServices = replyServices;
        }


        //#region 註冊
        //public ActionResult Register()
        //{
        //    var list = new List<SelectListItem>();
        //    list.Add(new SelectListItem() { Text = "請選擇性別", Value = "", Selected = true });
        //    list.Add(new SelectListItem() { Text = "男生", Value = "男生" });
        //    list.Add(new SelectListItem() { Text = "女生", Value = "女生" });
        //    ViewBag.SexList = list;
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Register(RegisterView RegisterUser)
        //{
        //    User newUser = Mapper.Map<User>(RegisterUser);
        //    _userservices.Register(newUser);
        //    return RedirectToAction("Index", "Home");
        //}

        //#endregion    

        #region 註冊
        public ActionResult Register()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = "請選擇性別", Value = "", Selected = true });
            list.Add(new SelectListItem() { Text = "男", Value = "男" });
            list.Add(new SelectListItem() { Text = "女", Value = "女" });
            ViewBag.SexList = list;
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterView RegisterUser, HttpPostedFileBase file)
        {           
            if (file != null)
            {
                User newUser = Mapper.Map<User>(RegisterUser);
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var type = Path.GetExtension(file.FileName);              
                var size = file.ContentLength;
                //將檔案存入資料庫                
                var pathName = newUser.Account + type;
                var path = Path.Combine(Server.MapPath("~/FileUpload"), pathName);
                
                UserHabit newHabit = Mapper.Map<UserHabit>(RegisterUser);
                _userservices.Register(newUser,fileName, type, path, size, newHabit);
                //將檔案存放至實體路徑
                file.SaveAs(path);
            }          
            else
            {
                var fileName = "";
                var path = "";
                var type = "";
                int size = 0;
                User newUser = Mapper.Map<User>(RegisterUser);
                UserHabit newHabit = Mapper.Map<UserHabit>(RegisterUser);
                _userservices.Register(newUser, fileName, type, path, size, newHabit);
            }
            return RedirectToAction("Index", "Home");
        }

        #endregion    

        #region 登入

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "POST,OPTIONS")]
        public ActionResult Login(LoginView loginUser)
        {
            var checkStr = _userservices.LoginCheck(loginUser.Account, loginUser.Password);
            var userPhoto = _userservices.Get(loginUser.Account).Url;
            if (checkStr == "")
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, _userservices.GetNameByAccount(loginUser.Account))
                }, "ApplicationCookie");

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;
                authManager.SignIn(identity);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // 有驗證錯誤訊息，加入頁面模型中
                ModelState.AddModelError("", checkStr);
                // 將資料回填至View 中
                return View(loginUser);
            }
        }

        #endregion

        #region 登出

        [Authorize]
        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;
            authManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region 個人資料

        [Authorize]
        public ActionResult ProFile()
        {
            //根據登入帳號取得使用者資料
            User nowUser = _userservices.Get(User.Identity.Name);
            ProfileView profileUser = Mapper.Map<ProfileView>(nowUser);
            return View(profileUser);
        }

        #endregion

        #region 修改個人資料

        [Authorize]
        public ActionResult UpdateProfile()
        {
            User nowUser = _userservices.Get(User.Identity.Name);
            var profileUser = Mapper.Map<ProfileView>(nowUser);
            return View(profileUser);
        }

        [HttpPost]
        public ActionResult UpdateProfile(ProfileView profileUser)
        {
            profileUser.Account = User.Identity.Name;
            var nowUser = Mapper.Map<User>(profileUser);
            _userservices.Update(nowUser);
            return RedirectToAction("Profile");
        }
        #endregion

        #region 修改密碼
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordView data)
        {
            var nowUser = _userservices.Get(User.Identity.Name);
            if (_userservices.PasswordCheck(nowUser, data.OldPassword))
            {
                if (_userservices.PasswordCheck(nowUser, data.NewPassword))
                {
                    ModelState.AddModelError("", "新密碼不可與舊密碼相同");
                    return View();
                }
                else
                {
                    _userservices.ChangePassword(nowUser, data.NewPassword);
                    return RedirectToAction("Index","Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "密碼輸入錯誤");
                return View();
            }
        }

        #endregion

        #region 上傳大頭貼
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var type = Path.GetExtension(file.FileName);
                var pathName = fileName + type;
                var path = Path.Combine(Server.MapPath("~/FileUpload"), pathName);
                var size = file.ContentLength;
                //將檔案存入資料庫
                _fileServices.Create(fileName, type, path, size);
                //將檔案存放至實體路徑
                file.SaveAs(path);
            }
            return RedirectToAction("Profile", "User");
        }
        #endregion
    }
}