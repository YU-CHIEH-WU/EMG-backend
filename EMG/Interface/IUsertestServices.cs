using EMG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMG.Interface
{
    public interface IUsertestServices
    {
        void Register(Usertest instance);

        bool AccountCheck(string account);

        Usertest Get(string account);

        void Update(Usertest nowUser);

        //string Login(User loginUser);

        string LoginCheck(string account, string password);

        void ChangePassword(Usertest data, string newPassword);

        bool PasswordCheck(Usertest checkUser, string password);

        string GetRole(string account);

        string GetNameByAccount(string account);

        string HashPassword(string password);
    }
}