using EMG.Model;
using System.Collections.Generic;

namespace EMG.Interface
{
    public interface IUserServices
    {
        void Register(User instance, string fileName, string type, string path, int size, UserHabit newHabit);

        bool AccountCheck(string account);

        User Get(string account);

        string GetAccount(string name);

        UserHabit GetUserHabit(string account);

        void Update(User nowUser);

        //string Login(User loginUser);

        string LoginCheck(string account, string password);

        void ChangePassword(User data, string newPassword);

        bool PasswordCheck(User checkUser, string password);

        string GetRole(string account);

        string GetNameByAccount(string account);

        string HashPassword(string password);


        //APP
        void saveHabit(UserHabit data);

        void editProfile(ProfileView data);
    }
}
