using EMG.Interface;
using EMG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace EMG.Services
{
    public class UsertestServices : IUsertestServices
    {
        private IUnitOfWork _db;

        private IRepository<Usertest> _repository;

        public UsertestServices(IUnitOfWork uow, IRepository<Usertest> repository)
        {
            this._db = uow;
            this._repository = repository;
        }

        #region 註冊

        public void Register(Usertest newUser)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - newUser.Birth.Year;
            if (newUser.Birth > today.AddYears(-age)) age--;
            Double BMI = newUser.Weight / ((newUser.Height / 100)*(newUser.Height / 100));
            //HashPassword
            newUser.Password = HashPassword(newUser.Password);
            newUser.Age = age;
            if (newUser.Sex == "男生")
            {
                newUser.BMR = newUser.Weight * 13.7 + newUser.Height * 5.0 - (6.8 * age) + 66;
                newUser.Bodyfat = (1.2 * BMI) + (0.23 * age) - 5.4 - (10.8 * 1);
            }
            else if (newUser.Sex == "女生")
            {
                newUser.BMR = newUser.Weight * 9.6 + newUser.Height * 1.8 - (4.7 * age) + 655;
                newUser.Bodyfat = (1.2 * BMI) + (0.23 * age) - 5.4 - (10.8 * 0);
            }        
            _repository.Create(newUser);
           _db.Save();
        }

        #endregion

        #region 帳號重複確認

        public bool AccountCheck(string account)
        {
            //取得會員資料
            Usertest serch = Get(account);
            //判斷是否有會員
            bool result = (serch == null);
            return result;
        }

        #endregion

        #region 取得使用者資料

        public Usertest Get(string account)
        {
            return _repository.Get(p => p.Account == account);
        }

        #endregion

        #region 修改使用者資料

        public void Update(Usertest nowUser)
        {
            _repository.Update(nowUser);
            _db.Save();
        }

        #endregion

        //#region 登入

        //public string Login(User loginUser)
        //{
        //    string roleData = GetRole(loginUser.Account);
        //    // 新增一個登入用Ticket
        //    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1
        //    , loginUser.Account // 使用者名稱
        //    , DateTime.Now // 起始時間
        //    , DateTime.Now.AddMinutes(30) // 到期時間，這裡設定為30 分鐘後
        //    , false // 設定是否以Cookie 存取
        //    , roleData // 使用者資料，這裡存入角色資料
        //    , FormsAuthentication.FormsCookiePath); // 設定儲存路徑，使用預設路徑
        //    // 將資料加密成字串傳回
        //    // 將資料存入Cookies 中
        //    return FormsAuthentication.Encrypt(ticket);
        //}
        //#endregion

        #region 登入確認

        public string LoginCheck(string account, string password)
        {
            // 取得傳入帳號的使用者資料
            Usertest loginUser = Get(account);
            // 判斷是否有此使用者
            if (loginUser != null)
            {
                // 進行帳號密碼確認
                if (PasswordCheck(loginUser, password))
                {
                    return "";
                }
                else
                {
                    return " 密碼輸入錯誤";
                }
            }
            else
            {
                return " 無此使用者帳號，請去註冊";
            }
        }

        #endregion

        #region 修改密碼

        public void ChangePassword(Usertest nowUser, string newPassword)
        {
            nowUser.Password = HashPassword(newPassword);
            Update(nowUser);
        }

        #endregion

        #region 密碼確認
        // 進行密碼確認方法
        public bool PasswordCheck(Usertest checkUser, string password)
        {
            // 判斷資料庫裡的密碼資料與傳入密碼資料Hash 後是否一樣
            bool result = checkUser.Password.Equals(HashPassword(password));
            return result;
        }
        #endregion

        #region 取得角色
        // 取得會員的權限角色資料
        public string GetRole(string account)
        {
            // 宣告初始角色字串
            string role = "User";
            // 取得傳入帳號的會員資料
            Usertest loginUser = Get(account);
            // 判斷資料庫欄位，用以確認是否為Admin
            if (loginUser.IsAdmin)
            {
                role += ",Admin";
            }
            // 回傳最後結果
            return role;
        }
        #endregion

        #region Hash 密碼
        //Hash 密碼用的方法
        public string HashPassword(string password)
        {
            // 宣告Hash 時所添加的無意義亂數值
            string saltkey = "sE7T8E4Wr81sd9qWQG3Jjgio8tj3kg";
            // 將剛剛宣告的字串與密碼結合
            string saltAndPassword = String.Concat(password, saltkey);
            // 定義SHA1 的HASH 物件
            SHA1CryptoServiceProvider sha1Hasher = new SHA1CryptoServiceProvider();
            // 取得密碼轉換成byte 資料
            byte[] passwordData = Encoding.Default.GetBytes(saltAndPassword);
            // 取得Hash 後byte 資料
            byte[] hashDate = sha1Hasher.ComputeHash(passwordData);
            // 將Hash 後byte 資料轉換成string
            string hashresult = "";
            for (int i = 0; i < hashDate.Length; i++)
            {
                hashresult += hashDate[i].ToString("x2");
            }
            // 回傳Hash 後結果
            return hashresult;
        }

        #endregion

        #region 取得姓名

        public string GetNameByAccount(string account)
        {
            Usertest currentUser = Get(account);
            return currentUser.Account;
        }

        #endregion
    }
}